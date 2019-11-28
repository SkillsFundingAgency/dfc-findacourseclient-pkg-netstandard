﻿using System.ComponentModel.DataAnnotations;

namespace DFC.FindACourseClientV2.Models.ExternalInterfaceModels
{
    public enum CourseType
    {
        [Display(Name = "All", Order = 1)]
        All,

        [Display(Name = "Classroom based", Order = 2)]
        ClassroomBased,

        [Display(Name = "Distance learning", Order = 3)]
        DistanceLearning,

        [Display(Name = "Online", Order = 4)]
        Online,

        [Display(Name = "Work based", Order = 5)]
        WorkBased,
    }
}
