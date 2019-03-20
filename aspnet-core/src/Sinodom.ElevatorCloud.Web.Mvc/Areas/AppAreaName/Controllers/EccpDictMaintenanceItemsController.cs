// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictMaintenanceItemsController.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Controllers
{
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.AspNetCore.Mvc.Authorization;

    using Microsoft.AspNetCore.Mvc;

    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.EccpDict;
    using Sinodom.ElevatorCloud.EccpDict.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpDictMaintenanceItems;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp dict maintenance items controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpDict_EccpDictMaintenanceItems)]
    public class EccpDictMaintenanceItemsController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp dict maintenance items app service.
        /// </summary>
        private readonly IEccpDictMaintenanceItemsAppService _eccpDictMaintenanceItemsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpDictMaintenanceItemsController"/> class.
        /// </summary>
        /// <param name="eccpDictMaintenanceItemsAppService">
        /// The eccp dict maintenance items app service.
        /// </param>
        public EccpDictMaintenanceItemsController(
            IEccpDictMaintenanceItemsAppService eccpDictMaintenanceItemsAppService)
        {
            this._eccpDictMaintenanceItemsAppService = eccpDictMaintenanceItemsAppService;
        }

        /// <summary>
        /// The create or edit modal.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpMvcAuthorize(
            AppPermissions.Pages_EccpDict_EccpDictMaintenanceItems_Create,
            AppPermissions.Pages_EccpDict_EccpDictMaintenanceItems_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetEccpDictMaintenanceItemForEditOutput getEccpDictMaintenanceItemForEditOutput;

            if (id.HasValue)
            {
                getEccpDictMaintenanceItemForEditOutput =
                    await this._eccpDictMaintenanceItemsAppService.GetEccpDictMaintenanceItemForEdit(
                        new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpDictMaintenanceItemForEditOutput =
                    new GetEccpDictMaintenanceItemForEditOutput
                        {
                            EccpDictMaintenanceItem = new CreateOrEditEccpDictMaintenanceItemDto()
                        };
            }

            var viewModel = new CreateOrEditEccpDictMaintenanceItemViewModel
                                {
                                    EccpDictMaintenanceItem =
                                        getEccpDictMaintenanceItemForEditOutput.EccpDictMaintenanceItem
                                };

            return this.PartialView("_CreateOrEditModal", viewModel);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var model = new EccpDictMaintenanceItemsViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp dict maintenance item modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpDictMaintenanceItemModal(GetEccpDictMaintenanceItemForView data)
        {
            var model = new EccpDictMaintenanceItemViewModel { EccpDictMaintenanceItem = data.EccpDictMaintenanceItem };

            return this.PartialView("_ViewEccpDictMaintenanceItemModal", model);
        }
    }
}