using AutoMapper;
using DFC.FindACourseClient.AutoMapperProfiles;
using DFC.FindACourseClient.Extensions;
using System.Runtime.CompilerServices;
using Comp = DFC.CompositeInterfaceModels.FindACourseClient;

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
                .ForMember(d => d.AttendanceMode, s => s.MapFrom(f => f.DeliveryMode.GetFriendlyName()))
                .ForMember(d => d.AttendancePattern, s => s.MapFrom(f => f.AttendancePattern.GetFriendlyName()))
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
                .ForMember(d => d.StartDateLabel, s => s.ConvertUsing(new DetailsResponseStartDateValueConverter(), f => f))
                .ForMember(d => d.StudyMode, s => s.MapFrom(f => f.StudyMode.ToString()))
                .ForMember(d => d.VenueDetails, s => s.MapFrom(f => f.Venue))
                .ForMember(d => d.SubjectCategory, s => s.MapFrom(f => f.Qualification.SectorSubjectAreaTier2Desc))
                .ForMember(d => d.LocationDetails, s => s.MapFrom(f => f.Venue))
                .ForMember(d => d.Location, s => s.MapFrom(f => f.Venue.VenueName))
                .ForMember(d => d.AdditionalPrice, s => s.MapFrom(f => f.CostDescription))
                .ForMember(d => d.AssessmentMethod, s => s.MapFrom(f => f.Course.HowYoullBeAssessed))
                .ForMember(d => d.SubRegions, s => s.MapFrom(f => f.SubRegions))
                .ForMember(d => d.RunId, s => s.MapFrom(f => f.CourseRunId))
                .ForMember(d => d.LanguageOfInstruction, s => s.Ignore())
                .ForMember(d => d.SupportingFacilities, s => s.Ignore())
                .ForMember(d => d.AwardingOrganisation, s => s.MapFrom(f => f.Qualification.AwardOrgName))
                .ForMember(d => d.NextSteps, s => s.MapFrom(f => f.Course.WhereNext))
                .ForMember(d => d.WhatYoullLearn, s => s.MapFrom(f => f.Course.WhatYoullLearn))
                .ForMember(d => d.HowYoullLearn, s => s.MapFrom(f => f.Course.HowYoullLearn))
                .ForMember(d => d.Sector, s => s.MapFrom(f => f.Course.SectorDescription))
                .ForMember(d => d.CourseType, s => s.MapFrom(f => f.Course.CourseTypeDescription))
                .ForMember(d => d.EducationLevel, s => s.MapFrom(f => f.Course.EducationLevel.GetFriendlyName()))
                .ForMember(d => d.AwardingBody, s => s.MapFrom(f => f.Course.AwardingBody));

            CreateMap<CourseDetailResponseAlternativeCourseRun, Oppurtunity>()
                .ForMember(d => d.OppurtunityId, s => s.MapFrom(f => f.CourseRunId.ToString()))
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
                .ForMember(d => d.Location, s => s.MapFrom(f => f))
                .ForMember(d => d.EmailAddress, s => s.MapFrom(f => f.Email))
                .ForMember(d => d.PhoneNumber, s => s.MapFrom(f => f.Telephone))
                .ForMember(d => d.Fax, s => s.Ignore())
                .ForMember(d => d.Facilities, s => s.Ignore());

            CreateMap<CourseDetailResponseVenue, LocationDetails>()
                .ForMember(d => d.LocationAddress, s => s.MapFrom(f => $"{f.AddressLine1}, {f.AddressLine2}, {f.Town}, {f.Postcode}"))
                .ForMember(d => d.Distance, s => s.Ignore());

            CreateMap<CourseDetailResponseVenue, Address>()
                .ForMember(d => d.Longitude, s => s.MapFrom(f => f.Longitude.ToString()))
                .ForMember(d => d.Latitude, s => s.MapFrom(f => f.Latitude.ToString()));

            CreateMap<CourseDetailResponseSubRegion, SubRegion>();

            CreateMap<CourseDetailResponseParentRegion, ParentRegion>();

            // Course Search
            CreateMap<Result, Course>()
                .ForMember(d => d.CourseId, s => s.MapFrom(f => f.CourseId.ToString()))
                .ForMember(d => d.RunId, s => s.MapFrom(f => f.CourseRunId.ToString()))
                .ForMember(d => d.TLevelId, s => s.MapFrom(f => f.TLevelId.ToString()))
                .ForMember(d => d.TLevelLocationId, s => s.MapFrom(f => f.TLevelLocationId.ToString()))
                .ForMember(d => d.Title, s => s.MapFrom(f => f.CourseName))
                .ForMember(d => d.LocationDetails, s => s.MapFrom(f => f))
                .ForMember(d => d.StartDateLabel, s => s.ConvertUsing(new SearchResponseStartDateValueConverter(), f => f))
                .ForMember(d => d.AttendanceMode, s => s.MapFrom(f => f.DeliveryModeDescription))
                .ForMember(d => d.AttendancePattern, s => s.MapFrom(f => f.VenueAttendancePatternDescription))
                .ForMember(d => d.StudyMode, s => s.MapFrom(f => f.VenueStudyModeDescription))
                .ForMember(d => d.Location, s => s.MapFrom(f => f.VenueTown))
                .ForMember(d => d.Duration, s => s.MapFrom(f => $"{f.DurationValue} {f.DurationUnit.ToString()}"))
                .ForMember(d => d.Sector, s => s.MapFrom(f => f.SectorDescription))
                .ForMember(d => d.CourseType, s => s.MapFrom(f => f.CourseTypeDescription))
                .ForMember(d => d.EducationLevel, s => s.MapFrom(f => f.EducationLevelDescription))
                .ForMember(d => d.AwardingBody, s => s.MapFrom(f => f.AwardingBody));

            CreateMap<Result, LocationDetails>()
                .ForMember(d => d.Distance, s => s.MapFrom(f => float.Parse(f.Distance ?? "0")))
                .ForMember(d => d.LocationAddress, s => s.MapFrom(f => string.IsNullOrWhiteSpace(f.VenueAddress) ? f.Region : f.VenueAddress));

            CreateMap<Result, LocationDetails>()
                .ForMember(d => d.Distance, s => s.MapFrom(f => float.Parse(f.Distance ?? "0")))
                .ForMember(d => d.LocationAddress, s => s.MapFrom(f => string.IsNullOrWhiteSpace(f.VenueAddress) ? f.Region : f.VenueAddress));

            //NEW COMPOSITE MAPPINGS - NEED MERGING LATER ON

            // Course Search
            CreateMap<Result, Comp.Course>()
                .ForMember(d => d.CourseId, s => s.MapFrom(f => f.CourseId.ToString()))
                .ForMember(d => d.RunId, s => s.MapFrom(f => f.CourseRunId.ToString()))
                .ForMember(d => d.TLevelId, s => s.MapFrom(f => f.TLevelId.ToString()))
                .ForMember(d => d.TLevelLocationId, s => s.MapFrom(f => f.TLevelLocationId.ToString()))
                .ForMember(d => d.Title, s => s.MapFrom(f => f.CourseName))
                .ForMember(d => d.Description, s => s.MapFrom(f => f.CourseDescription))
                .ForMember(d => d.LocationDetails, s => s.MapFrom(f => f))
                .ForMember(d => d.StartDateLabel, s => s.ConvertUsing(new SearchResponseStartDateValueConverter(), f => f))
                .ForMember(d => d.AttendanceMode, s => s.MapFrom(f => f.DeliveryModeDescription))
                .ForMember(d => d.AttendancePattern, s => s.MapFrom(f => f.VenueAttendancePatternDescription))
                .ForMember(d => d.StudyMode, s => s.MapFrom(f => f.VenueStudyModeDescription))
                .ForMember(d => d.Location, s => s.MapFrom(f => f.VenueTown))
                .ForMember(d => d.Duration, s => s.MapFrom(f => $"{f.DurationValue} {f.DurationUnit.ToString()}"))
                .ForMember(d => d.Sector, s => s.MapFrom(f => f.SectorDescription))
                .ForMember(d => d.CourseType, s => s.MapFrom(f => f.CourseTypeDescription))
                .ForMember(d => d.EducationLevel, s => s.MapFrom(f => f.EducationLevelDescription))
                .ForMember(d => d.AwardingBody, s => s.MapFrom(f => f.AwardingBody));

            CreateMap<Result, Comp.LocationDetails>()
               .ForMember(d => d.Distance, s => s.MapFrom(f => float.Parse(f.Distance ?? "0")))
               .ForMember(d => d.LocationAddress, s => s.MapFrom(f => string.IsNullOrWhiteSpace(f.VenueAddress) ? f.Region : f.VenueAddress));

            CreateMap<Comp.CourseSearchFilters, CourseSearchFilters>();
            CreateMap<Comp.CourseSearchProperties, CourseSearchProperties>();

            // Course Details
            CreateMap<CourseRunDetailResponse, Comp.CourseDetails>()
                .ForMember(d => d.CourseId, s => s.MapFrom(f => f.Course.CourseId.ToString()))
                .ForMember(d => d.Cost, s => s.MapFrom(f => f.Cost))
                .ForMember(d => d.StartDate, s => s.MapFrom(f => f.StartDate))
                .ForMember(d => d.FlexibleStartDate, s => s.MapFrom(f => f.FlexibleStartDate))
                .ForMember(d => d.Title, s => s.MapFrom(f => f.CourseName))
                .ForMember(d => d.AttendanceMode, s => s.MapFrom(f => f.DeliveryMode.GetFriendlyName()))
                .ForMember(d => d.AttendancePattern, s => s.MapFrom(f => f.AttendancePattern.GetFriendlyName()))
                .ForMember(d => d.Description, s => s.MapFrom(f => f.Course.CourseDescription))
                .ForMember(d => d.CourseWebpageLink, s => s.MapFrom(f => f.CourseURL))
                .ForMember(d => d.Duration, s => s.MapFrom(f => $"{f.DurationValue} {f.DurationUnit.ToString()}"))
                .ForMember(d => d.EntryRequirements, s => s.MapFrom(f => f.Course.EntryRequirements))
                .ForMember(d => d.EquipmentRequired, s => s.MapFrom(f => f.Course.WhatYoullNeed))
                .ForMember(d => d.AlternativeCourseRuns, s => s.MapFrom(f => f.AlternativeCourseRuns))
                .ForMember(d => d.ProviderDetails, s => s.MapFrom(f => f))
                .ForMember(d => d.ProviderName, s => s.MapFrom(f => f.Provider.ProviderName))
                .ForMember(d => d.QualificationLevel, s => s.MapFrom(f => f.Qualification.QualificationLevel.ToString()))
                .ForMember(d => d.QualificationName, s => s.MapFrom(f => f.Qualification.LearnAimRefTitle))
                .ForMember(d => d.StartDateLabel, s => s.ConvertUsing(new DetailsResponseStartDateValueConverter(), f => f))
                .ForMember(d => d.StudyMode, s => s.MapFrom(f => f.StudyMode.ToString()))
                .ForMember(d => d.VenueDetails, s => s.MapFrom(f => f.Venue))
                .ForMember(d => d.SubjectCategory, s => s.MapFrom(f => f.Qualification.SectorSubjectAreaTier2Desc))
                .ForMember(d => d.LocationDetails, s => s.MapFrom(f => f.Venue))
                .ForMember(d => d.Location, s => s.MapFrom(f => f.Venue.VenueName))
                .ForMember(d => d.AdditionalPrice, s => s.MapFrom(f => f.CostDescription))
                .ForMember(d => d.AssessmentMethod, s => s.MapFrom(f => f.Course.HowYoullBeAssessed))
                .ForMember(d => d.SubRegions, s => s.MapFrom(f => f.SubRegions))
                .ForMember(d => d.RunId, s => s.MapFrom(f => f.CourseRunId))
                .ForMember(d => d.LanguageOfInstruction, s => s.Ignore())
                .ForMember(d => d.SupportingFacilities, s => s.Ignore())
                .ForMember(d => d.AwardingOrganisation, s => s.MapFrom(f => f.Qualification.AwardOrgName))
                .ForMember(d => d.NextSteps, s => s.MapFrom(f => f.Course.WhereNext))
                .ForMember(d => d.WhatYoullLearn, s => s.MapFrom(f => f.Course.WhatYoullLearn))
                .ForMember(d => d.HowYoullLearn, s => s.MapFrom(f => f.Course.HowYoullLearn))
                .ForMember(d => d.CourseType, s => s.MapFrom(f => f.Course.CourseTypeDescription))
                .ForMember(d => d.Sector, s => s.MapFrom(f => f.Course.SectorDescription))
                .ForMember(d => d.EducationLevel, s => s.MapFrom(f => f.Course.EducationLevelDescription))
                .ForMember(d => d.AwardingBody, s => s.MapFrom(f => f.Course.AwardingBody));

            CreateMap<CourseDetailResponseAlternativeCourseRun, Comp.AlternativeCourseRun>()
                .ForMember(d => d.RunId, s => s.MapFrom(f => f.CourseRunId.ToString()))
                .ForMember(d => d.VenueName, s => s.MapFrom(f => f.Venue.VenueName))
                .ForMember(d => d.VenueUrl, s => s.MapFrom(f => f.Venue.Website))
                .ForMember(d => d.CourseURL, s => s.MapFrom(f => f.CourseURL))
                .ForMember(d => d.StartDate, s => s.MapFrom(f => f.StartDate));

            CreateMap<CourseRunDetailResponse, Comp.ProviderDetails>()
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

            CreateMap<CourseDetailResponseVenue, Comp.Venue>()
                .ForMember(d => d.Location, s => s.MapFrom(f => f))
                .ForMember(d => d.EmailAddress, s => s.MapFrom(f => f.Email))
                .ForMember(d => d.PhoneNumber, s => s.MapFrom(f => f.Telephone))
                .ForMember(d => d.Fax, s => s.Ignore())
                .ForMember(d => d.Facilities, s => s.Ignore());

            CreateMap<CourseDetailResponseVenue, Comp.LocationDetails>()
                .ForMember(d => d.LocationAddress, s => s.MapFrom(f => $"{f.AddressLine1}, {f.AddressLine2}, {f.Town}, {f.Postcode}"))
                .ForMember(d => d.Distance, s => s.Ignore());

            CreateMap<CourseDetailResponseVenue, Comp.Address>()
                .ForMember(d => d.Longitude, s => s.MapFrom(f => f.Longitude.ToString()))
                .ForMember(d => d.Latitude, s => s.MapFrom(f => f.Latitude.ToString()));

            CreateMap<CourseDetailResponseSubRegion, Comp.SubRegion>();
            CreateMap<CourseDetailResponseParentRegion, Comp.ParentRegion>();
        }
    }
}