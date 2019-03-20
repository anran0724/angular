
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions.Dtos
{
    public class CreateOrEditEccpCompanyUserAuditLogDto : FullAuditedEntityDto<int?>
    {

		public bool CheckState { get; set; }
		
		
		[Required]
		[StringLength(EccpCompanyUserAuditLogConsts.MaxRemarksLength, MinimumLength = EccpCompanyUserAuditLogConsts.MinRemarksLength)]
		public string Remarks { get; set; }
		
		
		 public long UserId { get; set; }
		 
		 
    }
}