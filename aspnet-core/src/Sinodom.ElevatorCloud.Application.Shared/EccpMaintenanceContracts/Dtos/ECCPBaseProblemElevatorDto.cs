// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseElevatorLookupTableDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Sinodom.ElevatorCloud.EccpMaintenanceContracts.Dtos
{
    using System;

    /// <summary>
    /// The eccp base elevator lookup table dto.
    /// </summary>
    public class ECCPBaseProblemElevatorDto
    {
        public string ProblemElevatorIds { get; set; }
        public string ProblemElevatorNames { get; set; }
        public List<ECCPBaseElevatorLookupTableDto> ECCPBaseElevatorLookupTableDtoList { get; set; }
    }
}