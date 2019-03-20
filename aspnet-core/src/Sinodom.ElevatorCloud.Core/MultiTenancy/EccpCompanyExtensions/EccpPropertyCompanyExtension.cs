namespace Sinodom.ElevatorCloud.MultiTenancy.CompanyExtensions
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Abp.Domain.Entities.Auditing;

    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;

    [Table("EccpPropertyCompanyExtensions")]
    public class EccpPropertyCompanyExtension : FullAuditedEntity
    {
        [Required]
        [StringLength(EccpPropertyCompanyExtensionConsts.MaxLegalPersonLength, MinimumLength = EccpPropertyCompanyExtensionConsts.MinLegalPersonLength)]
        public virtual string LegalPerson { get; set; }

        [Required]
        [StringLength(EccpPropertyCompanyExtensionConsts.MaxMobileLength, MinimumLength = EccpPropertyCompanyExtensionConsts.MinMobileLength)]
        public virtual string Mobile { get; set; }

        public virtual int PropertyCompanyId { get; set; }

        public ECCPBasePropertyCompany PropertyCompany { get; set; }

        public virtual Guid? BusinessLicenseId { get; set; }

        public virtual Guid? AptitudePhotoId { get; set; }

        public virtual bool IsMember { get; set; }

        public virtual int? SyncCompanyId { get; set; }
    }
}