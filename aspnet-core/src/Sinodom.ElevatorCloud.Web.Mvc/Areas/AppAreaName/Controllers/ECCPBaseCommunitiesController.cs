// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPBaseCommunitiesController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.ECCPBaseCommunities;
    using Sinodom.ElevatorCloud.ECCPBaseCommunities.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.ECCPBaseCommunities;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp base communities controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpBase_EccpBaseCommunities)]
    public class ECCPBaseCommunitiesController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp base communities app service.
        /// </summary>
        private readonly IECCPBaseCommunitiesAppService _eccpBaseCommunitiesAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPBaseCommunitiesController"/> class.
        /// </summary>
        /// <param name="eccpBaseCommunitiesAppService">
        /// The eccp base communities app service.
        /// </param>
        public ECCPBaseCommunitiesController(IECCPBaseCommunitiesAppService eccpBaseCommunitiesAppService)
        {
            this._eccpBaseCommunitiesAppService = eccpBaseCommunitiesAppService;
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
            AppPermissions.Pages_EccpBase_EccpBaseCommunities_Create,
            AppPermissions.Pages_EccpBase_EccpBaseCommunities_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(long? id)
        {
            GetECCPBaseCommunityForEditOutput getEccpBaseCommunityForEditOutput;

            if (id.HasValue)
            {
                getEccpBaseCommunityForEditOutput =
                    await this._eccpBaseCommunitiesAppService.GetECCPBaseCommunityForEdit(
                        new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getEccpBaseCommunityForEditOutput =
                    new GetECCPBaseCommunityForEditOutput
                        {
                            ECCPBaseCommunity = new CreateOrEditECCPBaseCommunityDto()
                        };
            }

            var viewModel = new CreateOrEditECCPBaseCommunityViewModel
                                {
                                    EccpBaseCommunity = getEccpBaseCommunityForEditOutput.ECCPBaseCommunity,
                                    ProvinceName = getEccpBaseCommunityForEditOutput.ProvinceName,
                                    CityName = getEccpBaseCommunityForEditOutput.CityName,
                                    DistrictName = getEccpBaseCommunityForEditOutput.DistrictName,
                                    StreetName = getEccpBaseCommunityForEditOutput.StreetName
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
            AppPermissions.Pages_EccpBase_EccpBaseCommunities_Create,
            AppPermissions.Pages_EccpBase_EccpBaseCommunities_Edit)]
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
            var model = new ECCPBaseCommunitiesViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp base community modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpBaseCommunityModal(GetECCPBaseCommunityForView data)
        {
            var model = new ECCPBaseCommunityViewModel
                            {
                                ECCPBaseCommunity = data.ECCPBaseCommunity,
                                ProvinceName = data.ProvinceName,
                                CityName = data.CityName,
                                DistrictName = data.DistrictName,
                                StreetName = data.StreetName
                            };

            return this.PartialView("_ViewECCPBaseCommunityModal", model);
        }
    }
}