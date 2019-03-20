
using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies
{
	[Table("EccpMaintenanceCompanyChangeLogs")]
    public class EccpMaintenanceCompanyChangeLog : FullAuditedEntity 
    {



		[Required]
		[StringLength(EccpMaintenanceCompanyChangeLogConsts.MaxFieldNameLength, MinimumLength = EccpMaintenanceCompanyChangeLogConsts.MinFieldNameLength)]
		public virtual string FieldName { get; set; }
		
		[Required]
		[StringLength(EccpMaintenanceCompanyChangeLogConsts.MaxOldValueLength, MinimumLength = EccpMaintenanceCompanyChangeLogConsts.MinOldValueLength)]
		public virtual string OldValue { get; set; }
		
		[Required]
		[StringLength(EccpMaintenanceCompanyChangeLogConsts.MaxNewValueLength, MinimumLength = EccpMaintenanceCompanyChangeLogConsts.MinNewValueLength)]
		public virtual string NewValue { get; set; }
		

		public virtual int MaintenanceCompanyId { get; set; }
		public ECCPBaseMaintenanceCompany MaintenanceCompany { get; set; }
		
    }
}