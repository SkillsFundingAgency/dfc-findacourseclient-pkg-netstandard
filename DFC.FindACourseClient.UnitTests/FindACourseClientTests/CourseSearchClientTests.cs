using DFC.FindACourseClient.Contracts;
using DFC.FindACourseClient.Contracts.CosmosDb;
using DFC.FindACourseClient.Models;
using DFC.FindACourseClient.Models.Configuration;
using DFC.FindACourseClient.Models.CosmosDb;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Xunit;

namespace DFC.FindACourseClient.UnitTests
{
    [Trait("Course Search Client", "Tests")]
    public class CourseSearchClientTests
    {
        private readonly ILogger<CourseSearchClient> fakeLogger;
        private readonly CourseSearchClientSettings courseSearchClientSettings;
        private readonly ICosmosRepository<APIAuditRecordCourse> fakeAuditRepository;
        private readonly IServiceClientProxy fakeServiceClientProxy;
        private readonly IMessageConverter fakeMessageConverter;
        private readonly ServiceInterface fakeClientChannel;
        private readonly CourseListInput courseListInput;

        public CourseSearchClientTests()
        {
            fakeLogger = A.Fake<ILogger<CourseSearchClient>>();
            fakeAuditRepository = A.Fake<ICosmosRepository<APIAuditRecordCourse>>();
            fakeServiceClientProxy = A.Fake<IServiceClientProxy>();
            fakeMessageConverter = A.Fake<IMessageConverter>();
            fakeClientChannel = A.Fake<ServiceInterface>(opt => opt.Implements<IClientChannel>());

            courseSearchClientSettings = new CourseSearchClientSettings()
            {
                CourseSearchSvcSettings = new CourseSearchSvcSettings()
                {
                    RequestTimeOutSeconds = 10,
                    ServiceEndpoint = new Uri("http://test.com"),
                },
            };

            courseListInput = new CourseListInput()
            {
                CourseListRequest = new CourseListRequestStructure()
                {
                    CourseSearchCriteria = new SearchCriteriaStructure()
                    {
                        APIKey = "APIKey",
                    },
                },
            };

            A.CallTo(() => fakeServiceClientProxy.CreateChannel(A<ChannelFactory<ServiceInterface>>.Ignored)).Returns((IClientChannel)fakeClientChannel);
            A.CallTo(() => fakeMessageConverter.GetCourseListRequest(A<string>.Ignored, A<CourseSearchSvcSettings>.Ignored)).Returns(courseListInput);
        }

        [Fact]
        public async Task GetCoursesAsync()
        {
            //Arrange
            var courseSearchClient = new CourseSearchClient(courseSearchClientSettings, fakeMessageConverter, fakeServiceClientProxy, fakeLogger, fakeAuditRepository);
            var expectedResult = A.CollectionOfDummy<CourseSumary>(3);

            A.CallTo(() => fakeClientChannel.CourseListAsync(A<CourseListInput>.Ignored)).Returns(A.Dummy<CourseListOutput>());
            A.CallTo(() => fakeMessageConverter.ConvertToCourse(A<CourseListOutput>.Ignored)).Returns(expectedResult);

            //Act
            var returnedCourse = await courseSearchClient.GetCoursesAsync("a-keyword").ConfigureAwait(false);

            //Asserts
            returnedCourse.Should().BeEquivalentTo(expectedResult);
            A.CallTo(() => fakeAuditRepository.UpsertAsync(A<APIAuditRecordCourse>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void GetCoursesAsyncExceptions()
        {
            //Arrange
            var courseSearchClient = new CourseSearchClient(courseSearchClientSettings, fakeMessageConverter, fakeServiceClientProxy, fakeLogger, fakeAuditRepository);
            A.CallTo(() => fakeClientChannel.CourseListAsync(A<CourseListInput>.Ignored)).Throws(new ApplicationException());

            //Act
            Func<Task> f = async () => { await courseSearchClient.GetCoursesAsync("a-keyword").ConfigureAwait(false); };

            //Asserts
            f.Should().Throw<ApplicationException>();
            A.CallTo(() => fakeAuditRepository.UpsertAsync(A<APIAuditRecordCourse>.Ignored)).MustNotHaveHappened();
        }
    }
}
