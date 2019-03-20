// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpDictMaintenanceTypeDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenancePlans.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp dict maintenance type dto.
    /// </summary>
    public class GetAllEccpDictMaintenanceTypeDto : EntityDto
    {
        /// <summary>
        ///     维保类型名称
        /// </summary>
        public string Name { get; set; }
    }
}