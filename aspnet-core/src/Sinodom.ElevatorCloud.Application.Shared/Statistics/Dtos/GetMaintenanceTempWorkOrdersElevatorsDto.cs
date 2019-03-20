using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
    public class GetMaintenanceTempWorkOrdersElevatorsDto
    {
        public Guid ElevatorId { get; set; }
        public string CertificateNum { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public List<GetMaintenanceTempWorkOrdersDto> MaintenanceTempWorkOrders { get; set; }
    }
}
