
using Sinodom.ElevatorCloud.ECCPBaseAreas;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.ECCPBasePropertyCompanies
{
	[Table("ECCPBasePropertyCompanies")]
    public class ECCPBasePropertyCompany : FullAuditedEntity, IMayHaveTenant
    {



		[Required]
		[StringLength(ECCPBasePropertyCompanyConsts.MaxNameLength, MinimumLength = ECCPBasePropertyCompanyConsts.MinNameLength)]
		public virtual string Name { get; set; }
		
		[Required]
		[StringLength(ECCPBasePropertyCompanyConsts.MaxAddresseLength, MinimumLength = ECCPBasePropertyCompanyConsts.MinAddresseLength)]
		public virtual string Addresse { get; set; }
		
		[Required]
		[StringLength(ECCPBasePropertyCompanyConsts.MaxLongitudeLength, MinimumLength = ECCPBasePropertyCompanyConsts.MinLongitudeLength)]
		public virtual string Longitude { get; set; }
		
		[Required]
		[StringLength(ECCPBasePropertyCompanyConsts.MaxLatitudeLength, MinimumLength = ECCPBasePropertyCompanyConsts.MinLatitudeLength)]
		public virtual string Latitude { get; set; }
		
		[Required]
		[StringLength(ECCPBasePropertyCompanyConsts.MaxTelephoneLength, MinimumLength = ECCPBasePropertyCompanyConsts.MinTelephoneLength)]
		public virtual string Telephone { get; set; }
		
		public virtual string Summary { get; set; }
        
        public virtual bool? IsAudit { get; set; }

        public virtual int? ProvinceId { get; set; }
		public ECCPBaseArea Province { get; set; }
		
		public virtual int? CityId { get; set; }
		public ECCPBaseArea City { get; set; }
		
		public virtual int? DistrictId { get; set; }
		public ECCPBaseArea District { get; set; }
		
		public virtual int? StreetId { get; set; }
		public ECCPBaseArea Street { get; set; }

        [Required]
        [StringLength(ECCPBasePropertyCompanyConsts.MaxOrgOrganizationalCodeLength, MinimumLength = ECCPBasePropertyCompanyConsts.MinOrgOrganizationalCodeLength)]
        public virtual string OrgOrganizationalCode { get; set; }

        public int? TenantId { get; set; }
    }
}