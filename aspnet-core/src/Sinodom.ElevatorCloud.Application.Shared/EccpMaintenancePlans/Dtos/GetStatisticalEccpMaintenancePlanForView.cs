
using Sinodom.ElevatorCloud.ECCPBaseAreas.Dtos;
using System.Collections.Generic;
using System.Linq;


namespace Sinodom.ElevatorCloud.EccpMaintenancePlans.Dtos
{
   public  class GetStatisticalEccpMaintenancePlanForView
    {
        /// <summary>
        /// Gets or sets the eccp base elevator num.
        /// </summary>
        public int EccpBaseElevatorNum { get; set; }

        public List<string> EccpBaseElevatorNameList { get; set; }

        public IQueryable<MaintenanceUser> MaintenanceUserNameList { get; set; }

        /// <summary>
        /// Gets or sets the eccp maintenance plan.
        /// </summary>
        public EccpMaintenancePlanDto EccpMaintenancePlan { get; set; }
    }
  
}
