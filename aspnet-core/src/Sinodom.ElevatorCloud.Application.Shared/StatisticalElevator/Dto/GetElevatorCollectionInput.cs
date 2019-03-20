using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.StatisticalElevator.Dto
{
    public class GetElevatorCollectionInput
    {
        public string TopLeft { get; set; }

        public string BottomRight { get; set; }

        public int CompaniesId { get; set; }

        public string CertificateNum { get; set; }

        public int ECCPDictElevatorStatusId { get; set; }
    }
}
