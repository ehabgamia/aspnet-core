using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace MCV.Portal.Source.VacationTypes
{
    public enum VacationUnit : byte
    {
        [Display(Name = "Day(s)")]
        Day,
        [Display(Name = "Hour(s)")]
        Hour,
        [Display(Name = "Minute(s)")]
        Minute
    }
}
