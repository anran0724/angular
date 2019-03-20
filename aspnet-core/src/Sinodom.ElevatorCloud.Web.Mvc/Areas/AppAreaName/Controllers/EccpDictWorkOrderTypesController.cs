// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictWorkOrderTypesController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpDictWorkOrderTypes;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp dict work order types controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpDict_EccpDictWorkOrderTypes)]
    public class EccpDictWorkOrderTypesController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp dict work order types app service.
        /// </summary>
        private readonly IEccpDictWorkOrderTypesAppService _eccpDictWorkOrderTypesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpDictWorkOrderTypesController"/> class.
        /// </summary>
        /// <param name="eccpDictWorkOrderTypesAppService">
        /// The eccp dict work order types app service.
        /// </param>
        public EccpDictWorkOrderTypesController(IEccpDictWorkOrderTypesAppService eccpDictWorkOrderTypesAppService)
        {
            this._eccpDictWorkOrderTypesAppService = eccpDictWorkOrderTypesAppService;
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
            AppPermissions.Pages_EccpDict_EccpDictWorkOrderTypes_Create,
            AppPermissions.Pages_EccpDict_EccpDictWorkOrderTypes_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetEccpDictWorkOrderTypeForEditOutput getEccpDictWorkOrderTypeForEditOutput;

            if (id.HasValue)
            {
                getEccpDictWorkOrderTypeForEditOutput =
                    await this._eccpDictWorkOrderTypesAppService.GetEccpDictWorkOrderTypeForEdit(
                        new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpDictWorkOrderTypeForEditOutput =
                    new GetEccpDictWorkOrderTypeForEditOutput
                        {
                            EccpDictWorkOrderType = new CreateOrEditEccpDictWorkOrderTypeDto()
                        };
            }

            var viewModel = new CreateOrEditEccpDictWorkOrderTypeViewModel
                                {
                                    EccpDictWorkOrderType = getEccpDictWorkOrderTypeForEditOutput.EccpDictWorkOrderType
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
            var model = new EccpDictWorkOrderTypesViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp dict work order type modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpDictWorkOrderTypeModal(GetEccpDictWorkOrderTypeForView data)
        {
            var model = new EccpDictWorkOrderTypeViewModel { EccpDictWorkOrderType = data.EccpDictWorkOrderType };

            return this.PartialView("_ViewEccpDictWorkOrderTypeModal", model);
        }
    }
}