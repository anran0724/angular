// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenanceContractDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceContracts.Dtos
{
    using System;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp maintenance contract dto.
    /// </summary>
    public class StopContractEccpMaintenanceContractDto : FullAuditedEntityDto<long?>
    {
        public string StopContractRemarks { get; set; }
        public bool IsStop { get; set; }
        public DateTime? StopDate { get; set; }
    }
}