using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
    public class GetEquipmentLiftInfoDto
    {
        public int LiftId { get; set; }
        public Guid? ElevatorId { get; set; }
        public string CertificateNum { get; set; }
        public string LongitudeAndLatitude { get; set; }
        public List<GetEquipmentInfoDto> EquipmentInfos { get; set; }
    }
}
