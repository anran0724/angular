// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseProductionCompaniesController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.ECCPBaseProductionCompanies;
    using Sinodom.ElevatorCloud.ECCPBaseProductionCompanies.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.ECCPBaseProductionCompanies;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp base production companies controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpBase_EccpBaseProductionCompanies)]
    public class ECCPBaseProductionCompaniesController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp base production companies app service.
        /// </summary>
        private readonly IECCPBaseProductionCompaniesAppService _eccpBaseProductionCompaniesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBaseProductionCompaniesController"/> class.
        /// </summary>
        /// <param name="eccpBaseProductionCompaniesAppService">
        /// The eccp base production companies app service.
        /// </param>
        public ECCPBaseProductionCompaniesController(
            IECCPBaseProductionCompaniesAppService eccpBaseProductionCompaniesAppService)
        {
            this._eccpBaseProductionCompaniesAppService = eccpBaseProductionCompaniesAppService;
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
            AppPermissions.Pages_EccpBase_EccpBaseProductionCompanies_Create,
            AppPermissions.Pages_EccpBase_EccpBaseProductionCompanies_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(long? id)
        {
            GetECCPBaseProductionCompanyForEditOutput getEccpBaseProductionCompanyForEditOutput;

            if (id.HasValue)
            {
                getEccpBaseProductionCompanyForEditOutput =
                    await this._eccpBaseProductionCompaniesAppService.GetECCPBaseProductionCompanyForEdit(
                        new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getEccpBaseProductionCompanyForEditOutput =
                    new GetECCPBaseProductionCompanyForEditOutput
                        {
                            ECCPBaseProductionCompany = new CreateOrEditECCPBaseProductionCompanyDto()
                        };
            }

            var viewModel = new CreateOrEditECCPBaseProductionCompanyViewModel
                                {
                                    EccpBaseProductionCompany =
                                        getEccpBaseProductionCompanyForEditOutput.ECCPBaseProductionCompany,
                                    ProvinceName = getEccpBaseProductionCompanyForEditOutput.ProvinceName,
                                    CityName = getEccpBaseProductionCompanyForEditOutput.CityName,
                                    DistrictName = getEccpBaseProductionCompanyForEditOutput.DistrictName,
                                    StreetName = getEccpBaseProductionCompanyForEditOutput.StreetName
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
            AppPermissions.Pages_EccpBase_EccpBaseProductionCompanies_Create,
            AppPermissions.Pages_EccpBase_EccpBaseProductionCompanies_Edit)]
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
            var model = new ECCPBaseProductionCompaniesViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp base production company modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpBaseProductionCompanyModal(GetECCPBaseProductionCompanyForView data)
        {
            var model = new ECCPBaseProductionCompanyViewModel
                            {
                                ECCPBaseProductionCompany = data.ECCPBaseProductionCompany,
                                ProvinceName = data.ProvinceName,
                                CityName = data.CityName,
                                DistrictName = data.DistrictName,
                                StreetName = data.StreetName
                            };

            return this.PartialView("_ViewECCPBaseProductionCompanyModal", model);
        }
    }
}