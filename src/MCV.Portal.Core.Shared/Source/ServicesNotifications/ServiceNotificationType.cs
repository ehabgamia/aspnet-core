using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MCV.Portal.Source.ServicesNotifications
{
    public enum ServiceNotificationType : byte
    {
        [Display(Name = "Info")]
        Info,
        [Display(Name = "Action")]
        Action,
        [Display(Name = "Success")]
        Success,
        [Display(Name = "Warning")]
        Warning,
        [Display(Name = "Error")]
        Error
    }
}
