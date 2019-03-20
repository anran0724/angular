using Abp.Application.Services.Dto;
using System;

namespace Sinodom.ElevatorCloud.EccpBaseSaicUnits.Dtos
{
    public class GetAllEccpBaseSaicUnitsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string NameFilter { get; set; }

		public string AddressFilter { get; set; }

		public string TelephoneFilter { get; set; }


		 public string ECCPBaseAreaNameFilter { get; set; }

		 		 public string ECCPBaseAreaName2Filter { get; set; }

		 		 public string ECCPBaseAreaName3Filter { get; set; }

		 		 public string ECCPBaseAreaName4Filter { get; set; }

		 
    }
}