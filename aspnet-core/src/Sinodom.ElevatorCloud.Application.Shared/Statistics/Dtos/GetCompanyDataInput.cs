using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.Statistics.Dtos
{
    public class GetCompanyDataInput
    {
        public int StateId { get; set; }
        public string CertificateNum { get; set; }
        public string LiftIds { get; set; }
    }
}
