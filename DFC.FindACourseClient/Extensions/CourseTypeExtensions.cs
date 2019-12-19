using DFC.FindACourseClient.Models.APIResponses.CourseGet.Enums;
using DFC.FindACourseClient.Models.ExternalInterfaceModels.Enums;
using System.Collections.Generic;
using System.Linq;

namespace DFC.FindACourseClient.Extensions
{
    public static class CourseTypeExtensions
    {
        public static List<DeliveryMode> MapToDeliveryModes(this CourseType courseTypes)
        {
            var result = new List<DeliveryMode>();
            switch (courseTypes)
            {
                case CourseType.ClassroomBased:
                    result.Add(DeliveryMode.ClassroomBased);
                    break;

                case CourseType.Online:
                    result.Add(DeliveryMode.Online);
                    break;

                case CourseType.WorkBased:
                    result.Add(DeliveryMode.WorkBased);
                    break;

                case CourseType.All:
                default:
                    result.Add(DeliveryMode.ClassroomBased);
                    result.Add(DeliveryMode.WorkBased);
                    result.Add(DeliveryMode.Online);
                    result.Add(DeliveryMode.Undefined);
                    break;
            }

            return result.Distinct().ToList();
        }
    }
}