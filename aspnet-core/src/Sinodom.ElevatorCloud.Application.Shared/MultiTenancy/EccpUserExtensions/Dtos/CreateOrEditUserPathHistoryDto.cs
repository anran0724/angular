
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions.Dtos
{
    public class CreateOrEditUserPathHistoryDto : EntityDto<long?>
    {

		[Required]
		[StringLength(UserPathHistoryConsts.MaxPhoneIdLength, MinimumLength = UserPathHistoryConsts.MinPhoneIdLength)]
		public string PhoneId { get; set; }
		
		
		public double Longitude { get; set; }
		
		
		public double Latitude { get; set; }
		
		
		 public long UserId { get; set; }
		 
		 
    }
}