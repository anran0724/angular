
using System;
using Abp.Application.Services.Dto;

namespace Sinodom.ElevatorCloud.LanFlows.Dtos
{
    public class LanFlowStatusActionDto : EntityDto
    {
		public int StatusValue { get; set; }

		public string StatusName { get; set; }

		public string ActionName { get; set; }

		public string ActionDesc { get; set; }

		public string ActionCode { get; set; }

		public string UserRoleCode { get; set; }

		public int ArgumentValue { get; set; }

		public bool IsStartProcess { get; set; }

		public bool IsEndProcess { get; set; }

		public bool IsAdopt { get; set; }

		public int SortCode { get; set; }


		 public int SchemeId { get; set; }

		 
    }
}