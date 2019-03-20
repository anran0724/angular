using Abp.Application.Services.Dto;
using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
using System.ComponentModel.DataAnnotations;

namespace Sinodom.ElevatorCloud.Common.Dto
{
    /// <summary>
    /// 部门
    /// </summary>
    public class SyncDeptInput : FullAuditedEntityDto<int?>
    {
        [Required]
        [StringLength(ECCPBaseMaintenanceCompanyConsts.MaxNameLength, MinimumLength = ECCPBaseMaintenanceCompanyConsts.MinNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(ECCPBaseMaintenanceCompanyConsts.MaxAddresseLength, MinimumLength = ECCPBaseMaintenanceCompanyConsts.MinAddresseLength)]
        public string Addresse { get; set; }

        [Required]
        [StringLength(ECCPBaseMaintenanceCompanyConsts.MaxLongitudeLength, MinimumLength = ECCPBaseMaintenanceCompanyConsts.MinLongitudeLength)]
        public string Longitude { get; set; }

        [Required]
        [StringLength(ECCPBaseMaintenanceCompanyConsts.MaxLatitudeLength, MinimumLength = ECCPBaseMaintenanceCompanyConsts.MinLatitudeLength)]
        public string Latitude { get; set; }

        [Required]
        [StringLength(ECCPBaseMaintenanceCompanyConsts.MaxTelephoneLength, MinimumLength = ECCPBaseMaintenanceCompanyConsts.MinTelephoneLength)]
        public string Telephone { get; set; }

        public string Summary { get; set; }

        public int? ProvinceId { get; set; }

        public int? CityId { get; set; }

        public int? DistrictId { get; set; }

        public int? StreetId { get; set; }

        public string OrgOrganizationalCode { get; set; }

        public int RoleGroupId { get; set; }
    }

}
