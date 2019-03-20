
using System;
using Abp.Application.Services.Dto;

namespace Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions.Dtos
{
    public class EccpCompanyUserAuditLogDto : EntityDto
    {
		public bool CheckState { get; set; }

		public string Remarks { get; set; }


		 public long UserId { get; set; }

		 
    }
}