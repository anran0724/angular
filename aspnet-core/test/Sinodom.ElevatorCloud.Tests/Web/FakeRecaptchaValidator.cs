using System.Threading.Tasks;
using Sinodom.ElevatorCloud.Security.Recaptcha;

namespace Sinodom.ElevatorCloud.Tests.Web
{
    public class FakeRecaptchaValidator : IRecaptchaValidator
    {
        public Task ValidateAsync(string captchaResponse)
        {
            return Task.CompletedTask;
        }
    }
}
