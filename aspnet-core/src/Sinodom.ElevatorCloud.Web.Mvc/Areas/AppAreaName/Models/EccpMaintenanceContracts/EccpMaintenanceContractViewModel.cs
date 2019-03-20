// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceContractViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceContracts
{
    using Sinodom.ElevatorCloud.EccpMaintenanceContracts.Dtos;

    /// <summary>
    /// The eccp maintenance contract view model.
    /// </summary>
    public class EccpMaintenanceContractViewModel : GetEccpMaintenanceContractForView
    {
        public GetEccpMaintenanceContractForEditOutput GetEccpMaintenanceContractForEdit { get; set; }
    }
}