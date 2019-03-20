
using Sinodom.ElevatorCloud.ECCPBaseProductionCompanies;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpBaseElevatorBrands
{
	[Table("EccpBaseElevatorBrands")]
    public class EccpBaseElevatorBrand : FullAuditedEntity 
    {



		[Required]
		[StringLength(EccpBaseElevatorBrandConsts.MaxNameLength, MinimumLength = EccpBaseElevatorBrandConsts.MinNameLength)]
		public virtual string Name { get; set; }
		

		public virtual long ProductionCompanyId { get; set; }
		public ECCPBaseProductionCompany ProductionCompany { get; set; }
		
    }
}