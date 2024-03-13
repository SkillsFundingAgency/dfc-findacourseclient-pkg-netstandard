using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Text;
using Xunit;

namespace DFC.FindACourseClient.UnitTests
{
    public class RequestParameterSerialisationTests
    {
        private readonly CourseSearchRequest courseSearchRequest;
        private readonly JsonMediaTypeFormatter jsonMediaTypeFormatter;
        private readonly List<string> shouldContainParameters;
        private readonly MemoryStream ms;

        public RequestParameterSerialisationTests()
        {
            courseSearchRequest = new CourseSearchRequest() { SubjectKeyword = "SearchTerm", SortBy = 1, Start = 2, Limit = 20, CourseTypes = new List<CourseType>() { CourseType.All }, SectorIds = new List<int> { 1, 2, 3 }, EducationLevels = new List<EducationLevel>() { EducationLevel.All } };
            jsonMediaTypeFormatter = new JsonMediaTypeFormatter();
            shouldContainParameters = new List<string> { nameof(courseSearchRequest.SubjectKeyword), nameof(courseSearchRequest.SortBy), nameof(courseSearchRequest.Start), nameof(courseSearchRequest.Limit), nameof(courseSearchRequest.CourseTypes), nameof(courseSearchRequest.SectorIds), nameof(courseSearchRequest.EducationLevels) };
            ms = new MemoryStream();
        }

        ~RequestParameterSerialisationTests()
        {
            ms.Dispose();
        }

        [Fact]
        public void SimpleSearchShouldOnlyContainMinimumParameters()
        {
            CheckRequestContainsOnlyExpectedParameters();
        }

