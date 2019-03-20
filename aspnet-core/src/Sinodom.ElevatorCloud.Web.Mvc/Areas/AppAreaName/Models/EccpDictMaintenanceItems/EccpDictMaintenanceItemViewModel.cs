// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictMaintenanceItemViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpDictMaintenanceItems
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.EccpDict.Dtos;

    /// <summary>
    /// The eccp dict maintenance item view model.
    /// </summary>
    public class EccpDictMaintenanceItemViewModel : GetEccpDictMaintenanceItemForView
    {
        /// <summary>
        /// Gets or sets the get eccp dict maintenance item for views.
        /// </summary>
        public List<GetEccpDictMaintenanceItemForView> GetEccpDictMaintenanceItemForViews { get; set; }
    }
}