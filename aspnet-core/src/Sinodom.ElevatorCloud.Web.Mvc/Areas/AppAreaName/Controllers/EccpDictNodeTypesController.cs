// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictNodeTypesController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpDictNodeTypes;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp dict node types controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpDict_EccpDictNodeTypes)]
    public class EccpDictNodeTypesController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp dict node types app service.
        /// </summary>
        private readonly IEccpDictNodeTypesAppService _eccpDictNodeTypesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpDictNodeTypesController"/> class.
        /// </summary>
        /// <param name="eccpDictNodeTypesAppService">
        /// The eccp dict node types app service.
        /// </param>
        public EccpDictNodeTypesController(IEccpDictNodeTypesAppService eccpDictNodeTypesAppService)
        {
            this._eccpDictNodeTypesAppService = eccpDictNodeTypesAppService;
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
            AppPermissions.Pages_EccpDict_EccpDictNodeTypes_Create,
            AppPermissions.Pages_EccpDict_EccpDictNodeTypes_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetEccpDictNodeTypeForEditOutput getEccpDictNodeTypeForEditOutput;

            if (id.HasValue)
            {
                getEccpDictNodeTypeForEditOutput =
                    await this._eccpDictNodeTypesAppService.GetEccpDictNodeTypeForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpDictNodeTypeForEditOutput =
                    new GetEccpDictNodeTypeForEditOutput { EccpDictNodeType = new CreateOrEditEccpDictNodeTypeDto() };
            }

            var viewModel = new CreateOrEditEccpDictNodeTypeViewModel
                                {
                                    EccpDictNodeType = getEccpDictNodeTypeForEditOutput.EccpDictNodeType
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
            var model = new EccpDictNodeTypesViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp dict node type modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpDictNodeTypeModal(GetEccpDictNodeTypeForView data)
        {
            var model = new EccpDictNodeTypeViewModel { EccpDictNodeType = data.EccpDictNodeType };

            return this.PartialView("_ViewEccpDictNodeTypeModal", model);
        }
    }
}