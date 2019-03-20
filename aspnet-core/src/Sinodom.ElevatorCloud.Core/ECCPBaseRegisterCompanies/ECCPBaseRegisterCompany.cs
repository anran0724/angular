
using Sinodom.ElevatorCloud.ECCPBaseAreas;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies
{
	[Table("ECCPBaseRegisterCompanies")]
    public class ECCPBaseRegisterCompany : FullAuditedEntity<long> 
    {



		[Required]
		[StringLength(ECCPBaseRegisterCompanyConsts.MaxNameLength, MinimumLength = ECCPBaseRegisterCompanyConsts.MinNameLength)]
		public virtual string Name { get; set; }
		
		[Required]
		[StringLength(ECCPBaseRegisterCompanyConsts.MaxAddresseLength, MinimumLength = ECCPBaseRegisterCompanyConsts.MinAddresseLength)]
		public virtual string Addresse { get; set; }
		
		[Required]
		[StringLength(ECCPBaseRegisterCompanyConsts.MaxTelephoneLength, MinimumLength = ECCPBaseRegisterCompanyConsts.MinTelephoneLength)]
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