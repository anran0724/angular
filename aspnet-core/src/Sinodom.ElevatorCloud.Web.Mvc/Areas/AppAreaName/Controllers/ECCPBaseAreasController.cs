// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseAreasController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.ECCPBaseAreas;
    using Sinodom.ElevatorCloud.ECCPBaseAreas.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.ECCPBaseAnnualInspectionUnits;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.ECCPBaseAreas;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp base areas controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpBase_EccpBaseAreas)]
    public class ECCPBaseAreasController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp base areas app service.
        /// </summary>
        private readonly IECCPBaseAreasAppService _eccpBaseAreasAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBaseAreasController"/> class.
        /// </summary>
        /// <param name="eccpBaseAreasAppService">
        /// The eccp base areas app service.
        /// </param>
        public ECCPBaseAreasController(IECCPBaseAreasAppService eccpBaseAreasAppService)
        {
            this._eccpBaseAreasAppService = eccpBaseAreasAppService;
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
            AppPermissions.Pages_EccpBase_EccpBaseAreas_Create,
            AppPermissions.Pages_EccpBase_EccpBaseAreas_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetECCPBaseAreaForEditOutput getEccpBaseAreaForEditOutput;

            if (id.HasValue)
            {
                getEccpBaseAreaForEditOutput =
                    await this._eccpBaseAreasAppService.GetECCPBaseAreaForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpBaseAreaForEditOutput = new GetECCPBaseAreaForEditOutput
                                                   {
                                                       ECCPBaseArea = new CreateOrEditECCPBaseAreaDto()
                                                   };
            }

            var viewModel = new CreateOrEditECCPBaseAreaViewModel
                                {
                                    EccpBaseArea = getEccpBaseAreaForEditOutput.ECCPBaseArea,
                                    ProvinceName = getEccpBaseAreaForEditOutput.ProvinceName,
                                    CityName = getEccpBaseAreaForEditOutput.CityName,
                                    DistrictName = getEccpBaseAreaForEditOutput.DistrictName,
                                    ProvinceId = getEccpBaseAreaForEditOutput.ProvinceId,
                                    CityId = getEccpBaseAreaForEditOutput.CityId,
                                    DistrictId = getEccpBaseAreaForEditOutput.DistrictId
                                };

            return this.PartialView("_CreateOrEditModal", viewModel);
        }

        /// <summary>
        /// The eccp base area lookup table modal.
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
            AppPermissions.Pages_EccpBase_EccpBaseAreas_Create,
            AppPermissions.Pages_EccpBase_EccpBaseAreas_Edit)]
        public PartialViewResult EccpBaseAreaLookupTableModal(int? id, string displayName)
        {
            var viewModel = new ECCPBaseAreaLookupTableViewModel
                                {
                                    Id = id, DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_ECCPBaseAreaLookupTableModal", viewModel);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var model = new ECCPBaseAreasViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp base area modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpBaseAreaModal(GetECCPBaseAreaForView data)
        {
            var model = new ECCPBaseAreaViewModel { ECCPBaseArea = data.ECCPBaseArea };

            return this.PartialView("_ViewECCPBaseAreaModal", model);
        }
    }
}