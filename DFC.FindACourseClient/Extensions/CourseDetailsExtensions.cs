using DFC.FindACourseClient.Models.APIResponses.CourseGet;
using DFC.FindACourseClient.Models.ExternalInterfaceModels;
using DFC.FindACourseClient.Models.ExternalInterfaceModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DFC.FindACourseClient.Extensions
{
    public static class CourseDetailsExtensions
    {
        public static CourseDetails ConvertToCourseDetails(this CourseRunDetailResponse input)
        {
            const string CourseDetailsPage = "/find-a-course/course-details";
            var address = new Dictionary<string, string>
            {
                [nameof(input.Venue.AddressLine1)] = input?.Venue?.AddressLine1,
                [nameof(input.Venue.AddressLine2)] = input?.Venue?.AddressLine2,
                [nameof(input.Venue.Town)] = input?.Venue?.Town,
                [nameof(input.Venue.Postcode)] = input?.Venue?.Postcode,
            };

            return new CourseDetails
            {
                CourseId = input.Course.CourseId.ToString(),
                Cost = input.Cost.ToString(),
                StartDate = input.StartDate.GetValueOrDefault(),
                Title = input.CourseName,
                AttendanceMode = input.DeliveryMode.ToString(),
                AttendancePattern = input.AttendancePattern.ToString(),
                AwardingOrganisation = input.Course.AwardOrgCode,
                CourseLink = $"{CourseDetailsPage}?{nameof(CourseDetails.CourseId)}={input.Course.CourseId}",
                Description = input.Course.CourseDescription,
                CourseWebpageLink = input.CourseURL,
                Duration = $"{input.DurationValue} {input.DurationUnit.ToString()}",
                EntryRequirements = input.Course.EntryRequirements,
                EquipmentRequired = input.Course.WhatYoullNeed,
                Oppurtunities = input.AlternativeCourseRuns.Select(x =>
                    new Oppurtunity
                    {
                        OppurtunityId = x.CourseRunId.ToString(),
                        StartDate = x.StartDate.ToString(),
                        VenueName = x.Venue.VenueName,
                        VenueUrl = x.Venue.Website,
                    }).ToList(),
                ProviderDetails = new ProviderDetails
                {
                    Website = input.Provider.Website,
                    Town = input.Provider.Town,
                    AddressLine = input.Provider.AddressLine1,
                    AddressLine2 = input.Provider.AddressLine2,
                    County = input.Provider.County,
                    EmailAddress = input.Provider.Email,
                    EmployerSatisfactionSpecified = input.Provider.EmployerSatisfaction.HasValue,
                    EmployerSatisfaction = double.Parse(input.Provider.EmployerSatisfaction.ToString()),
                    LearnerSatisfactionSpecified = input.Provider.LearnerSatisfaction.HasValue,
                    LearnerSatisfaction = double.Parse(input.Provider.LearnerSatisfaction.ToString()),
                    Latitude = input.Venue?.Latitude.ToString(),
                    Longitude = input.Venue?.Longitude.ToString(),
                    Name = input.Provider.ProviderName,
                    PhoneNumber = input.Provider.Telephone,
                    PostCode = input.Provider.Postcode,
                },
                ProviderName = input.Provider.ProviderName,
                QualificationLevel = Enum.Parse(typeof(QualificationLevel), input.Qualification.QualificationLevel.ToString()).ToString(),
                QualificationName = input.Qualification.LearnAimRefTitle,
                StartDateLabel = "Course start date",
                StudyMode = input.StudyMode.ToString(),
                VenueDetails = new Venue
                {
                    Location = new Address
                    {
                        Town = input.Venue?.Town,
                        AddressLine1 = input.Venue?.AddressLine1,
                        AddressLine2 = input.Venue?.AddressLine2,
                        Postcode = input.Venue?.Postcode,
                        County = input.Venue?.County,
                        Longitude = input.Venue?.Longitude.ToString(),
                        Latitude = input.Venue?.Latitude.ToString(),
                    },
                },
                SubjectCategory = input.Qualification.SectorSubjectAreaTier2Desc,
                LocationDetails = new LocationDetails
                {
                    //Distance = dont have this cos Details doesnt take the postcode so cant work out the distance to venue
                    LocationAddress = string.Join(", ", address.Where(x => !string.IsNullOrWhiteSpace(x.Value)).Select(add => add.Value)),
                },
                Location = input.Venue?.VenueName,
                AdditionalPrice = input.CostDescription,
                AdvancedLearnerLoansOffered = input.Course.AdvancedLearnerLoan,
                AssessmentMethod = input.Course.HowYoullBeAssessed,
                //LanguageOfInstruction = not present
                //SupportingFacilities = not present in source
            };
        }
    }
}