// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseAnnualInspectionUnitsController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits;
    using Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.ECCPBaseAnnualInspectionUnits;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp base annual inspection units controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpBase_EccpBaseAnnualInspectionUnits)]
    public class ECCPBaseAnnualInspectionUnitsController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp base annual inspection units app service.
        /// </summary>
        private readonly IECCPBaseAnnualInspectionUnitsAppService _eccpBaseAnnualInspectionUnitsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBaseAnnualInspectionUnitsController"/> class.
        /// </summary>
        /// <param name="eccpBaseAnnualInspectionUnitsAppService">
        /// The eccp base annual inspection units app service.
        /// </param>
        public ECCPBaseAnnualInspectionUnitsController(
            IECCPBaseAnnualInspectionUnitsAppService eccpBaseAnnualInspectionUnitsAppService)
        {
            this._eccpBaseAnnualInspectionUnitsAppService = eccpBaseAnnualInspectionUnitsAppService;
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
            AppPermissions.Pages_EccpBase_EccpBaseAnnualInspectionUnits_Create,
            AppPermissions.Pages_EccpBase_EccpBaseAnnualInspectionUnits_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(long? id)
        {
            GetECCPBaseAnnualInspectionUnitForEditOutput getEccpBaseAnnualInspectionUnitForEditOutput;

            if (id.HasValue)
            {
                getEccpBaseAnnualInspectionUnitForEditOutput =
                    await this._eccpBaseAnnualInspectionUnitsAppService.GetECCPBaseAnnualInspectionUnitForEdit(
                        new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getEccpBaseAnnualInspectionUnitForEditOutput = new GetECCPBaseAnnualInspectionUnitForEditOutput
                                                                   {
                                                                       ECCPBaseAnnualInspectionUnit =
                                                                           new
                                                                               CreateOrEditECCPBaseAnnualInspectionUnitDto()
                                                                   };
            }

            var viewModel = new CreateOrEditECCPBaseAnnualInspectionUnitViewModel
                                {
                                    EccpBaseAnnualInspectionUnit =
                                        getEccpBaseAnnualInspectionUnitForEditOutput.ECCPBaseAnnualInspectionUnit,
                                    ProvinceName = getEccpBaseAnnualInspectionUnitForEditOutput.ProvinceName,
                                    CityName = getEccpBaseAnnualInspectionUnitForEditOutput.CityName,
                                    DistrictName = getEccpBaseAnnualInspectionUnitForEditOutput.DistrictName,
                                    StreetName = getEccpBaseAnnualInspectionUnitForEditOutput.StreetName
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
            AppPermissions.Pages_EccpBase_EccpBaseAnnualInspectionUnits_Create,
            AppPermissions.Pages_EccpBase_EccpBaseAnnualInspectionUnits_Edit)]
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
            var model = new ECCPBaseAnnualInspectionUnitsViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp base annual inspection unit modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpBaseAnnualInspectionUnitModal(GetECCPBaseAnnualInspectionUnitForView data)
        {
            var model = new ECCPBaseAnnualInspectionUnitViewModel
                            {
                                ECCPBaseAnnualInspectionUnit = data.ECCPBaseAnnualInspectionUnit,
                                ProvinceName = data.ProvinceName,
                                CityName = data.CityName,
                                DistrictName = data.DistrictName,
                                StreetName = data.StreetName
                            };

            return this.PartialView("_ViewECCPBaseAnnualInspectionUnitModal", model);
        }
    }
}