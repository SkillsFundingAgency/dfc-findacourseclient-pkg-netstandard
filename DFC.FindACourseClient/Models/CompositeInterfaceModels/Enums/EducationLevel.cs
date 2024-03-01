using System.ComponentModel.DataAnnotations;

namespace DFC.CompositeInterfaceModels.FindACourseClient
{
    public enum EducationLevel
    {
        [Display(Name = "All", Order = 1)]
        All = -1,

        [Display(Name = "Entry Level", Order = 2)]
        EntryLevel,

        [Display(Name = "One", Order = 3)]
        One,

        [Display(Name = "Two", Order = 4)]
        Two,

        [Display(Name = "Three", Order = 5)]
        Three,

        [Display(Name = "Four", Order = 6)]
        Four,

        [Display(Name = "Five", Order = 7)]
        Five,

        [Display(Name = "Six", Order = 8)]
        Six,

        [Display(Name = "Seven", Order = 9)]
        Seven,
    }
}