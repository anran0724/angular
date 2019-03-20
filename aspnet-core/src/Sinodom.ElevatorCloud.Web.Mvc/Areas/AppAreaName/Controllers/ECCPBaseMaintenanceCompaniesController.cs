// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseMaintenanceCompaniesController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.ECCPBaseMaintenanceCompanies;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp base maintenance companies controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpBase_EccpBaseMaintenanceCompanies)]
    public class ECCPBaseMaintenanceCompaniesController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _e ccp base maintenance companies app service.
        /// </summary>
        private readonly IECCPBaseMaintenanceCompaniesAppService _eccpBaseMaintenanceCompaniesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBaseMaintenanceCompaniesController"/> class.
        /// </summary>
        /// <param name="eccpBaseMaintenanceCompaniesAppService">
        /// The e ccp base maintenance companies app service.
        /// </param>
        public ECCPBaseMaintenanceCompaniesController(
            IECCPBaseMaintenanceCompaniesAppService eccpBaseMaintenanceCompaniesAppService)
        {
            this._eccpBaseMaintenanceCompaniesAppService = eccpBaseMaintenanceCompaniesAppService;
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
            AppPermissions.Pages_EccpBase_EccpBaseMaintenanceCompanies_Create,
            AppPermissions.Pages_EccpBase_EccpBaseMaintenanceCompanies_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetECCPBaseMaintenanceCompanyForEditOutput getEccpBaseMaintenanceCompanyForEditOutput;

            if (id.HasValue)
            {
                getEccpBaseMaintenanceCompanyForEditOutput =
                    await this._eccpBaseMaintenanceCompaniesAppService.GetECCPBaseMaintenanceCompanyForEdit(
                        new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpBaseMaintenanceCompanyForEditOutput = new GetECCPBaseMaintenanceCompanyForEditOutput
                {
                    ECCPBaseMaintenanceCompany =
                                                                         new EditECCPBaseMaintenanceCompanyDto()
                };
            }

            var viewModel = new CreateOrEditECCPBaseMaintenanceCompanyViewModel
            {
                EccpBaseMaintenanceCompany =
                                        getEccpBaseMaintenanceCompanyForEditOutput.ECCPBaseMaintenanceCompany,
                ProvinceName = getEccpBaseMaintenanceCompanyForEditOutput.ProvinceName,
                CityName = getEccpBaseMaintenanceCompanyForEditOutput.CityName,
                DistrictName = getEccpBaseMaintenanceCompanyForEditOutput.DistrictName,
                StreetName = getEccpBaseMaintenanceCompanyForEditOutput.StreetName,
                AptitudePhotoId = getEccpBaseMaintenanceCompanyForEditOutput.AptitudePhotoId,
                BusinessLicenseId = getEccpBaseMaintenanceCompanyForEditOutput.BusinessLicenseId
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
            AppPermissions.Pages_EccpBase_EccpBaseMaintenanceCompanies_Create,
            AppPermissions.Pages_EccpBase_EccpBaseMaintenanceCompanies_Edit)]
        public PartialViewResult EccpBaseAreaLookupTableModal(int? id, string displayName)
        {
            var viewModel = new ECCPBaseAreaLookupTableViewModel
            {
                Id = id,
                DisplayName = displayName,
                FilterText = string.Empty
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
            var model = new ECCPBaseMaintenanceCompaniesViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp base maintenance company modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpBaseMaintenanceCompanyModal(GetECCPBaseMaintenanceCompanyForView data)
        {
            var model = new ECCPBaseMaintenanceCompanyViewModel
            {
                ECCPBaseMaintenanceCompany = data.ECCPBaseMaintenanceCompany,
                ProvinceName = data.ProvinceName,
                CityName = data.CityName,
                DistrictName = data.DistrictName,
                StreetName = data.StreetName
            };

            return this.PartialView("_ViewECCPBaseMaintenanceCompanyModal", model);
        }
    }
}