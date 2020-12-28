using System.ComponentModel.DataAnnotations;

namespace MCV.Portal.Web.Models.Account
{
    public class SendPasswordResetLinkViewModel
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}