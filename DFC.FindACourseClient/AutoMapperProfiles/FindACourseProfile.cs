using AutoMapper;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DFC.FindACourseClientFramework.IntergrationTests")]

namespace DFC.FindACourseClient
{
    public class FindACourseProfile : Profile
    {
        public FindACourseProfile()
        {
            // Course Details
            CreateMap<CourseRunDetailResponse, CourseDetails>()
                .ForMember(d => d.CourseId, s => s.MapFrom(f => f.Course.CourseId.ToString()))
                .ForMember(d => d.Cost, s => s.MapFrom(f => f.Cost))
                .ForMember(d => d.StartDate, s => s.MapFrom(f => f.StartDate))
                .ForMember(d => d.Title, s => s.MapFrom(f => f.CourseName))
                .ForMember(d => d.AttendanceMode, s => s.MapFrom(f => f.DeliveryMode.ToString()))
                .ForMember(d => d.AttendancePattern, s => s.MapFrom(f => f.AttendancePattern.ToString()))
                .ForMember(d => d.Description, s => s.MapFrom(f => f.Course.CourseDescription))
                .ForMember(d => d.CourseWebpageLink, s => s.MapFrom(f => f.CourseURL))
                .ForMember(d => d.Duration, s => s.MapFrom(f => $"{f.DurationValue} {f.DurationUnit.ToString()}"))
                .ForMember(d => d.EntryRequirements, s => s.MapFrom(f => f.Course.EntryRequirements))
                .ForMember(d => d.EquipmentRequired, s => s.MapFrom(f => f.Course.WhatYoullNeed))
                .ForMember(d => d.Oppurtunities, s => s.MapFrom(f => f.AlternativeCourseRuns))
                .ForMember(d => d.ProviderDetails, s => s.MapFrom(f => f))
                .ForMember(d => d.ProviderName, s => s.MapFrom(f => f.Provider.ProviderName))
                .ForMember(d => d.QualificationLevel, s => s.MapFrom(f => f.Qualification.QualificationLevel.ToString()))
                .ForMember(d => d.QualificationName, s => s.MapFrom(f => f.Qualification.LearnAimRefTitle))
                .ForMember(d => d.StartDateLabel, s => s.MapFrom(f => "Course start date"))
                .ForMember(d => d.StudyMode, s => s.MapFrom(f => f.StudyMode.ToString()))
                .ForMember(d => d.VenueDetails, s => s.MapFrom(f => f.Venue))
                .ForMember(d => d.SubjectCategory, s => s.MapFrom(f => f.Qualification.SectorSubjectAreaTier2Desc))
                .ForMember(d => d.LocationDetails, s => s.MapFrom(f => f.Venue))
                .ForMember(d => d.Location, s => s.MapFrom(f => f.Venue.VenueName))
                .ForMember(d => d.AdditionalPrice, s => s.MapFrom(f => f.CostDescription))
                .ForMember(d => d.AdvancedLearnerLoansOffered, s => s.MapFrom(f => f.Course.AdvancedLearnerLoan))
                .ForMember(d => d.AssessmentMethod, s => s.MapFrom(f => f.Course.HowYoullBeAssessed))
                .ForMember(d => d.SubRegions, s => s.MapFrom(f => f.SubRegions));

            CreateMap<CourseDetailResponseAlternativeCourseRun, Oppurtunity>()
                .ForMember(d => d.OppurtunityId, s => s.MapFrom(f => f.CourseRunId.ToString()))
                .ForMember(d => d.StartDate, s => s.MapFrom(f => f.StartDate.ToString()))
                .ForMember(d => d.VenueName, s => s.MapFrom(f => f.Venue.VenueName))
                .ForMember(d => d.VenueUrl, s => s.MapFrom(f => f.Venue.Website));

            CreateMap<CourseRunDetailResponse, ProviderDetails>()
                .ForMember(d => d.Website, s => s.MapFrom(f => f.Provider.Website))
                .ForMember(d => d.Town, s => s.MapFrom(f => f.Provider.Town))
                .ForMember(d => d.AddressLine, s => s.MapFrom(f => f.Provider.AddressLine1))
                .ForMember(d => d.AddressLine2, s => s.MapFrom(f => f.Provider.AddressLine2))
                .ForMember(d => d.County, s => s.MapFrom(f => f.Provider.County))
                .ForMember(d => d.EmailAddress, s => s.MapFrom(f => f.Provider.Email))
                .ForMember(d => d.EmployerSatisfactionSpecified, s => s.MapFrom(f => f.Provider.EmployerSatisfaction.HasValue))
                .ForMember(d => d.EmployerSatisfaction, s => s.MapFrom(f => double.Parse(f.Provider.EmployerSatisfaction.GetValueOrDefault(0).ToString())))
                .ForMember(d => d.LearnerSatisfactionSpecified, s => s.MapFrom(f => f.Provider.LearnerSatisfaction.HasValue))
                .ForMember(d => d.LearnerSatisfaction, s => s.MapFrom(f => double.Parse(f.Provider.LearnerSatisfaction.GetValueOrDefault(0).ToString())))
                .ForMember(d => d.Latitude, s => s.MapFrom(f => f.Venue.Latitude))
                .ForMember(d => d.Longitude, s => s.MapFrom(f => f.Venue.Longitude))
                .ForMember(d => d.Name, s => s.MapFrom(f => f.Provider.ProviderName))
                .ForMember(d => d.PhoneNumber, s => s.MapFrom(f => f.Provider.Telephone))
                .ForMember(d => d.PostCode, s => s.MapFrom(f => f.Provider.Postcode));

            CreateMap<CourseDetailResponseVenue, Venue>()
                .ForMember(d => d.Location, s => s.MapFrom(f => f));

            CreateMap<CourseDetailResponseVenue, LocationDetails>()
                .ForMember(d => d.LocationAddress, s => s.MapFrom(f => $"{f.AddressLine1}, {f.AddressLine2}, {f.Town}, {f.Postcode}"));

            CreateMap<CourseDetailResponseVenue, Address>()
                .ForMember(d => d.Longitude, s => s.MapFrom(f => f.Longitude.ToString()))
                .ForMember(d => d.Latitude, s => s.MapFrom(f => f.Latitude.ToString()));

            CreateMap<CourseDetailResponseSubRegion, SubRegion>();

            CreateMap<CourseDetailResponseParentRegion, ParentRegion>();

            // Course Search
            CreateMap<Result, Course>()
                .ForMember(d => d.CourseId, s => s.MapFrom(f => f.CourseId.ToString()))
                .ForMember(d => d.Title, s => s.MapFrom(f => f.QualificationCourseTitle))
                .ForMember(d => d.LocationDetails, s => s.MapFrom(f => f))
                .ForMember(d => d.StartDate, s => s.MapFrom(f => f.StartDate.ToString()))
                .ForMember(d => d.StartDateLabel, s => s.MapFrom(f => "Start date:"))
                .ForMember(d => d.AttendanceMode, s => s.MapFrom(f => f.DeliveryModeDescription))
                .ForMember(d => d.AttendancePattern, s => s.MapFrom(f => f.VenueAttendancePatternDescription))
                .ForMember(d => d.StudyMode, s => s.MapFrom(f => f.VenueStudyModeDescription))
                .ForMember(d => d.Location, s => s.MapFrom(f => f.VenueTown))
                .ForMember(d => d.Duration, s => s.MapFrom(f => $"{f.DurationValue} {f.DurationUnit.ToString()}"));

            CreateMap<Result, LocationDetails>()
                .ForMember(d => d.Distance, s => s.MapFrom(f => float.Parse(f.Distance ?? "0")))
                .ForMember(d => d.LocationAddress, s => s.MapFrom(f => f.VenueAddress));
        }
    }
}