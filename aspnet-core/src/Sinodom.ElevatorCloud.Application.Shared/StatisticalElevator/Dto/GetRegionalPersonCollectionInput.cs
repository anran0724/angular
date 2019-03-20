using System;
using System.Collections.Generic;
using System.Text;

namespace Sinodom.ElevatorCloud.StatisticalElevator.Dto
{
    public class GetRegionalPersonCollectionInput
    {
        public string TopLeft { get; set; }

        public string BottomRight { get; set; }

        public int ECCPBaseMaintenanceCompaniesId { get; set; }

        /// <summary>
        /// 1省, 2市, 3区, 4园区  
        /// </summary>
        public int Type { get; set; }

        public string UserName { get; set; }

    }
}
