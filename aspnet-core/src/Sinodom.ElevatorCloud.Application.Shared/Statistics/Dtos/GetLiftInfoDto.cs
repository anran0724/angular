using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
    public class GetLiftInfoDto
    {
        public int LiftId { get; set; }
        public Guid? ElevatorId { get; set; }
        public string CertificateNum { get; set; }
        public string LongitudeAndLatitude { get; set; }
        public List<GetAdviceInfoDto> AdviceInfos { get; set; }
    }
}
