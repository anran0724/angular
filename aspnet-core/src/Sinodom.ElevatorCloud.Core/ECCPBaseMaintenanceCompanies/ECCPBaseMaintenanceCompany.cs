
using Sinodom.ElevatorCloud.ECCPBaseAreas;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies
{
    [Table("ECCPBaseMaintenanceCompanies")]
    public class ECCPBaseMaintenanceCompany : FullAuditedEntity, IMayHaveTenant
    {



        [Required]
        [StringLength(ECCPBaseMaintenanceCompanyConsts.MaxNameLength, MinimumLength = ECCPBaseMaintenanceCompanyConsts.MinNameLength)]
        public virtual string Name { get; set; }

        [Required]
        [StringLength(ECCPBaseMaintenanceCompanyConsts.MaxAddresseLength, MinimumLength = ECCPBaseMaintenanceCompanyConsts.MinAddresseLength)]
        public virtual string Addresse { get; set; }

        [Required]
        [StringLength(ECCPBaseMaintenanceCompanyConsts.MaxLongitudeLength, MinimumLength = ECCPBaseMaintenanceCompanyConsts.MinLongitudeLength)]
        public virtual string Longitude { get; set; }

        [Required]
        [StringLength(ECCPBaseMaintenanceCompanyConsts.MaxLatitudeLength, MinimumLength = ECCPBaseMaintenanceCompanyConsts.MinLatitudeLength)]
        public virtual string Latitude { get; set; }

        [Required]
        [StringLength(ECCPBaseMaintenanceCompanyConsts.MaxTelephoneLength, MinimumLength = ECCPBaseMaintenanceCompanyConsts.MinTelephoneLength)]
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
        [StringLength(ECCPBaseMaintenanceCompanyConsts.MaxOrgOrganizationalCodeLength, MinimumLength = ECCPBaseMaintenanceCompanyConsts.MinOrgOrganizationalCodeLength)]
        public virtual string OrgOrganizationalCode { get; set; }

        public int? TenantId { get; set; }
    }
}