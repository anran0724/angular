
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Sinodom.ElevatorCloud.EccpAppVersions.Dtos
{
    public class CreateOrEditEccpAppVersionDto : FullAuditedEntityDto<int?>
    {

		[Required]
		[StringLength(EccpAppVersionConsts.MaxVersionNameLength, MinimumLength = EccpAppVersionConsts.MinVersionNameLength)]
		public string VersionName { get; set; }
		
		
		[Required]
		[StringLength(EccpAppVersionConsts.MaxVersionCodeLength, MinimumLength = EccpAppVersionConsts.MinVersionCodeLength)]
		public string VersionCode { get; set; }
		
		
		[StringLength(EccpAppVersionConsts.MaxUpdateLogLength, MinimumLength = EccpAppVersionConsts.MinUpdateLogLength)]
		public string UpdateLog { get; set; }
		
		
		[Required]
		[StringLength(EccpAppVersionConsts.MaxDownloadUrlLength, MinimumLength = EccpAppVersionConsts.MinDownloadUrlLength)]
		public string DownloadUrl { get; set; }
		
		
		[StringLength(EccpAppVersionConsts.MaxVersionTypeLength, MinimumLength = EccpAppVersionConsts.MinVersionTypeLength)]
		public string VersionType { get; set; }

    }
}