// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpBaseElevatorBrandsController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.EccpBaseElevatorBrands;
    using Sinodom.ElevatorCloud.EccpBaseElevatorBrands.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpBaseElevatorBrands;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp base elevator brands controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpBase_EccpBaseElevatorBrands)]
    public class EccpBaseElevatorBrandsController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp base elevator brands app service.
        /// </summary>
        private readonly IEccpBaseElevatorBrandsAppService _eccpBaseElevatorBrandsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpBaseElevatorBrandsController"/> class.
        /// </summary>
        /// <param name="eccpBaseElevatorBrandsAppService">
        /// The eccp base elevator brands app service.
        /// </param>
        public EccpBaseElevatorBrandsController(IEccpBaseElevatorBrandsAppService eccpBaseElevatorBrandsAppService)
        {
            this._eccpBaseElevatorBrandsAppService = eccpBaseElevatorBrandsAppService;
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
            AppPermissions.Pages_EccpBase_EccpBaseElevatorBrands_Create,
            AppPermissions.Pages_EccpBase_EccpBaseElevatorBrands_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetEccpBaseElevatorBrandForEditOutput getEccpBaseElevatorBrandForEditOutput;

            if (id.HasValue)
            {
                getEccpBaseElevatorBrandForEditOutput =
                    await this._eccpBaseElevatorBrandsAppService.GetEccpBaseElevatorBrandForEdit(
                        new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpBaseElevatorBrandForEditOutput =
                    new GetEccpBaseElevatorBrandForEditOutput
                        {
                            EccpBaseElevatorBrand = new CreateOrEditEccpBaseElevatorBrandDto()
                        };
            }

            var viewModel = new CreateOrEditEccpBaseElevatorBrandViewModel
                                {
                                    EccpBaseElevatorBrand = getEccpBaseElevatorBrandForEditOutput.EccpBaseElevatorBrand,
                                    EccpBaseProductionCompanyName = getEccpBaseElevatorBrandForEditOutput
                                        .ECCPBaseProductionCompanyName
                                };

            return this.PartialView("_CreateOrEditModal", viewModel);
        }

        /// <summary>
        /// The eccp base production company lookup table modal.
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
            AppPermissions.Pages_EccpBase_EccpBaseElevatorBrands_Create,
            AppPermissions.Pages_EccpBase_EccpBaseElevatorBrands_Edit)]
        public PartialViewResult EccpBaseProductionCompanyLookupTableModal(long? id, string displayName)
        {
            var viewModel = new ECCPBaseProductionCompanyLookupTableViewModel
                                {
                                    Id = id, DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_ECCPBaseProductionCompanyLookupTableModal", viewModel);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var model = new EccpBaseElevatorBrandsViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp base elevator brand modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpBaseElevatorBrandModal(GetEccpBaseElevatorBrandForView data)
        {
            var model = new EccpBaseElevatorBrandViewModel
                            {
                                EccpBaseElevatorBrand = data.EccpBaseElevatorBrand,
                                ECCPBaseProductionCompanyName = data.ECCPBaseProductionCompanyName
                            };

            return this.PartialView("_ViewEccpBaseElevatorBrandModal", model);
        }
    }
}