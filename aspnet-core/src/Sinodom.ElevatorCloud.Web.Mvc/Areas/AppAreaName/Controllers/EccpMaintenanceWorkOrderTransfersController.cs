// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkOrderTransfersController.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Controllers
{
    using Abp.AspNetCore.Mvc.Authorization;

    using Microsoft.AspNetCore.Mvc;

    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrderTransfers.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceWorkOrders;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.MaintenanceWorkOrderTransfers;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp maintenance work order transfers controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrderTransfers)]
    public class EccpMaintenanceWorkOrderTransfersController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [AbpMvcAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrderTransfers_Audit)]
        public ActionResult Index()
        {
            var model = new EccpMaintenanceWorkOrdersViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The maintenance work order transfers audit logs table model.
        /// 审批日志
        /// </summary>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult MaintenanceWorkOrderTransfersAuditLogsTableModel()
        {
            return this.PartialView("_MaintenanceWorkOrderTransfersAuditLogsTableModel");
        }

        /// <summary>
        /// The view maintenance work order transfers model.
        /// 工单申请转接详情
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewMaintenanceWorkOrderTransfersModel(
            GetEccpMaintenanceWorkOrderTransfersForView data)
        {
            var model = new MaintenanceWorkOrderTransfersViewModel
                            {
                                Title = data.Title,
                                StatusName = data.StatusName,
                                OrderTypeName = data.OrderTypeName,
                                OrderCreationTime = data.OrderCreationTime,
                                ApplicationTransferName = data.ApplicationTransferName,
                                TransferUserName = data.TransferUserName,
                                IsApproved = data.IsApproved,
                                Category = data.Category,
                                ApplicationTransferCreationTime = data.ApplicationTransferCreationTime
                            };
            return this.PartialView("_ViewMaintenanceWorkOrderTransfersModel", model);
        }
    }
}