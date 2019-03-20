
using System;
using Abp.Application.Services.Dto;

namespace Sinodom.ElevatorCloud.EccpBaseSaicUnits.Dtos
{
    public class EccpBaseSaicUnitDto : EntityDto
    {
		public string Name { get; set; }

		public string Address { get; set; }

		public string Telephone { get; set; }


		 public int? ProvinceId { get; set; }

		 		 public int? CityId { get; set; }

		 		 public int? DistrictId { get; set; }

		 		 public int? StreetId { get; set; }

		 
    }
}