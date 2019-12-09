using System.ComponentModel.DataAnnotations;

namespace DFC.FindACourseClient.Models.ExternalInterfaceModels
{
    public enum StartDate
    {
        [Display(Name = "Anytime", Order = 1)]
        Anytime,

        [Display(Name = "From today", Order = 2)]
        FromToday,

        [Display(Name = "Select date from", Order = 3)]
        SelectDateFrom,
    }
}