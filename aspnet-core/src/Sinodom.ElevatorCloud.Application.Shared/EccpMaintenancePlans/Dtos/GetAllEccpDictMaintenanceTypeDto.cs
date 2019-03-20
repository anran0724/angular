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
        ///     ά����������
        /// </summary>
        public string Name { get; set; }
    }
}