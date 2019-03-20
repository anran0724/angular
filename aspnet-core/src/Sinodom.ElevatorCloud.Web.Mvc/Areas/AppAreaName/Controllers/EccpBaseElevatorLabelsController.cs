// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpBaseElevatorLabelsController.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.AspNetCore.Mvc.Authorization;

    using Microsoft.AspNetCore.Mvc;

    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.EccpBaseElevatorLabels;
    using Sinodom.ElevatorCloud.EccpBaseElevatorLabels.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpBaseElevatorLabels;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp base elevator labels controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabels)]
    public class EccpBaseElevatorLabelsController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp base elevator labels app service.
        /// </summary>
        private readonly IEccpBaseElevatorLabelsAppService _eccpBaseElevatorLabelsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpBaseElevatorLabelsController"/> class.
        /// </summary>
        /// <param name="eccpBaseElevatorLabelsAppService">
        /// The eccp base elevator labels app service.
        /// </param>
        public EccpBaseElevatorLabelsController(IEccpBaseElevatorLabelsAppService eccpBaseElevatorLabelsAppService)
        {
            this._eccpBaseElevatorLabelsAppService = eccpBaseElevatorLabelsAppService;
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
            AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabels_Create,
            AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabels_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(long? id)
        {
            GetEccpBaseElevatorLabelForEditOutput getEccpBaseElevatorLabelForEditOutput;

            if (id.HasValue)
            {
                getEccpBaseElevatorLabelForEditOutput =
                    await this._eccpBaseElevatorLabelsAppService.GetEccpBaseElevatorLabelForEdit(
                        new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getEccpBaseElevatorLabelForEditOutput =
                    new GetEccpBaseElevatorLabelForEditOutput
                        {
                            EccpBaseElevatorLabel = new CreateOrEditEccpBaseElevatorLabelDto()
                        };
            }

            var viewModel = new CreateOrEditEccpBaseElevatorLabelViewModel
                                {
                                    EccpBaseElevatorLabel = getEccpBaseElevatorLabelForEditOutput.EccpBaseElevatorLabel,
                                    EccpBaseElevatorName = getEccpBaseElevatorLabelForEditOutput.EccpBaseElevatorName,
                                    EccpDictLabelStatusName =
                                        getEccpBaseElevatorLabelForEditOutput.EccpDictLabelStatusName,
                                    UserName = getEccpBaseElevatorLabelForEditOutput.UserName
                                };

            return this.PartialView("_CreateOrEditModal", viewModel);
        }

        /// <summary>
        /// The eccp base elevator lookup table modal.
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
            AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabels_Create,
            AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabels_Edit)]
        public PartialViewResult EccpBaseElevatorLookupTableModal(Guid? id, string displayName)
        {
            var viewModel = new EccpBaseElevatorLookupTableViewModel
                                {
                                    Id = id.ToString(), DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_EccpBaseElevatorLookupTableModal", viewModel);
        }

        /// <summary>
        /// The eccp dict label status lookup table modal.
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
            AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabels_Create,
            AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabels_Edit)]
        public PartialViewResult EccpDictLabelStatusLookupTableModal(int? id, string displayName)
        {
            var viewModel = new EccpDictLabelStatusLookupTableViewModel
                                {
                                    Id = id, DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_EccpDictLabelStatusLookupTableModal", viewModel);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var model = new EccpBaseElevatorLabelsViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The user lookup table modal.
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
            AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabels_Create,
            AppPermissions.Pages_EccpElevator_EccpBaseElevatorLabels_Edit)]
        public PartialViewResult UserLookupTableModal(long? id, string displayName)
        {
            var viewModel = new UserLookupTableViewModel { Id = id, DisplayName = displayName, FilterText = string.Empty };

            return this.PartialView("_UserLookupTableModal", viewModel);
        }

        /// <summary>
        /// The view eccp base elevator label modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpBaseElevatorLabelModal(GetEccpBaseElevatorLabelForView data)
        {
            var model = new EccpBaseElevatorLabelViewModel
                            {
                                EccpBaseElevatorLabel = data.EccpBaseElevatorLabel,
                                EccpBaseElevatorName = data.EccpBaseElevatorName,
                                EccpDictLabelStatusName = data.EccpDictLabelStatusName,
                                UserName = data.UserName
                            };

            return this.PartialView("_ViewEccpBaseElevatorLabelModal", model);
        }
    }
}