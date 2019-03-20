// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictTempWorkOrderTypesController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpDictTempWorkOrderTypes;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp dict temp work order types controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpDict_EccpDictTempWorkOrderTypes)]
    public class EccpDictTempWorkOrderTypesController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp dict temp work order types app service.
        /// </summary>
        private readonly IEccpDictTempWorkOrderTypesAppService _eccpDictTempWorkOrderTypesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpDictTempWorkOrderTypesController"/> class.
        /// </summary>
        /// <param name="eccpDictTempWorkOrderTypesAppService">
        /// The eccp dict temp work order types app service.
        /// </param>
        public EccpDictTempWorkOrderTypesController(
            IEccpDictTempWorkOrderTypesAppService eccpDictTempWorkOrderTypesAppService)
        {
            this._eccpDictTempWorkOrderTypesAppService = eccpDictTempWorkOrderTypesAppService;
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
            AppPermissions.Pages_EccpDict_EccpDictTempWorkOrderTypes_Create,
            AppPermissions.Pages_EccpDict_EccpDictTempWorkOrderTypes_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetEccpDictTempWorkOrderTypeForEditOutput getEccpDictTempWorkOrderTypeForEditOutput;

            if (id.HasValue)
            {
                getEccpDictTempWorkOrderTypeForEditOutput =
                    await this._eccpDictTempWorkOrderTypesAppService.GetEccpDictTempWorkOrderTypeForEdit(
                        new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpDictTempWorkOrderTypeForEditOutput =
                    new GetEccpDictTempWorkOrderTypeForEditOutput
                        {
                            EccpDictTempWorkOrderType = new CreateOrEditEccpDictTempWorkOrderTypeDto()
                        };
            }

            var viewModel = new CreateOrEditEccpDictTempWorkOrderTypeViewModel
                                {
                                    EccpDictTempWorkOrderType = getEccpDictTempWorkOrderTypeForEditOutput
                                        .EccpDictTempWorkOrderType
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
            var model = new EccpDictTempWorkOrderTypesViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp dict temp work order type modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpDictTempWorkOrderTypeModal(GetEccpDictTempWorkOrderTypeForView data)
        {
            var model = new EccpDictTempWorkOrderTypeViewModel
                            {
                                EccpDictTempWorkOrderType = data.EccpDictTempWorkOrderType
                            };

            return this.PartialView("_ViewEccpDictTempWorkOrderTypeModal", model);
        }
    }
}