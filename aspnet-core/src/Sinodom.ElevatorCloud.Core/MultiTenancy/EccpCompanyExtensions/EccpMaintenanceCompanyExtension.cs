namespace Sinodom.ElevatorCloud.MultiTenancy.CompanyExtensions
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Abp.Domain.Entities.Auditing;

    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;

    [Table("EccpMaintenanceCompanyExtensions")]
    public class EccpMaintenanceCompanyExtension : FullAuditedEntity
    {
        [Required]
        [StringLength(
            EccpMaintenanceCompanyExtensionConsts.MaxLegalPersonLength,
            MinimumLength = EccpMaintenanceCompanyExtensionConsts.MinLegalPersonLength)]
        public virtual string LegalPerson { get; set; }

        [Required]
        [StringLength(
            EccpMaintenanceCompanyExtensionConsts.MaxMobileLength,
            MinimumLength = EccpMaintenanceCompanyExtensionConsts.MinMobileLength)]
        public virtual string Mobile { get; set; }

        public virtual int MaintenanceCompanyId { get; set; }

        public ECCPBaseMaintenanceCompany MaintenanceCompany { get; set; }

        public virtual Guid? BusinessLicenseId { get; set; }

        public virtual Guid? AptitudePhotoId { get; set; }

        public virtual bool IsMember { get; set; }

        public virtual int? SyncCompanyId { get; set; }
    }
}