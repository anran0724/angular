using Abp.Application.Editions;

namespace Sinodom.ElevatorCloud.Editions
{
    public class ECCPEdition : Edition
    {
        public int? ECCPEditionsTypeId { get; set; }

        public ECCPEditionsType ECCPEditionsType { get; set; }

        public bool IsRegister { get; set; }
    }
}