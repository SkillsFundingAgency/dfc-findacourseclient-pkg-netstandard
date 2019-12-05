using DFC.FindACourseClientV2.Contracts.CosmosDb;
using DFC.FindACourseClientV2.Models.CosmosDb;
using DFC.FindACourseClientV2.Services;
using FakeItEasy;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DFC.FindACourseClientV2.UnitTests.Services
{
    public class AuditServiceTests
    {
        [Fact]
        public async Task CreateAuditUpsertsToDatabase()
        {
            // Arrange
            const string request = "Some Sample request";
            const string response = "Some Sample response";
            var correlationId = Guid.NewGuid();

            var repository = A.Fake<ICosmosRepository<ApiAuditRecordCourse>>();
            var auditService = new AuditService(repository);

            // Act
            await auditService.CreateAudit(request, response, correlationId).ConfigureAwait(false);

            // Assert
            A.CallTo(() => repository.UpsertAsync(A<ApiAuditRecordCourse>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}