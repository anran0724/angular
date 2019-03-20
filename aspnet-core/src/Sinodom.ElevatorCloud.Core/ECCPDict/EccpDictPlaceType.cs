

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpDict
{
	[Table("EccpDictPlaceTypes")]
    public class EccpDictPlaceType : FullAuditedEntity 
    {



		[Required]
		[StringLength(EccpDictPlaceTypeConsts.MaxNameLength, MinimumLength = EccpDictPlaceTypeConsts.MinNameLength)]
		public virtual string Name { get; set; }
		

    }
}