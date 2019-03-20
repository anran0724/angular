using Abp.Application.Services.Dto;
using System;

namespace Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions.Dtos
{
    public class GetAllEccpCompanyUserAuditLogsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int CheckStateFilter { get; set; }


		 public string UserNameFilter { get; set; }

		 
    }
}