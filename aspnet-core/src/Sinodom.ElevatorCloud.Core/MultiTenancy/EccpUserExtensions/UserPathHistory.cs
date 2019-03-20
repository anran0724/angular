using Sinodom.ElevatorCloud.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions
{
	[Table("UserPathHistories")]
    public class UserPathHistory : FullAuditedEntity<long> , IMustHaveTenant
    {
			public int TenantId { get; set; }
			

		[Required]
		[StringLength(UserPathHistoryConsts.MaxPhoneIdLength, MinimumLength = UserPathHistoryConsts.MinPhoneIdLength)]
		public virtual string PhoneId { get; set; }
		
		public virtual double Longitude { get; set; }
		
		public virtual double Latitude { get; set; }
		

		public virtual long UserId { get; set; }
		public User User { get; set; }
		
    }
}