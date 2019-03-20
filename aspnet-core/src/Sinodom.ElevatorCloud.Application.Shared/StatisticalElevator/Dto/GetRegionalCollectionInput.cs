using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.StatisticalElevator.Dto
{
    public class GetRegionalCollectionInput
    {
        public int CompaniesId { get; set; }      

        public int ECCPDictElevatorStatusId { get; set; }

        public int Level { get; set; }

        public string TopLeft { get; set; }

        public string BottomRight { get; set; }
        /// <summary>
        /// 1省, 2市, 3区, 4园区  
        /// </summary>
        public int Type { get; set; }

        public string CertificateNum { get; set; }
    }
}
