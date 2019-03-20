using System.Threading.Tasks;

namespace Sinodom.ElevatorCloud.Security.Recaptcha
{
    public interface IRecaptchaValidator
    {
        Task ValidateAsync(string captchaResponse);
    }
}
