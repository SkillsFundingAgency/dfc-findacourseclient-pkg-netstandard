using DFC.FindACourseClientV2.Models.APIResponses.CourseGet.Enums;
using DFC.FindACourseClientV2.Models.ExternalInterfaceModels.Enums;
using System.Collections.Generic;
using System.Linq;

namespace DFC.FindACourseClientV2.Extensions
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

        //public static List<string> GetTribalAttendanceModes(this CourseType courseType)
        //{
        //    switch (courseType)
        //    {
        //        case CourseType.ClassroomBased:
        //            return ClassAttendanceModes();

        //        case CourseType.DistanceLearning:
        //            return DistantAttendanceModes();

        //        case CourseType.Online:
        //            return OnlineAttendanceModes();

        //        case CourseType.WorkBased:
        //            return WorkAttendanceModes();

        //        case CourseType.All:
        //        default:
        //            return AllAttendanceModes();
        //    }
        //}

        //private static List<string> AllAttendanceModes() =>
        //    new List<string>
        //    {
        //        "AM1", //Location / campus
        //        "AM2", //Face-to-face(non-campus)
        //        "AM3", //Work-based
        //        "AM4", //Mixed Mode
        //        "AM5", //Distance with attendance
        //        "AM6", //Distance without attendance
        //        "AM7", //Online without attendance
        //        "AM8", //Online with attendance
        //        "AM9", //Not known
        //    };

        //private static List<string> ClassAttendanceModes() =>
        //    new List<string>
        //    {
        //        "AM1", //Location / campus
        //        "AM2", //Face-to-face(non-campus)
        //        "AM4", //Mixed Mode
        //        "AM9", //Not known
        //    };

        //private static List<string> OnlineAttendanceModes() =>
        //    new List<string>
        //    {
        //        "AM4", //Mixed Mode
        //        "AM7", //Online without attendance
        //        "AM8", //Online with attendance
        //        "AM9", //Not known
        //    };

        //private static List<string> DistantAttendanceModes() =>
        //    new List<string>
        //    {
        //        "AM4", //Mixed Mode
        //        "AM5", //Distance with attendance
        //        "AM6", //Distance without attendance
        //        "AM9", //Not known
        //    };

        //private static List<string> WorkAttendanceModes() =>
        //    new List<string>
        //    {
        //        "AM3", //Work-based
        //        "AM4", //Mixed Mode
        //        "AM6", //Distance without attendance
        //        "AM7", //Online without attendance
        //        "AM9", //Not known
        //    };
    }
}