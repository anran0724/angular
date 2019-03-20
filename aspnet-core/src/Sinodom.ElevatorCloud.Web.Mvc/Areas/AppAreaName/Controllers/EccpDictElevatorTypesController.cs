// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictElevatorTypesController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpDictElevatorTypes;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp dict elevator types controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpDict_EccpDictElevatorTypes)]
    public class EccpDictElevatorTypesController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp dict elevator types app service.
        /// </summary>
        private readonly IEccpDictElevatorTypesAppService _eccpDictElevatorTypesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpDictElevatorTypesController"/> class.
        /// </summary>
        /// <param name="eccpDictElevatorTypesAppService">
        /// The eccp dict elevator types app service.
        /// </param>
        public EccpDictElevatorTypesController(IEccpDictElevatorTypesAppService eccpDictElevatorTypesAppService)
        {
            this._eccpDictElevatorTypesAppService = eccpDictElevatorTypesAppService;
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
            AppPermissions.Pages_EccpDict_EccpDictElevatorTypes_Create,
            AppPermissions.Pages_EccpDict_EccpDictElevatorTypes_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetEccpDictElevatorTypeForEditOutput getEccpDictElevatorTypeForEditOutput;

            if (id.HasValue)
            {
                getEccpDictElevatorTypeForEditOutput =
                    await this._eccpDictElevatorTypesAppService.GetEccpDictElevatorTypeForEdit(
                        new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpDictElevatorTypeForEditOutput =
                    new GetEccpDictElevatorTypeForEditOutput
                        {
                            EccpDictElevatorType = new CreateOrEditEccpDictElevatorTypeDto()
                        };
            }

            var viewModel = new CreateOrEditEccpDictElevatorTypeViewModel
                                {
                                    EccpDictElevatorType = getEccpDictElevatorTypeForEditOutput.EccpDictElevatorType
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
            var model = new EccpDictElevatorTypesViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp dict elevator type modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpDictElevatorTypeModal(GetEccpDictElevatorTypeForView data)
        {
            var model = new EccpDictElevatorTypeViewModel { EccpDictElevatorType = data.EccpDictElevatorType };

            return this.PartialView("_ViewEccpDictElevatorTypeModal", model);
        }
    }
}