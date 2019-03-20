

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpDict
{
	[Table("EccpDictNodeTypes")]
    public class EccpDictNodeType : FullAuditedEntity 
    {



		[Required]
		[StringLength(EccpDictNodeTypeConsts.MaxNameLength, MinimumLength = EccpDictNodeTypeConsts.MinNameLength)]
		public virtual string Name { get; set; }
		

    }
}