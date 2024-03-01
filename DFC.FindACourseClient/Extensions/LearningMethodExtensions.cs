using System.Collections.Generic;
using System.Linq;
using Comp = DFC.CompositeInterfaceModels.FindACourseClient;

namespace DFC.FindACourseClient
{
    internal static class LearningMethodExtensions
    {
        internal static List<DeliveryMode> MapToDeliveryModes(this LearningMethod learningMethods)
        {
            var result = new List<DeliveryMode>();
            switch (learningMethods)
            {
                case LearningMethod.BlendedLearning:
                    result.Add(DeliveryMode.BlendedLearning);
                    break;

                case LearningMethod.ClassroomBased:
                    result.Add(DeliveryMode.ClassroomBased);
                    break;

                case LearningMethod.Online:
                    result.Add(DeliveryMode.Online);
                    break;

                case LearningMethod.WorkBased:
                    result.Add(DeliveryMode.WorkBased);
                    break;

                case LearningMethod.All:
                default:
                    result.Add(DeliveryMode.BlendedLearning);
                    result.Add(DeliveryMode.ClassroomBased);
                    result.Add(DeliveryMode.WorkBased);
                    result.Add(DeliveryMode.Online);
                    result.Add(DeliveryMode.Undefined);
                    break;
            }

            return result.Distinct().ToList();
        }

        internal static List<DeliveryMode> MapToCompositeDeliveryModes(this IList<Comp.LearningMethod> learningMethods)
        {
            var deliveryModeList = new List<DeliveryMode>();

            foreach (var item in learningMethods)
            {
                switch (item)
                {
                    case Comp.LearningMethod.BlendedLearning:
                        deliveryModeList.Add(DeliveryMode.BlendedLearning);
                        break;

                    case Comp.LearningMethod.ClassroomBased:
                        deliveryModeList.Add(DeliveryMode.ClassroomBased);
                        break;

                    case Comp.LearningMethod.Online:
                        deliveryModeList.Add(DeliveryMode.Online);
                        break;

                    case Comp.LearningMethod.WorkBased:
                        deliveryModeList.Add(DeliveryMode.WorkBased);
                        break;

                    case Comp.LearningMethod.All:
                    default:
                        break;
                }
            }

            return deliveryModeList.Distinct().ToList();
        }
    }
}