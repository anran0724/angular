

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpDict
{
	[Table("ECCPDictElevatorStatuses")]
    public class ECCPDictElevatorStatus : FullAuditedEntity 
    {



		[Required]
		[StringLength(ECCPDictElevatorStatusConsts.MaxNameLength, MinimumLength = ECCPDictElevatorStatusConsts.MinNameLength)]
		public virtual string Name { get; set; }
		
		[StringLength(ECCPDictElevatorStatusConsts.MaxColorStyleLength, MinimumLength = ECCPDictElevatorStatusConsts.MinColorStyleLength)]
		public virtual string ColorStyle { get; set; }
		

    }
}