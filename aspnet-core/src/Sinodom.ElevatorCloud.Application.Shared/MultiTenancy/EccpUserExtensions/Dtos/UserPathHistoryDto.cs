
using System;
using Abp.Application.Services.Dto;

namespace Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions.Dtos
{
    public class UserPathHistoryDto : EntityDto<long>
    {
		public string PhoneId { get; set; }

		public double Longitude { get; set; }

		public double Latitude { get; set; }


		 public long UserId { get; set; }

		 
    }
}