        [Theory]
        [InlineData("TestProvider", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        public void ProviderNameIsOnlySentIfValid(string providerName, bool shouldSend)
        {
            courseSearchRequest.ProviderName = providerName;

            if (shouldSend)
            {
                shouldContainParameters.Add(nameof(courseSearchRequest.ProviderName));
            }

            CheckRequestContainsOnlyExpectedParameters();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void QualificationLevelsIsSentIfValid(bool hasValue)
        {
            courseSearchRequest.QualificationLevels = hasValue ? new List<string>() { "1", "2", "3" } : new List<string>();

            if (hasValue)
            {
                shouldContainParameters.Add(nameof(courseSearchRequest.QualificationLevels));
            }

            CheckRequestContainsOnlyExpectedParameters();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void StudyModesIsSentIfValid(bool hasValue)
        {
            courseSearchRequest.StudyModes = hasValue ? new List<StudyMode>() { StudyMode.Flexible, StudyMode.FullTime } : new List<StudyMode>();

            if (hasValue)
            {
                shouldContainParameters.Add(nameof(courseSearchRequest.StudyModes));
            }

            CheckRequestContainsOnlyExpectedParameters();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DeliveryModesIsSentIfValid(bool hasValue)
        {
            courseSearchRequest.DeliveryModes = hasValue ? new List<DeliveryMode>() { DeliveryMode.Online, DeliveryMode.ClassroomBased } : new List<DeliveryMode>();

            if (hasValue)
            {
                shouldContainParameters.Add(nameof(courseSearchRequest.DeliveryModes));
            }

            CheckRequestContainsOnlyExpectedParameters();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void AttendancePatternsIsSentIfValid(bool hasValue)
        {
            courseSearchRequest.AttendancePatterns = hasValue ? new List<int>() { 1, 2, 3 } : new List<int>();

            if (hasValue)
            {
                shouldContainParameters.Add(nameof(courseSearchRequest.AttendancePatterns));
            }

            CheckRequestContainsOnlyExpectedParameters();
        }

        [Theory]
        [InlineData("TestTown", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        public void TownIsOnlySentIfValid(string town, bool shouldSend)
        {
            courseSearchRequest.Town = town;

            if (shouldSend)
            {
                shouldContainParameters.Add(nameof(courseSearchRequest.Town));
            }

            CheckRequestContainsOnlyExpectedParameters();
        }

        [Theory]
        [InlineData("B1 1AA", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        public void PostcodeAndDistanceSentIfValid(string postCode, bool shouldSend)
        {
            courseSearchRequest.Postcode = postCode;

            if (shouldSend)
            {
                courseSearchRequest.Distance = 123;
                shouldContainParameters.Add(nameof(courseSearchRequest.Postcode));
                shouldContainParameters.Add(nameof(courseSearchRequest.Distance));
            }

            CheckRequestContainsOnlyExpectedParameters();
        }

        [Theory]
        [InlineData("01-02-2021", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        public void StartDateToSentIfValid(string startDateTo, bool shouldSend)
        {
            courseSearchRequest.StartDateTo = startDateTo;

            if (shouldSend)
            {
                shouldContainParameters.Add(nameof(courseSearchRequest.StartDateTo));
            }

            CheckRequestContainsOnlyExpectedParameters();
        }

        [Theory]
        [InlineData("01-02-2021", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        public void StartDateFromSentIfValid(string startDateFrom, bool shouldSend)
        {
            courseSearchRequest.StartDateFrom = startDateFrom;

            if (shouldSend)
            {
                shouldContainParameters.Add(nameof(courseSearchRequest.StartDateFrom));
            }

            CheckRequestContainsOnlyExpectedParameters();
        }

        [Fact]
        public void LogLatAndDistanceIsSentIfValid()
        {
            courseSearchRequest.Longitude = 12.34;
            courseSearchRequest.Latitude = 56.78;
            courseSearchRequest.Distance = 123;
            shouldContainParameters.Add(nameof(courseSearchRequest.Longitude));
            shouldContainParameters.Add(nameof(courseSearchRequest.Distance));
            shouldContainParameters.Add(nameof(courseSearchRequest.Latitude));
            CheckRequestContainsOnlyExpectedParameters();
        }

        [Theory]
        [InlineData("TestCode", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        public void CampaignCodeIsOnlySentIfValid(string campaignCode, bool shouldSend)
        {
            courseSearchRequest.CampaignCode = campaignCode;

            if (shouldSend)
            {
                shouldContainParameters.Add(nameof(courseSearchRequest.CampaignCode));
            }

            CheckRequestContainsOnlyExpectedParameters();
        }

        private void CheckRequestContainsOnlyExpectedParameters()
        {
            jsonMediaTypeFormatter.WriteToStream(courseSearchRequest.GetType(), courseSearchRequest, ms, Encoding.Default);
            var request = Encoding.ASCII.GetString(ms.ToArray());

            //All the properties that can be serialised
            foreach (PropertyInfo prop in courseSearchRequest.GetType().GetProperties())
            {
                // Is this property expected to be send in the request
                if (shouldContainParameters.Contains(prop.Name))
                {
                    var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    if (type == typeof(string))
                    {
                        request.Should().Contain($"\"{prop.Name}\":\"{prop.GetValue(courseSearchRequest)}\"");
                    }
                    else if (type == typeof(int) || type == typeof(double))
                    {
                        request.Should().Contain($"\"{prop.Name}\":{prop.GetValue(courseSearchRequest)}");
                    }
                    else if (type == typeof(List<int>))
                    {
                        var propList = prop.GetValue(courseSearchRequest) as List<int>;
                        request.Should().Contain($"\"{prop.Name}\":[{string.Join(",", propList.Select(n => n.ToString(CultureInfo.CurrentCulture)).ToArray())}]");
                    }
                    else if (type == typeof(List<StudyMode>))
                    {
                        var propList = prop.GetValue(courseSearchRequest) as List<StudyMode>;
                        request.Should().Contain($"\"{prop.Name}\":[{string.Join(",", propList.Select(n => ((int)n)).ToArray())}]");
                    }
                    else if (type == typeof(List<DeliveryMode>))
                    {
                        var propList = prop.GetValue(courseSearchRequest) as List<DeliveryMode>;
                        request.Should().Contain($"\"{prop.Name}\":[{string.Join(",", propList.Select(n => ((int)n)).ToArray())}]");
                    }
                    else if (type == typeof(List<CourseType>))
                    {
                        var propList = prop.GetValue(courseSearchRequest) as List<CourseType>;
                        request.Should().Contain($"\"{prop.Name}\":[{string.Join(",", propList.Select(n => ((int)n)).ToArray())}]");
                    }
                    else if (type == typeof(List<EducationLevel>))
                    {
                        var propList = prop.GetValue(courseSearchRequest) as List<EducationLevel>;
                        request.Should().Contain($"\"{prop.Name}\":[{string.Join(",", propList.Select(n => ((int)n)).ToArray())}]");
                    }
                    else if (type == typeof(List<string>))
                    {
                        var propList = prop.GetValue(courseSearchRequest) as List<string>;
                        request.Should().Contain($"\"{prop.Name}\":[{string.Join(",", propList.Select(n => $"\"{n}\"").ToArray())}]");
                    }
                    else
                    {
                        Assert.True(true == false, "Type of property is not supported, amend this test");
                    }
                }
                else
                {
                    //If the test is not expecting the property it should not be there
                    request.Should().NotContain($"\"{prop.Name}\":");
                }
            }
        }
    }
}