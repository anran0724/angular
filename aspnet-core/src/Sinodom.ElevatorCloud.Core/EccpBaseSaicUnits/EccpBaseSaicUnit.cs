using Sinodom.ElevatorCloud.ECCPBaseAreas;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpBaseSaicUnits
{
    [Table("EccpBaseSaicUnits")]
    public class EccpBaseSaicUnit : FullAuditedEntity, IMayHaveTenant, IHasArea
    {
        public int? TenantId { get; set; }


        [Required]
        [StringLength(EccpBaseSaicUnitConsts.MaxNameLength, MinimumLength = EccpBaseSaicUnitConsts.MinNameLength)]
        public virtual string Name { get; set; }

        [Required]
        [StringLength(EccpBaseSaicUnitConsts.MaxAddressLength, MinimumLength = EccpBaseSaicUnitConsts.MinAddressLength)]
        public virtual string Address { get; set; }

        [Required]
        [StringLength(EccpBaseSaicUnitConsts.MaxTelephoneLength, MinimumLength = EccpBaseSaicUnitConsts.MinTelephoneLength)]
        public virtual string Telephone { get; set; }

        [StringLength(EccpBaseSaicUnitConsts.MaxSummaryLength, MinimumLength = EccpBaseSaicUnitConsts.MinSummaryLength)]
        public virtual string Summary { get; set; }

        public virtual double? Longitude { get; set; }

        public virtual double? Latitude { get; set; }


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