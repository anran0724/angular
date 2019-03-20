

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpDict
{
	[Table("EccpDictTempWorkOrderTypes")]
    public class EccpDictTempWorkOrderType : FullAuditedEntity 
    {



		[Required]
		[StringLength(EccpDictTempWorkOrderTypeConsts.MaxNameLength, MinimumLength = EccpDictTempWorkOrderTypeConsts.MinNameLength)]
		public virtual string Name { get; set; }
		

    }
}