
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Sinodom.ElevatorCloud.EccpAppVersions.Dtos
{
    public class UploadCreateOrEditEccpAppVersionDto : FullAuditedEntityDto<int?>
    {

		[Required]
		[StringLength(EccpAppVersionConsts.MaxVersionNameLength, MinimumLength = EccpAppVersionConsts.MinVersionNameLength)]
		public string VersionName { get; set; }
		
		
		[Required]
		[StringLength(EccpAppVersionConsts.MaxVersionCodeLength, MinimumLength = EccpAppVersionConsts.MinVersionCodeLength)]
		public string VersionCode { get; set; }
		
		
		[StringLength(EccpAppVersionConsts.MaxUpdateLogLength, MinimumLength = EccpAppVersionConsts.MinUpdateLogLength)]
		public string UpdateLog { get; set; }
		
		
		[StringLength(EccpAppVersionConsts.MaxDownloadUrlLength, MinimumLength = EccpAppVersionConsts.MinDownloadUrlLength)]
		public string DownloadUrl { get; set; }
		
		
		[StringLength(EccpAppVersionConsts.MaxVersionTypeLength, MinimumLength = EccpAppVersionConsts.MinVersionTypeLength)]
		public string VersionType { get; set; }

        /// <summary>
        /// Gets or sets the file token.
        /// </summary>
        [Required]
        [MaxLength(400)]
        public string FileToken { get; set; }

    }
}