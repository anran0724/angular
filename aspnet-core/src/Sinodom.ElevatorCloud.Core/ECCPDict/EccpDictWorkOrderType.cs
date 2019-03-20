

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpDict
{
	[Table("EccpDictWorkOrderTypes")]
    public class EccpDictWorkOrderType : FullAuditedEntity 
    {



		[Required]
		[StringLength(EccpDictWorkOrderTypeConsts.MaxNameLength, MinimumLength = EccpDictWorkOrderTypeConsts.MinNameLength)]
		public virtual string Name { get; set; }
		

    }
}