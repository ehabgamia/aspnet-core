using System.Threading.Tasks;
using MCV.Portal.Security.Recaptcha;

namespace MCV.Portal.Test.Base.Web
{
    public class FakeRecaptchaValidator : IRecaptchaValidator
    {
        public Task ValidateAsync(string captchaResponse)
        {
            return Task.CompletedTask;
        }
    }
}
