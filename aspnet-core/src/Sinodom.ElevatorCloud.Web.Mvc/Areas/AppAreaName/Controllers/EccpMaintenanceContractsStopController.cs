// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceContractsController.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Sinodom.ElevatorCloud.Storage;

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Controllers
{
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.AspNetCore.Mvc.Authorization;

    using Microsoft.AspNetCore.Mvc;

    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.EccpMaintenanceContracts;
    using Sinodom.ElevatorCloud.EccpMaintenanceContracts.Dtos;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpMaintenanceContracts;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp maintenance contracts controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceContractsStop)]
    public class EccpMaintenanceContractsStopController : ProfileControllerBase
    {
        /// <summary>
        /// The _eccp maintenance contracts app service.
        /// </summary>
        private readonly IEccpMaintenanceContractsStopAppService _eccpMaintenanceContractsStopAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceContractsController"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceContractsAppService">
        /// The eccp maintenance contracts app service.
        /// </param>
        public EccpMaintenanceContractsStopController(
            IEccpMaintenanceContractsStopAppService eccpMaintenanceContractsStopAppService,
            ITempFileCacheManager tempFileCacheManager) : base(tempFileCacheManager)
        {
            this._eccpMaintenanceContractsStopAppService = eccpMaintenanceContractsStopAppService;
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
                await this._eccpMaintenanceContractsStopAppService.GetEccpMaintenanceContractForRecoveryContract(
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
        [AbpMvcAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceContracts_StopContract)]
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
        [AbpMvcAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceContracts_StopContract)]
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


        [AbpMvcAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceContracts_StopContract)]
        public async Task<PartialViewResult> RecoveryContractModal(long? id, List<ECCPBaseElevatorLookupTableDto> elevators)
        {
            GetEccpMaintenanceContractForEditOutput getEccpMaintenanceContractForEditOutput;

            if (id.HasValue)
            {
                getEccpMaintenanceContractForEditOutput =
                    await this._eccpMaintenanceContractsStopAppService.GetEccpMaintenanceContractForRecoveryContract(
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
                EccpBaseElevatorsNames = string.Join(",", elevators.Select(e => e.DisplayName)),
                EccpBaseElevatorsIds = string.Join(",", elevators.Select(e => e.Id))
            };

            return this.PartialView("_RecoveryContractModal", viewModel);
        }
    }
}