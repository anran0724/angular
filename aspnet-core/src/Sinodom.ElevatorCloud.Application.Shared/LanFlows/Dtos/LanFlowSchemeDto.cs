
using System;
using Abp.Application.Services.Dto;

namespace Sinodom.ElevatorCloud.LanFlows.Dtos
{
    public class LanFlowSchemeDto : EntityDto
    {
		public string SchemeName { get; set; }

		public string SchemeType { get; set; }

		public string SchemeContent { get; set; }

		public string TableName { get; set; }

		public int? AuthorizeType { get; set; }

		public int SortCode { get; set; }



    }
}