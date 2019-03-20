using Sinodom.ElevatorCloud.MultiTenancy.Payments;

namespace Sinodom.ElevatorCloud.Web.Models.Payment
{
    public class CancelPaymentModel
    {
        public string PaymentId { get; set; }

        public SubscriptionPaymentGatewayType Gateway { get; set; }
    }
}
