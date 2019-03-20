using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Sinodom.ElevatorCloud.LanFlows.Dtos
{
    public class GetLanFlowStatusActionForEditOutput
    {
		public CreateOrEditLanFlowStatusActionDto LanFlowStatusAction { get; set; }

		public string LanFlowSchemeSchemeName { get; set;}


    }
}