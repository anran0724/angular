namespace Sinodom.ElevatorCloud.MultiTenancy.CompanyExtensions
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Abp.Domain.Entities.Auditing;
    
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;

    [Table("EccpPropertyCompanyAuditLogs")]
    public class EccpPropertyCompanyAuditLog : FullAuditedEntity
    {
        public virtual bool CheckState { get; set; }

        [Required]
        [StringLength(EccpPropertyCompanyAuditLogConsts.MaxRemarksLength, MinimumLength = EccpPropertyCompanyAuditLogConsts.MinRemarksLength)]
        public virtual string Remarks { get; set; }

        public virtual int PropertyCompanyId { get; set; }

        public ECCPBasePropertyCompany PropertyCompany { get; set; }

    }
}