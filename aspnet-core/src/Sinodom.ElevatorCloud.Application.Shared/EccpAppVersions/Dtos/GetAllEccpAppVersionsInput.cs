using Abp.Application.Services.Dto;
using System;

namespace Sinodom.ElevatorCloud.EccpAppVersions.Dtos
{
    public class GetAllEccpAppVersionsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}