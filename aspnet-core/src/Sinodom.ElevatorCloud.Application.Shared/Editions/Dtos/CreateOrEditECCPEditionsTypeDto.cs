
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Sinodom.ElevatorCloud.Editions.Dtos
{
    public class CreateOrEditECCPEditionsTypeDto : FullAuditedEntityDto<int?>
    {

		[Required]
		[StringLength(ECCPEditionsTypeConsts.MaxNameLength, MinimumLength = ECCPEditionsTypeConsts.MinNameLength)]
		public string Name { get; set; }
		
		

    }
}