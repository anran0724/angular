// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpBaseElevatorViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpBaseElevators
{
    using Sinodom.ElevatorCloud.EccpBaseElevators.Dtos;
    using System.Collections.Generic;

    /// <summary>
    /// The eccp base elevator view model.
    /// </summary>
    public class EccpBaseElevatorViewModel : GetEccpBaseElevatorForView
    {
        /// <summary>
        /// Gets or sets the eccp base elevator subsidiary info.
        /// </summary>
        public EccpBaseElevatorSubsidiaryInfoDto EccpBaseElevatorSubsidiaryInfo { get; set; }

        public List<GetChangeLogByElevatorIdDto> EccpBaseElevatorChangeLogList { get; set; }
    }
}