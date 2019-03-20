using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Sinodom.ElevatorCloud.EccpAppVersions.Dtos
{
    public class GetEccpAppVersionForEditOutput
    {
        public CreateOrEditEccpAppVersionDto EccpAppVersion { get; set; }
    }
}