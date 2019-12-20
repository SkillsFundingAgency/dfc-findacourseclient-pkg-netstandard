using System.Collections.Generic;
using System.Linq;

namespace DFC.FindACourseClient
{
    internal static class CourseTypeExtensions
    {
        internal static List<DeliveryMode> MapToDeliveryModes(this CourseType courseTypes)
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