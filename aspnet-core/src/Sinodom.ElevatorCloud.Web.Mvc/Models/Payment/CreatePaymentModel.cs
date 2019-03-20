using Sinodom.ElevatorCloud.Editions;
using Sinodom.ElevatorCloud.MultiTenancy.Payments;

namespace Sinodom.ElevatorCloud.Web.Models.Payment
{
    public class CreatePaymentModel
    {
        public int EditionId { get; set; }

        public PaymentPeriodType? PaymentPeriodType { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }

        public SubscriptionPaymentGatewayType Gateway { get; set; }
    }
}
