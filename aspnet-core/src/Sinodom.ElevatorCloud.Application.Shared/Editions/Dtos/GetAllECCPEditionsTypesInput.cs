using Abp.Application.Services.Dto;
using System;

namespace Sinodom.ElevatorCloud.Editions.Dtos
{
    public class GetAllECCPEditionsTypesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}