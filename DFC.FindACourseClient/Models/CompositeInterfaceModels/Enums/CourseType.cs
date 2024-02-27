using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DFC.FindACourseClient.Models.CompositeInterfaceModels.Enums
{
    public enum CourseType
    {
        [Display(Name = "Essential Skills", Order = 1)]
        EssentialSkills,

        [Display(Name = "T-levels", Order = 2)]
        TLevels,

        [Display(Name = "HTQs", Order = 3)]
        HTQs,

        [Display(Name = "Free courses for Jobs", Order = 4)]
        FreeCoursesForJobs,

        [Display(Name = "Multiply", Order = 5)]
        Multiply,

        [Display(Name = "Skills Bootcamps", Order = 5)]
        SkillsBootcamps,
    }
}
