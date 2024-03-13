using System.ComponentModel.DataAnnotations;

namespace DFC.CompositeInterfaceModels.FindACourseClient
{
    public enum LearningMethod
    {
        [Display(Name = "All", Order = 1)]
        All,

        [Display(Name = "Classroom based", Order = 2)]
        ClassroomBased,

        [Display(Name = "Online", Order = 3)]
        Online,

        [Display(Name = "Work based", Order = 4)]
        WorkBased,

        [Display(Name = "Blended learning", Order = 5)]
        BlendedLearning,
    }
}