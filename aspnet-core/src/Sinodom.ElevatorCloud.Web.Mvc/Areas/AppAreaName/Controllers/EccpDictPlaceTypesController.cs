// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictPlaceTypesController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpDictPlaceTypes;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp dict place types controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpDict_EccpDictPlaceTypes)]
    public class EccpDictPlaceTypesController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp dict place types app service.
        /// </summary>
        private readonly IEccpDictPlaceTypesAppService _eccpDictPlaceTypesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpDictPlaceTypesController"/> class.
        /// </summary>
        /// <param name="eccpDictPlaceTypesAppService">
        /// The eccp dict place types app service.
        /// </param>
        public EccpDictPlaceTypesController(IEccpDictPlaceTypesAppService eccpDictPlaceTypesAppService)
        {
            this._eccpDictPlaceTypesAppService = eccpDictPlaceTypesAppService;
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
            AppPermissions.Pages_EccpDict_EccpDictPlaceTypes_Create,
            AppPermissions.Pages_EccpDict_EccpDictPlaceTypes_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetEccpDictPlaceTypeForEditOutput getEccpDictPlaceTypeForEditOutput;

            if (id.HasValue)
            {
                getEccpDictPlaceTypeForEditOutput =
                    await this._eccpDictPlaceTypesAppService.GetEccpDictPlaceTypeForEdit(
                        new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpDictPlaceTypeForEditOutput =
                    new GetEccpDictPlaceTypeForEditOutput
                        {
                            EccpDictPlaceType = new CreateOrEditEccpDictPlaceTypeDto()
                        };
            }

            var viewModel = new CreateOrEditEccpDictPlaceTypeViewModel
                                {
                                    EccpDictPlaceType = getEccpDictPlaceTypeForEditOutput.EccpDictPlaceType
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
            var model = new EccpDictPlaceTypesViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp dict place type modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpDictPlaceTypeModal(GetEccpDictPlaceTypeForView data)
        {
            var model = new EccpDictPlaceTypeViewModel { EccpDictPlaceType = data.EccpDictPlaceType };

            return this.PartialView("_ViewEccpDictPlaceTypeModal", model);
        }
    }
}