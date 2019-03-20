// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPEditionsTypesController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.Editions;
    using Sinodom.ElevatorCloud.Editions.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.ECCPEditionsTypes;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp editions types controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpDict_EccpEditionsTypes)]
    public class ECCPEditionsTypesController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp editions types app service.
        /// </summary>
        private readonly IECCPEditionsTypesAppService _eccpEditionsTypesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPEditionsTypesController"/> class.
        /// </summary>
        /// <param name="eccpEditionsTypesAppService">
        /// The eccp editions types app service.
        /// </param>
        public ECCPEditionsTypesController(IECCPEditionsTypesAppService eccpEditionsTypesAppService)
        {
            this._eccpEditionsTypesAppService = eccpEditionsTypesAppService;
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
            AppPermissions.Pages_EccpDict_EccpEditionsTypes_Create,
            AppPermissions.Pages_EccpDict_EccpEditionsTypes_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetECCPEditionsTypeForEditOutput getEccpEditionsTypeForEditOutput;

            if (id.HasValue)
            {
                getEccpEditionsTypeForEditOutput =
                    await this._eccpEditionsTypesAppService.GetECCPEditionsTypeForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpEditionsTypeForEditOutput =
                    new GetECCPEditionsTypeForEditOutput { ECCPEditionsType = new CreateOrEditECCPEditionsTypeDto() };
            }

            var viewModel = new CreateOrEditECCPEditionsTypeViewModel
                                {
                                    EccpEditionsType = getEccpEditionsTypeForEditOutput.ECCPEditionsType
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
            var model = new ECCPEditionsTypesViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp editions type modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpEditionsTypeModal(GetECCPEditionsTypeForView data)
        {
            var model = new ECCPEditionsTypeViewModel { ECCPEditionsType = data.ECCPEditionsType };

            return this.PartialView("_ViewECCPEditionsTypeModal", model);
        }
    }
}