using Abp.Application.Services.Dto;
using System;

namespace Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions.Dtos
{
    public class GetAllUserPathHistoriesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string PhoneIdFilter { get; set; }

		public double? MaxLongitudeFilter { get; set; }
		public double? MinLongitudeFilter { get; set; }

		public double? MaxLatitudeFilter { get; set; }
		public double? MinLatitudeFilter { get; set; }


		 public string UserNameFilter { get; set; }

		 
    }
}