
using System;
using Abp.Application.Services.Dto;

namespace Sinodom.ElevatorCloud.EccpAppVersions.Dtos
{
    public class EccpAppVersionDto : EntityDto
    {
		public string VersionName { get; set; }

		public string VersionCode { get; set; }

		public string UpdateLog { get; set; }

		public string DownloadUrl { get; set; }

		public string VersionType { get; set; }



    }
}