using System.ComponentModel.DataAnnotations;

namespace MCV.Portal.Authorization.Accounts.Dto
{
    public class SendEmailActivationLinkInput
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}