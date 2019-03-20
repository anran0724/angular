
using Sinodom.ElevatorCloud.EccpBaseElevatorBrands;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpBaseElevatorModels
{
	[Table("EccpBaseElevatorModels")]
    public class EccpBaseElevatorModel : FullAuditedEntity 
    {



		[Required]
		[StringLength(EccpBaseElevatorModelConsts.MaxNameLength, MinimumLength = EccpBaseElevatorModelConsts.MinNameLength)]
		public virtual string Name { get; set; }
		

		public virtual int ElevatorBrandId { get; set; }
		public EccpBaseElevatorBrand ElevatorBrand { get; set; }
		
    }
}