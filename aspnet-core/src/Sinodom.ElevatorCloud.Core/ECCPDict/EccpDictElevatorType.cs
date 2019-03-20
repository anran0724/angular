

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpDict
{
	[Table("EccpDictElevatorTypes")]
    public class EccpDictElevatorType : FullAuditedEntity 
    {



		[Required]
		[StringLength(EccpDictElevatorTypeConsts.MaxNameLength, MinimumLength = EccpDictElevatorTypeConsts.MinNameLength)]
		public virtual string Name { get; set; }
		

    }
}