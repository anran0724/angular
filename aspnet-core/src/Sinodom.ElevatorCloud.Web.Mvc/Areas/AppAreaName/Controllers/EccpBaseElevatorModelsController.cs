// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpBaseElevatorModelsController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.EccpBaseElevatorModels;
    using Sinodom.ElevatorCloud.EccpBaseElevatorModels.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpBaseElevatorModels;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp base elevator models controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpBase_EccpBaseElevatorModels)]
    public class EccpBaseElevatorModelsController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp base elevator models app service.
        /// </summary>
        private readonly IEccpBaseElevatorModelsAppService _eccpBaseElevatorModelsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpBaseElevatorModelsController"/> class.
        /// </summary>
        /// <param name="eccpBaseElevatorModelsAppService">
        /// The eccp base elevator models app service.
        /// </param>
        public EccpBaseElevatorModelsController(IEccpBaseElevatorModelsAppService eccpBaseElevatorModelsAppService)
        {
            this._eccpBaseElevatorModelsAppService = eccpBaseElevatorModelsAppService;
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
            AppPermissions.Pages_EccpBase_EccpBaseElevatorModels_Create,
            AppPermissions.Pages_EccpBase_EccpBaseElevatorModels_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetEccpBaseElevatorModelForEditOutput getEccpBaseElevatorModelForEditOutput;

            if (id.HasValue)
            {
                getEccpBaseElevatorModelForEditOutput =
                    await this._eccpBaseElevatorModelsAppService.GetEccpBaseElevatorModelForEdit(
                        new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpBaseElevatorModelForEditOutput =
                    new GetEccpBaseElevatorModelForEditOutput
                        {
                            EccpBaseElevatorModel = new CreateOrEditEccpBaseElevatorModelDto()
                        };
            }

            var viewModel = new CreateOrEditEccpBaseElevatorModelViewModel
                                {
                                    EccpBaseElevatorModel = getEccpBaseElevatorModelForEditOutput.EccpBaseElevatorModel,
                                    EccpBaseElevatorBrandName =
                                        getEccpBaseElevatorModelForEditOutput.EccpBaseElevatorBrandName
                                };

            return this.PartialView("_CreateOrEditModal", viewModel);
        }

        /// <summary>
        /// The eccp base elevator brand lookup table modal.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        [AbpMvcAuthorize(
            AppPermissions.Pages_EccpBase_EccpBaseElevatorModels_Create,
            AppPermissions.Pages_EccpBase_EccpBaseElevatorModels_Edit)]
        public PartialViewResult EccpBaseElevatorBrandLookupTableModal(int? id, string displayName)
        {
            var viewModel = new EccpBaseElevatorBrandLookupTableViewModel
                                {
                                    Id = id, DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_EccpBaseElevatorBrandLookupTableModal", viewModel);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var model = new EccpBaseElevatorModelsViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp base elevator model modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpBaseElevatorModelModal(GetEccpBaseElevatorModelForView data)
        {
            var model = new EccpBaseElevatorModelViewModel
                            {
                                EccpBaseElevatorModel = data.EccpBaseElevatorModel,
                                EccpBaseElevatorBrandName = data.EccpBaseElevatorBrandName
                            };

            return this.PartialView("_ViewEccpBaseElevatorModelModal", model);
        }
    }
}