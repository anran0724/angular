
using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.ECCPBasePropertyCompanies
{
	[Table("EccpPropertyCompanyChangeLogs")]
    public class EccpPropertyCompanyChangeLog : FullAuditedEntity 
    {



		[Required]
		[StringLength(EccpPropertyCompanyChangeLogConsts.MaxFieldNameLength, MinimumLength = EccpPropertyCompanyChangeLogConsts.MinFieldNameLength)]
		public virtual string FieldName { get; set; }
		
		[Required]
		[StringLength(EccpPropertyCompanyChangeLogConsts.MaxOldValueLength, MinimumLength = EccpPropertyCompanyChangeLogConsts.MinOldValueLength)]
		public virtual string OldValue { get; set; }
		
		[Required]
		[StringLength(EccpPropertyCompanyChangeLogConsts.MaxNewValueLength, MinimumLength = EccpPropertyCompanyChangeLogConsts.MinNewValueLength)]
		public virtual string NewValue { get; set; }
		

		public virtual int PropertyCompanyId { get; set; }
		public ECCPBasePropertyCompany PropertyCompany { get; set; }
		
    }
}