using System.Collections.Generic;
using System.Linq;
using Comp = DFC.CompositeInterfaceModels.FindACourseClient;

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

        internal static List<DeliveryMode> MapToCompositeDeliveryModes(this IList<Comp.CourseType> courseTypes)
        {
            var deliveryModeList = new List<DeliveryMode>();

            foreach (var item in courseTypes)
            {
                switch (item)
                {
                    case Comp.CourseType.ClassroomBased:
                        deliveryModeList.Add(DeliveryMode.ClassroomBased);
                        break;

                    case Comp.CourseType.Online:
                        deliveryModeList.Add(DeliveryMode.Online);
                        break;

                    case Comp.CourseType.WorkBased:
                        deliveryModeList.Add(DeliveryMode.WorkBased);
                        break;

                    case Comp.CourseType.All:
                    default:
                        deliveryModeList.Add(DeliveryMode.ClassroomBased);
                        deliveryModeList.Add(DeliveryMode.WorkBased);
                        deliveryModeList.Add(DeliveryMode.Online);
                        deliveryModeList.Add(DeliveryMode.Undefined);
                        break;
                }
            }

            return deliveryModeList.Distinct().ToList();
        }
    }
}