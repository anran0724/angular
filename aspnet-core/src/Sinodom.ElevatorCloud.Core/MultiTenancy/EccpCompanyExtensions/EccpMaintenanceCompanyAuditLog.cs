namespace Sinodom.ElevatorCloud.MultiTenancy.CompanyExtensions
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Abp.Domain.Entities.Auditing;

    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;

    [Table("EccpMaintenanceCompanyAuditLogs")]
    public class EccpMaintenanceCompanyAuditLog : FullAuditedEntity
    {
        public virtual bool CheckState { get; set; }

        [Required]
        [StringLength(EccpMaintenanceCompanyAuditLogConsts.MaxRemarksLength, MinimumLength = EccpMaintenanceCompanyAuditLogConsts.MinRemarksLength)]
        public virtual string Remarks { get; set; }

        public virtual int MaintenanceCompanyId { get; set; }

        public ECCPBaseMaintenanceCompany MaintenanceCompany { get; set; }
    }
}