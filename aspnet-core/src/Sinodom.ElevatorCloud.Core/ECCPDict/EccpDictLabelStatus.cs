

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpDict
{
	[Table("EccpDictLabelStatuses")]
    public class EccpDictLabelStatus : FullAuditedEntity 
    {



		[Required]
		[StringLength(EccpDictLabelStatusConsts.MaxNameLength, MinimumLength = EccpDictLabelStatusConsts.MinNameLength)]
		public virtual string Name { get; set; }
		

    }
}