using DFC.FindACourseClient.Models.APIResponses.CourseGet.Enums;
using DFC.FindACourseClient.Models.ExternalInterfaceModels.Enums;
using System.Collections.Generic;
using System.Linq;

namespace DFC.FindACourseClient.Extensions
{
    public static class CourseTypeExtensions
    {
        public static List<DeliveryMode> MapToDeliveryModes(this List<CourseType> courseTypes)
        {
            var result = new List<DeliveryMode>();

            foreach (var courseType in courseTypes)
            {
                switch (courseType)
                {
                    case CourseType.ClassroomBased:
                        result.Add(DeliveryMode.ClassroomBased);
                        break;

                    case CourseType.DistanceLearning: // Equivalent doesnt exist in DeliveryMode
                        result.Add(DeliveryMode.Undefined);
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
            }

            return result.Distinct().ToList();
        }
    }
}