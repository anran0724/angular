
using Sinodom.ElevatorCloud.Authorization.Users;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions
{
	[Table("EccpCompanyUserAuditLogs")]
    public class EccpCompanyUserAuditLog : FullAuditedEntity 
    {



		public virtual bool CheckState { get; set; }
		
		[Required]
		[StringLength(EccpCompanyUserAuditLogConsts.MaxRemarksLength, MinimumLength = EccpCompanyUserAuditLogConsts.MinRemarksLength)]
		public virtual string Remarks { get; set; }
		

		public virtual long UserId { get; set; }
		public User User { get; set; }
		
    }
}