using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DFC.CompositeInterfaceModels.FindACourseClient
{
    public enum CourseType
    {
        [Display(Name = "All", Order = 1)]
        All,

        [Display(Name = "Essential Skills", Order = 2)]
        EssentialSkills,

        [Display(Name = "T-levels", Order = 3)]
        TLevels,

        [Display(Name = "HTQs", Order = 4)]
        HTQs,

        [Display(Name = "Free courses for Jobs", Order = 5)]
        FreeCoursesForJobs,

        [Display(Name = "Multiply", Order = 6)]
        Multiply,

        [Display(Name = "Skills Bootcamp", Order = 7)]
        SkillsBootcamp,
    }
}
