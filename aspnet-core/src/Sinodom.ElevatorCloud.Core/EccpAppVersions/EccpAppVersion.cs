

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpAppVersions
{
	[Table("EccpAppVersions")]
    public class EccpAppVersion : FullAuditedEntity 
    {



		[Required]
		[StringLength(EccpAppVersionConsts.MaxVersionNameLength, MinimumLength = EccpAppVersionConsts.MinVersionNameLength)]
		public virtual string VersionName { get; set; }
		
		[Required]
		[StringLength(EccpAppVersionConsts.MaxVersionCodeLength, MinimumLength = EccpAppVersionConsts.MinVersionCodeLength)]
		public virtual string VersionCode { get; set; }
		
		[StringLength(EccpAppVersionConsts.MaxUpdateLogLength, MinimumLength = EccpAppVersionConsts.MinUpdateLogLength)]
		public virtual string UpdateLog { get; set; }
		
		[Required]
		[StringLength(EccpAppVersionConsts.MaxDownloadUrlLength, MinimumLength = EccpAppVersionConsts.MinDownloadUrlLength)]
		public virtual string DownloadUrl { get; set; }
		
		[StringLength(EccpAppVersionConsts.MaxVersionTypeLength, MinimumLength = EccpAppVersionConsts.MinVersionTypeLength)]
		public virtual string VersionType { get; set; }
		

    }
}