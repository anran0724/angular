
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Sinodom.ElevatorCloud.EccpBaseSaicUnits.Dtos
{
    public class EditEccpBaseSaicUnitDto : EntityDto<int?>
    {

        [Required]
        [StringLength(EccpBaseSaicUnitConsts.MaxNameLength, MinimumLength = EccpBaseSaicUnitConsts.MinNameLength)]
        public string Name { get; set; }


        [Required]
        [StringLength(EccpBaseSaicUnitConsts.MaxAddressLength, MinimumLength = EccpBaseSaicUnitConsts.MinAddressLength)]
        public string Address { get; set; }


        [Required]
        [StringLength(EccpBaseSaicUnitConsts.MaxTelephoneLength, MinimumLength = EccpBaseSaicUnitConsts.MinTelephoneLength)]
        public string Telephone { get; set; }


        [StringLength(EccpBaseSaicUnitConsts.MaxSummaryLength, MinimumLength = EccpBaseSaicUnitConsts.MinSummaryLength)]
        public string Summary { get; set; }


        public double? Longitude { get; set; }


        public double? Latitude { get; set; }


        public int? ProvinceId { get; set; }

        public int? CityId { get; set; }

        public int? DistrictId { get; set; }

        public int? StreetId { get; set; }
    }
}