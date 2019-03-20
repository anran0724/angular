// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBasePropertyCompaniesController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.ECCPBasePropertyCompanies;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp base property companies controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpBase_EccpBasePropertyCompanies)]
    public class ECCPBasePropertyCompaniesController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp base property companies app service.
        /// </summary>
        private readonly IECCPBasePropertyCompaniesAppService _eccpBasePropertyCompaniesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBasePropertyCompaniesController"/> class.
        /// </summary>
        /// <param name="eccpBasePropertyCompaniesAppService">
        /// The eccp base property companies app service.
        /// </param>
        public ECCPBasePropertyCompaniesController(
            IECCPBasePropertyCompaniesAppService eccpBasePropertyCompaniesAppService)
        {
            this._eccpBasePropertyCompaniesAppService = eccpBasePropertyCompaniesAppService;
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
            AppPermissions.Pages_EccpBase_EccpBasePropertyCompanies_Create,
            AppPermissions.Pages_EccpBase_EccpBasePropertyCompanies_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetECCPBasePropertyCompanyForEditOutput getEccpBasePropertyCompanyForEditOutput;

            if (id.HasValue)
            {
                getEccpBasePropertyCompanyForEditOutput =
                    await this._eccpBasePropertyCompaniesAppService.GetECCPBasePropertyCompanyForEdit(
                        new EntityDto { Id = (int)id });
            }
            else
            {
                getEccpBasePropertyCompanyForEditOutput =
                    new GetECCPBasePropertyCompanyForEditOutput
                    {
                        ECCPBasePropertyCompany = new EditECCPBasePropertyCompanyDto()
                    };
            }

            var viewModel = new CreateOrEditECCPBasePropertyCompanyViewModel
            {
                EccpBasePropertyCompany = getEccpBasePropertyCompanyForEditOutput.ECCPBasePropertyCompany,
                ProvinceName = getEccpBasePropertyCompanyForEditOutput.ProvinceName,
                CityName = getEccpBasePropertyCompanyForEditOutput.CityName,
                DistrictName = getEccpBasePropertyCompanyForEditOutput.DistrictName,
                StreetName = getEccpBasePropertyCompanyForEditOutput.StreetName,
                AptitudePhotoId = getEccpBasePropertyCompanyForEditOutput.AptitudePhotoId,
                BusinessLicenseId = getEccpBasePropertyCompanyForEditOutput.BusinessLicenseId
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
            AppPermissions.Pages_EccpBase_EccpBasePropertyCompanies_Create,
            AppPermissions.Pages_EccpBase_EccpBasePropertyCompanies_Edit)]
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
            var model = new ECCPBasePropertyCompaniesViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp base property company modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpBasePropertyCompanyModal(GetECCPBasePropertyCompanyForView data)
        {
            var model = new ECCPBasePropertyCompanyViewModel
            {
                ECCPBasePropertyCompany = data.ECCPBasePropertyCompany,
                ProvinceName = data.ProvinceName,
                CityName = data.CityName,
                DistrictName = data.DistrictName,
                StreetName = data.StreetName
            };

            return this.PartialView("_ViewECCPBasePropertyCompanyModal", model);
        }
    }
}