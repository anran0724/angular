namespace Sinodom.ElevatorCloud.EccpElevatorQrCodes
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using Abp.Domain.Entities;
    using Abp.Domain.Entities.Auditing;

    using Sinodom.ElevatorCloud.EccpBaseElevators;

    [Table("EccpElevatorQrCodeBindLogs")]
    public class EccpElevatorQrCodeBindLog : FullAuditedEntity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }


        public virtual Guid? OldElevatorId { get; set; }
        public EccpBaseElevator OldElevator { get; set; }


        public virtual Guid? NewElevatorId { get; set; }
        public EccpBaseElevator NewElevator { get; set; }


        public virtual Guid? OldQrCodeId { get; set; }
        public EccpElevatorQrCode OldQrCode { get; set; }


        public virtual Guid? NewQrCodeId { get; set; }
        public EccpElevatorQrCode NewQrCode { get; set; }

        public virtual string Remark { get; set; }
    }
}