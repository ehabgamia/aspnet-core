using System.ComponentModel.DataAnnotations;

namespace MCV.Portal.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
