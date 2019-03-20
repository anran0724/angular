using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
    public class GetTaskInfoDto
    {
        public int ID { get; set; }
        public int LiftId { get; set; }
        public string CertificateNum { get; set; }
        public string LongitudeAndLatitude { get; set; }
        public string Source { get; set; }
        public string StatusName { get; set; }
    }
}
