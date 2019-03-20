// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseRegisterCompaniesController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies;
    using Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.ECCPBaseRegisterCompanies;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp base register companies controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpBase_EccpBaseRegisterCompanies)]
    public class ECCPBaseRegisterCompaniesController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp base register companies app service.
        /// </summary>
        private readonly IECCPBaseRegisterCompaniesAppService _eccpBaseRegisterCompaniesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBaseRegisterCompaniesController"/> class.
        /// </summary>
        /// <param name="eccpBaseRegisterCompaniesAppService">
        /// The eccp base register companies app service.
        /// </param>
        public ECCPBaseRegisterCompaniesController(
            IECCPBaseRegisterCompaniesAppService eccpBaseRegisterCompaniesAppService)
        {
            this._eccpBaseRegisterCompaniesAppService = eccpBaseRegisterCompaniesAppService;
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
            AppPermissions.Pages_EccpBase_EccpBaseRegisterCompanies_Create,
            AppPermissions.Pages_EccpBase_EccpBaseRegisterCompanies_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(long? id)
        {
            GetECCPBaseRegisterCompanyForEditOutput getEccpBaseRegisterCompanyForEditOutput;

            if (id.HasValue)
            {
                getEccpBaseRegisterCompanyForEditOutput =
                    await this._eccpBaseRegisterCompaniesAppService.GetECCPBaseRegisterCompanyForEdit(
                        new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getEccpBaseRegisterCompanyForEditOutput =
                    new GetECCPBaseRegisterCompanyForEditOutput
                        {
                            ECCPBaseRegisterCompany = new CreateOrEditECCPBaseRegisterCompanyDto()
                        };
            }

            var viewModel = new CreateOrEditECCPBaseRegisterCompanyViewModel
                                {
                                    EccpBaseRegisterCompany =
                                        getEccpBaseRegisterCompanyForEditOutput.ECCPBaseRegisterCompany,
                                    ProvinceName = getEccpBaseRegisterCompanyForEditOutput.ProvinceName,
                                    CityName = getEccpBaseRegisterCompanyForEditOutput.CityName,
                                    DistrictName = getEccpBaseRegisterCompanyForEditOutput.DistrictName,
                                    StreetName = getEccpBaseRegisterCompanyForEditOutput.StreetName
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
            AppPermissions.Pages_EccpBase_EccpBaseRegisterCompanies_Create,
            AppPermissions.Pages_EccpBase_EccpBaseRegisterCompanies_Edit)]
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
            var model = new ECCPBaseRegisterCompaniesViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp base register company modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpBaseRegisterCompanyModal(GetECCPBaseRegisterCompanyForView data)
        {
            var model = new ECCPBaseRegisterCompanyViewModel
                            {
                                ECCPBaseRegisterCompany = data.ECCPBaseRegisterCompany,
                                ProvinceName = data.ProvinceName,
                                CityName = data.CityName,
                                DistrictName = data.DistrictName,
                                StreetName = data.StreetName
                            };

            return this.PartialView("_ViewECCPBaseRegisterCompanyModal", model);
        }
    }
}