namespace Sinodom.ElevatorCloud.EccpBaseElevators
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Abp.Domain.Entities;
    using Abp.Domain.Entities.Auditing;

    [Table("EccpBaseElevatorSubsidiaryInfos")]
    public class EccpBaseElevatorSubsidiaryInfo : FullAuditedEntity<Guid>
    {
        [StringLength(EccpBaseElevatorSubsidiaryInfoConsts.MaxCustomNumLength, MinimumLength = EccpBaseElevatorSubsidiaryInfoConsts.MinCustomNumLength)]
        public virtual string CustomNum { get; set; }

        [StringLength(EccpBaseElevatorSubsidiaryInfoConsts.MaxManufacturingLicenseNumberLength, MinimumLength = EccpBaseElevatorSubsidiaryInfoConsts.MinManufacturingLicenseNumberLength)]
        public virtual string ManufacturingLicenseNumber { get; set; }

        public virtual int? FloorNumber { get; set; }

        public virtual int? GateNumber { get; set; }

        public virtual double? RatedSpeed { get; set; }

        public virtual double? Deadweight { get; set; }

        public virtual Guid? ElevatorId { get; set; }

        public EccpBaseElevator Elevator { get; set; }
    }
}