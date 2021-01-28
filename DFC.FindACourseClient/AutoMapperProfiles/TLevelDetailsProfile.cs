using AutoMapper;
using DFC.FindACourseClient.AutoMapperProfiles;
using DFC.FindACourseClient.Extensions;
using System.Runtime.CompilerServices;
using Comp = DFC.CompositeInterfaceModels.FindACourseClient;

[assembly: InternalsVisibleTo("DFC.FindACourseClientFramework.IntergrationTests")]

namespace DFC.FindACourseClient
{
    public class TLevelDetailsProfile : Profile
    {
        public TLevelDetailsProfile()
        {
            // TLevel Details
            CreateMap<TLevelDetailResponse, Comp.TLevelDetails>()
                .ForMember(d => d.TLevelId, s => s.MapFrom(f => f.TLevelId.ToString()))
                .ForMember(d => d.DeliveryMode, s => s.MapFrom(f => f.DeliveryMode.GetFriendlyName()))
                .ForMember(d => d.AttendancePattern, s => s.MapFrom(f => f.AttendancePattern.GetFriendlyName()))
                .ForMember(d => d.StudyMode, s => s.MapFrom(f => f.StudyMode.GetFriendlyName()))
                .ForMember(d => d.ProviderDetails, s => s.MapFrom(f => f))
                .ForMember(d => d.Venues, s => s.MapFrom(f => f.Locations))
                .ForMember(d => d.Duration, s => s.MapFrom(f => $"{f.DurationValue} {f.DurationUnit.ToString()}"));

            CreateMap<TLevelQualification, Comp.TLevelQualification>();

            CreateMap<TLevelDetailResponse, Comp.ProviderDetails>()
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
                .ForMember(d => d.Name, s => s.MapFrom(f => f.Provider.ProviderName))
                .ForMember(d => d.PhoneNumber, s => s.MapFrom(f => f.Provider.Telephone))
                .ForMember(d => d.PostCode, s => s.MapFrom(f => f.Provider.Postcode));

            CreateMap<TLevelLocation, Comp.Venue>()
            .ForMember(d => d.EmailAddress, s => s.MapFrom(f => f.Email))
            .ForMember(d => d.PhoneNumber, s => s.MapFrom(f => f.Telephone))
            .ForMember(d => d.Location, s => s.MapFrom(f => f));

            CreateMap<TLevelLocation, Comp.Address>()
            .ForMember(d => d.Longitude, s => s.MapFrom(f => f.Longitude.ToString()))
            .ForMember(d => d.Latitude, s => s.MapFrom(f => f.Latitude.ToString()));
        }
    }
}
