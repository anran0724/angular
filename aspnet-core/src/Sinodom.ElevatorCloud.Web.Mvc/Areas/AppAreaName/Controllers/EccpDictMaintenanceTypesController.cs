// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictMaintenanceTypesController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpDictMaintenanceTypes;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp dict maintenance types controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpDict_EccpDictMaintenanceTypes)]
    public class EccpDictMaintenanceTypesController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp dict maintenance types app service.
        /// </summary>
        private readonly IEccpDictMaintenanceTypesAppService _eccpDictMaintenanceTypesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpDictMaintenanceTypesController"/> class.
        /// </summary>
        /// <param name="eccpDictMaintenanceTypesAppService">
        /// The eccp dict maintenance types app service.
        /// </param>
        public EccpDictMaintenanceTypesController(
            IEccpDictMaintenanceTypesAppService eccpDictMaintenanceTypesAppService)
        {
            this._eccpDictMaintenanceTypesAppService = eccpDictMaintenanceTypesAppService;
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
            AppPermissions.Pages_EccpDict_EccpDictMaintenanceTypes_Create,
            AppPermissions.Pages_EccpDict_EccpDictMaintenanceTypes_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetEccpDictMaintenanceTypeForEditOutput getEccpDictMaintenanceTypeForEditOutput;

            if (id.HasValue)
            {
                getEccpDictMaintenanceTypeForEditOutput =
                    await this._eccpDictMaintenanceTypesAppService.GetEccpDictMaintenanceTypeForEdit(
                        new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpDictMaintenanceTypeForEditOutput =
                    new GetEccpDictMaintenanceTypeForEditOutput
                        {
                            EccpDictMaintenanceType = new CreateOrEditEccpDictMaintenanceTypeDto()
                        };
            }

            var viewModel = new CreateOrEditEccpDictMaintenanceTypeViewModel
                                {
                                    EccpDictMaintenanceType =
                                        getEccpDictMaintenanceTypeForEditOutput.EccpDictMaintenanceType
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
            var model = new EccpDictMaintenanceTypesViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp dict maintenance type modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpDictMaintenanceTypeModal(GetEccpDictMaintenanceTypeForView data)
        {
            var model = new EccpDictMaintenanceTypeViewModel { EccpDictMaintenanceType = data.EccpDictMaintenanceType };

            return this.PartialView("_ViewEccpDictMaintenanceTypeModal", model);
        }
    }
}