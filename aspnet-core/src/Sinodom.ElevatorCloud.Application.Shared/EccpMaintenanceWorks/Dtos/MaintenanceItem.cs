// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos
{
    public class MaintenanceItem
    {
        public  Guid MaintenanceWorkFlowId { get; set; }
        public virtual string Name { get; set; }
        public virtual string TermCode { get; set; }
        public virtual int DisOrder { get; set; }
    }


}