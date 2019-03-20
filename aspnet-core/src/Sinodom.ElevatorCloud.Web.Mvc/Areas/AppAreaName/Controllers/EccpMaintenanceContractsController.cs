// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceContractsController.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.EccpMaintenanceContracts;
    using Sinodom.ElevatorCloud.EccpMaintenanceContracts.Dtos;
    using Sinodom.ElevatorCloud.Storage;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceContracts;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp maintenance contracts controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceContracts)]
    public class EccpMaintenanceContractsController : ProfileControllerBase
    {
        /// <summary>
        /// The _eccp maintenance contracts app service.
        /// </summary>
        private readonly IEccpMaintenanceContractsAppService _eccpMaintenanceContractsAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceContractsController"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceContractsAppService">
        /// The eccp maintenance contracts app service.
        /// </param>
        /// <param name="tempFileCacheManager">
        /// The temp file cache manager.
        /// </param>
        public EccpMaintenanceContractsController(
            IEccpMaintenanceContractsAppService eccpMaintenanceContractsAppService,
            ITempFileCacheManager tempFileCacheManager)
            : base(tempFileCacheManager)
        {
            this._eccpMaintenanceContractsAppService = eccpMaintenanceContractsAppService;
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
            AppPermissions.Pages_Administration_EccpMaintenanceContracts_Create,
            AppPermissions.Pages_Administration_EccpMaintenanceContracts_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(long? id)
        {
            GetEccpMaintenanceContractForEditOutput getEccpMaintenanceContractForEditOutput;

            if (id.HasValue)
            {
                getEccpMaintenanceContractForEditOutput =
                    await this._eccpMaintenanceContractsAppService.GetEccpMaintenanceContractForEdit(
                        new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getEccpMaintenanceContractForEditOutput = new GetEccpMaintenanceContractForEditOutput
                {
                    EccpMaintenanceContract =
                                                                      new CreateOrEditEccpMaintenanceContractDto
                                                                      {
                                                                          StartDate = DateTime.Now,
                                                                          EndDate = DateTime.Now
                                                                      }
                };
            }

            var viewModel = new CreateOrEditEccpMaintenanceContractViewModel
            {
                EccpMaintenanceContract =
                                        getEccpMaintenanceContractForEditOutput.EccpMaintenanceContract,
                EccpBaseMaintenanceCompanyName =
                                        getEccpMaintenanceContractForEditOutput.ECCPBaseMaintenanceCompanyName,
                EccpBasePropertyCompanyName =
                                        getEccpMaintenanceContractForEditOutput.ECCPBasePropertyCompanyName,
                EccpBaseElevatorsNames =
                                        getEccpMaintenanceContractForEditOutput.EccpBaseElevatorsNames,
                EccpBaseElevatorsIds = getEccpMaintenanceContractForEditOutput.EccpBaseElevatorsIds
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
            AppPermissions.Pages_Administration_EccpMaintenanceContracts_Create,
            AppPermissions.Pages_Administration_EccpMaintenanceContracts_Edit)]
        public PartialViewResult EccpBaseElevatorLookupTableModal(string id, string displayName)
        {
            var viewModel = new ECCPBaseElevatorLookupTableViewModel
            {
                Id = id,
                DisplayName = displayName,
                FilterText = string.Empty
            };

            return this.PartialView("_EccpBaseElevatorLookupTableModal", viewModel);
        }

        /// <summary>
        /// The eccp base maintenance company lookup table modal.
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
            AppPermissions.Pages_Administration_EccpMaintenanceContracts_Create,
            AppPermissions.Pages_Administration_EccpMaintenanceContracts_Edit)]
        public PartialViewResult ECCPBaseMaintenanceCompanyLookupTableModal(int? id, string displayName)
        {
            var viewModel = new ECCPBaseMaintenanceCompanyLookupTableViewModel
            {
                Id = id,
                DisplayName = displayName,
                FilterText = string.Empty
            };

            return this.PartialView("_ECCPBaseMaintenanceCompanyLookupTableModal", viewModel);
        }

        // [AbpMvcAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceContracts_Create, AppPermissions.Pages_Administration_EccpMaintenanceContracts_Edit)]
        /// <summary>
        /// The eccp base property company lookup table modal.
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
        public PartialViewResult ECCPBasePropertyCompanyLookupTableModal(int? id, string displayName)
        {
            var viewModel = new ECCPBasePropertyCompanyLookupTableViewModel
            {
                Id = id,
                DisplayName = displayName,
                FilterText = string.Empty
            };

            return this.PartialView("_ECCPBasePropertyCompanyLookupTableModal", viewModel);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var model = new EccpMaintenanceContractsViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp maintenance contract modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public async Task<PartialViewResult> ViewEccpMaintenanceContractModal(GetEccpMaintenanceContractForView data)
        {
            GetEccpMaintenanceContractForEditOutput getEccpMaintenanceContractForEditOutput =
                await this._eccpMaintenanceContractsAppService.GetEccpMaintenanceContractForEdit(
                    new EntityDto<long> { Id = (long)data.EccpMaintenanceContract.Id });

            var model = new EccpMaintenanceContractViewModel
            {
                GetEccpMaintenanceContractForEdit = getEccpMaintenanceContractForEditOutput,
                EccpMaintenanceContract = data.EccpMaintenanceContract,
                ECCPBaseMaintenanceCompanyName = data.ECCPBaseMaintenanceCompanyName,
                ECCPBasePropertyCompanyName = data.ECCPBasePropertyCompanyName
            };

            return this.PartialView("_ViewEccpMaintenanceContractModal", model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceContracts_StopContract)]
        public async Task<PartialViewResult> StopContractModal(long? id)
        {
            GetEccpMaintenanceContractForEditOutput getEccpMaintenanceContractForEditOutput =
                    await this._eccpMaintenanceContractsAppService.GetEccpMaintenanceContractForEdit(
                        new EntityDto<long> { Id = (long)id });

            var viewModel = new CreateOrEditEccpMaintenanceContractViewModel
            {
                EccpMaintenanceContract =
                    getEccpMaintenanceContractForEditOutput.EccpMaintenanceContract
            };

            return this.PartialView("_StopContractModal", viewModel);
        }
    }
}