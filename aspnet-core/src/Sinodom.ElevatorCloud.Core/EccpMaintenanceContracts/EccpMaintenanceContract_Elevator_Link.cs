namespace Sinodom.ElevatorCloud.EccpMaintenanceContracts
{
    using System;

    using Abp.Domain.Entities;
    using Abp.Domain.Entities.Auditing;

    using Sinodom.ElevatorCloud.EccpBaseElevators;

    public class EccpMaintenanceContract_Elevator_Link : FullAuditedEntity<long>
    {
        public virtual long? MaintenanceContractId { get; set; }
        public EccpMaintenanceContract MaintenanceContract { get; set; }

        public virtual Guid? ElevatorId { get; set; }
        public EccpBaseElevator Elevator { get; set; }
    }
}