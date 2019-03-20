using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions.Dtos
{
    public class GetUserPathHistoryForEditOutput
    {
		public CreateOrEditUserPathHistoryDto UserPathHistory { get; set; }

		public string UserName { get; set;}


    }
}