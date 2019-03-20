
using Sinodom.ElevatorCloud.EccpBaseElevators;
using Sinodom.ElevatorCloud.EccpDict;
using Sinodom.ElevatorCloud.Authorization.Users;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpBaseElevatorLabels
{
	[Table("EccpBaseElevatorLabels")]
    public class EccpBaseElevatorLabel : FullAuditedEntity<long> , IMayHaveTenant
    {
		public int? TenantId { get; set; }


		[Required]
		[StringLength(EccpBaseElevatorLabelConsts.MaxLabelNameLength, MinimumLength = EccpBaseElevatorLabelConsts.MinLabelNameLength)]
		public virtual string LabelName { get; set; }
		
		[Required]
		[StringLength(EccpBaseElevatorLabelConsts.MaxUniqueIdLength, MinimumLength = EccpBaseElevatorLabelConsts.MinUniqueIdLength)]
		public virtual string UniqueId { get; set; }
		
		[StringLength(EccpBaseElevatorLabelConsts.MaxLocalInformationLength, MinimumLength = EccpBaseElevatorLabelConsts.MinLocalInformationLength)]
		public virtual string LocalInformation { get; set; }
		
		public virtual DateTime? BindingTime { get; set; }
		
		public virtual Guid BinaryObjectsId { get; set; }
		

		public virtual Guid? ElevatorId { get; set; }
		public EccpBaseElevator Elevator { get; set; }
		
		public virtual int LabelStatusId { get; set; }
		public EccpDictLabelStatus LabelStatus { get; set; }
		
		public virtual long? UserId { get; set; }
		public User User { get; set; }
		
    }
}