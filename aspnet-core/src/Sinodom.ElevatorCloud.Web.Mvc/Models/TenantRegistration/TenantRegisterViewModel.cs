using Sinodom.ElevatorCloud.Editions;
using Sinodom.ElevatorCloud.Editions.Dto;
using Sinodom.ElevatorCloud.Security;
using Sinodom.ElevatorCloud.MultiTenancy.Payments;
using Sinodom.ElevatorCloud.MultiTenancy.Payments.Dto;

namespace Sinodom.ElevatorCloud.Web.Models.TenantRegistration
{
    public class TenantRegisterViewModel
    {
        public PasswordComplexitySetting PasswordComplexitySetting { get; set; }

        public string TenancyName { get; set; }
        public string AdminEmailAddress { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string LegalPerson { get; set; }
        public int? EditionId { get; set; }

        public string PaymentId { get; set; }

        public SubscriptionPaymentGatewayType? Gateway { get; set; }

        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }

        public bool ShowPaymentExpireNotification()
        {
            return !string.IsNullOrEmpty(PaymentId);
        }
    }
}
