using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Sinodom.ElevatorCloud.LanFlows.Dtos
{
    public class GetLanFlowInstanceOperationHistoryForEditOutput
    {
		public CreateOrEditLanFlowInstanceOperationHistoryDto LanFlowInstanceOperationHistory { get; set; }


    }
}