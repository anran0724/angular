
using Sinodom.ElevatorCloud.ECCPBaseAreas;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits
{
	[Table("ECCPBaseAnnualInspectionUnits")]
    public class ECCPBaseAnnualInspectionUnit : FullAuditedEntity<long> 
    {



		[Required]
		[StringLength(ECCPBaseAnnualInspectionUnitConsts.MaxNameLength, MinimumLength = ECCPBaseAnnualInspectionUnitConsts.MinNameLength)]
		public virtual string Name { get; set; }
		
		[Required]
		[StringLength(ECCPBaseAnnualInspectionUnitConsts.MaxAddresseLength, MinimumLength = ECCPBaseAnnualInspectionUnitConsts.MinAddresseLength)]
		public virtual string Addresse { get; set; }
		
		[Required]
		[StringLength(ECCPBaseAnnualInspectionUnitConsts.MaxTelephoneLength, MinimumLength = ECCPBaseAnnualInspectionUnitConsts.MinTelephoneLength)]
		public virtual string Telephone { get; set; }
		
		public virtual string Summary { get; set; }
		

		public virtual int? ProvinceId { get; set; }
		public ECCPBaseArea Province { get; set; }
		
		public virtual int? CityId { get; set; }
		public ECCPBaseArea City { get; set; }
		
		public virtual int? DistrictId { get; set; }
		public ECCPBaseArea District { get; set; }
		
		public virtual int? StreetId { get; set; }
		public ECCPBaseArea Street { get; set; }
		
    }
}