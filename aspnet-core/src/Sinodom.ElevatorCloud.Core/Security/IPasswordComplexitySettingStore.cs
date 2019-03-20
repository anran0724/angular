using System.Threading.Tasks;

namespace Sinodom.ElevatorCloud.Security
{
    public interface IPasswordComplexitySettingStore
    {
        Task<PasswordComplexitySetting> GetSettingsAsync();
    }
}
