
using Sinodom.ElevatorCloud.EccpBaseElevators;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Sinodom.ElevatorCloud.EccpElevatorQrCodes
{
	[Table("EccpElevatorQrCodes")]
    public class EccpElevatorQrCode : FullAuditedEntity<Guid> , IMayHaveTenant
    {
		public int? TenantId { get; set; }


		[Required]
		[StringLength(EccpElevatorQrCodeConsts.MaxAreaNameLength, MinimumLength = EccpElevatorQrCodeConsts.MinAreaNameLength)]
		public virtual string AreaName { get; set; }
		
		[Required]
		[StringLength(EccpElevatorQrCodeConsts.MaxElevatorNumLength, MinimumLength = EccpElevatorQrCodeConsts.MinElevatorNumLength)]
		public virtual string ElevatorNum { get; set; }
		
		[StringLength(EccpElevatorQrCodeConsts.MaxImgPictureLength, MinimumLength = EccpElevatorQrCodeConsts.MinImgPictureLength)]
		public virtual string ImgPicture { get; set; }
		
		public virtual bool IsInstall { get; set; }
		
		public virtual bool IsGrant { get; set; }
		
		public virtual DateTime? InstallDateTime { get; set; }
		
		public virtual DateTime? GrantDateTime { get; set; }
		

		public virtual Guid? ElevatorId { get; set; }
		public EccpBaseElevator Elevator { get; set; }
		
    }
}