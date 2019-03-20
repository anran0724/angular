using Abp.AutoMapper;
using Sinodom.ElevatorCloud.Editions;
using Sinodom.ElevatorCloud.MultiTenancy.Payments.Dto;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.SubscriptionManagement
{
    [AutoMapTo(typeof(ExecutePaymentDto))]
    public class PaymentResultViewModel : SubscriptionPaymentDto
    {
        public EditionPaymentType EditionPaymentType { get; set; }
    }
}
