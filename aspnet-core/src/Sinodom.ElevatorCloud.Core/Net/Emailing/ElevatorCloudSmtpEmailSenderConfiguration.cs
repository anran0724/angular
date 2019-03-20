using Abp.Configuration;
using Abp.Net.Mail;
using Abp.Net.Mail.Smtp;
using Abp.Runtime.Security;

namespace Sinodom.ElevatorCloud.Net.Emailing
{
    public class ElevatorCloudSmtpEmailSenderConfiguration : SmtpEmailSenderConfiguration
    {
        public ElevatorCloudSmtpEmailSenderConfiguration(ISettingManager settingManager) : base(settingManager)
        {

        }

        public override string Password => SimpleStringCipher.Instance.Decrypt(GetNotEmptySettingValue(EmailSettingNames.Smtp.Password));
    }
}