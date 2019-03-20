
using Sinodom.ElevatorCloud.EccpBaseElevators;
using Sinodom.ElevatorCloud.EccpDict;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpBaseElevatorLabels
{
	[Table("EccpBaseElevatorLabelBindLogs")]
    public class EccpBaseElevatorLabelBindLog : FullAuditedEntity<long> 
    {



		[Required]
		[StringLength(EccpBaseElevatorLabelBindLogConsts.MaxLabelNameLength, MinimumLength = EccpBaseElevatorLabelBindLogConsts.MinLabelNameLength)]
		public virtual string LabelName { get; set; }
		
		[StringLength(EccpBaseElevatorLabelBindLogConsts.MaxLocalInformationLength, MinimumLength = EccpBaseElevatorLabelBindLogConsts.MinLocalInformationLength)]
		public virtual string LocalInformation { get; set; }
		
		public virtual DateTime? BindingTime { get; set; }
		
		public virtual Guid BinaryObjectsId { get; set; }
		
		[StringLength(EccpBaseElevatorLabelBindLogConsts.MaxRemarkLength, MinimumLength = EccpBaseElevatorLabelBindLogConsts.MinRemarkLength)]
		public virtual string Remark { get; set; }
		
		public virtual long ElevatorLabelId { get; set; }
		

		public virtual Guid? ElevatorId { get; set; }
		public EccpBaseElevator Elevator { get; set; }
		
		public virtual int LabelStatusId { get; set; }
		public EccpDictLabelStatus LabelStatus { get; set; }
		
    }
}