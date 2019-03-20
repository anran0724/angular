// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceReportGenerationController.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Controllers
{
    using System.Threading.Tasks;

    using Abp.AspNetCore.Mvc.Authorization;

    using Microsoft.AspNetCore.Mvc;

    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpDictMaintenanceItems;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceWorkOrders;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp maintenance report generation controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceReportGeneration)]
    public class EccpMaintenanceReportGenerationController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp maintenance work orders app service.
        /// </summary>
        private readonly IEccpMaintenanceWorkOrdersAppService _eccpMaintenanceWorkOrdersAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceReportGenerationController"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceWorkOrdersAppService">
        /// The eccp maintenance work orders app service.
        /// </param>
        public EccpMaintenanceReportGenerationController(
            IEccpMaintenanceWorkOrdersAppService eccpMaintenanceWorkOrdersAppService)
        {
            this._eccpMaintenanceWorkOrdersAppService = eccpMaintenanceWorkOrdersAppService;
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var model = new EccpMaintenanceWorkOrdersViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The print modal.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<PartialViewResult> PrintModal(int id)
        {
            var getEccpDictMaintenanceItemForViews =
                await this._eccpMaintenanceWorkOrdersAppService.GetEccpDictMaintenanceItem(id);

            var model = new EccpDictMaintenanceItemPrintViewModel
            {
                GetEccpDictMaintenanceItemPrintForViews = getEccpDictMaintenanceItemForViews
            };

            return this.PartialView("_PrintModal", model);
        }
    }
}