using Sinodom.ElevatorCloud.Editions.Dto;

namespace Sinodom.ElevatorCloud.MultiTenancy.Payments.Dto
{
    public class PaymentInfoDto
    {
        public EditionSelectDto Edition { get; set; }

        public decimal AdditionalPrice { get; set; }
    }
}
