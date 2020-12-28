using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MCV.Portal.Source.Vacations
{
    public enum VacationStatus : byte
    {
        [Display(Name = "Cancelled")]
        Cancelled,
        [Display(Name = "Done")]
        Done,
        [Display(Name = "Manager Approved")]
        ManagerApproved,
        [Display(Name = "Manager Reject")]
        ManagerReject,
        [Display(Name = "Pending For Manager")]
        PendingForManager,
        [Display(Name = "Pending For Personnel")]
        PendingForPersonnel,
        [Display(Name = "Split")]
        Split
    }
}
