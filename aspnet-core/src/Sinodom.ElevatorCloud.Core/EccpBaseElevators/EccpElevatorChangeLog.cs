
using Sinodom.ElevatorCloud.EccpBaseElevators;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpBaseElevators
{
	[Table("EccpElevatorChangeLogs")]
    public class EccpElevatorChangeLog : FullAuditedEntity 
    {



		[Required]
		[StringLength(EccpElevatorChangeLogConsts.MaxFieldNameLength, MinimumLength = EccpElevatorChangeLogConsts.MinFieldNameLength)]
		public virtual string FieldName { get; set; }
		
		[Required]
		[StringLength(EccpElevatorChangeLogConsts.MaxOldValueLength, MinimumLength = EccpElevatorChangeLogConsts.MinOldValueLength)]
		public virtual string OldValue { get; set; }
		
		[Required]
		[StringLength(EccpElevatorChangeLogConsts.MaxNewValueLength, MinimumLength = EccpElevatorChangeLogConsts.MinNewValueLength)]
		public virtual string NewValue { get; set; }
		

		public virtual Guid ElevatorId { get; set; }
		public EccpBaseElevator Elevator { get; set; }
		
    }
}