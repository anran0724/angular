// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpBaseElevatorsController.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.AspNetCore.Mvc.Authorization;
    using Abp.Domain.Repositories;

    using Microsoft.AspNetCore.Mvc;

    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.EccpBaseElevators;
    using Sinodom.ElevatorCloud.EccpBaseElevators.Dtos;
    using Sinodom.ElevatorCloud.Editions;
    using Sinodom.ElevatorCloud.MultiTenancy;
    using Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpBaseElevators;
    using Sinodom.ElevatorCloud.Web.Controllers;

    /// <summary>
    /// The eccp base elevators controller.
    /// </summary>
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevators)]
    public class EccpBaseElevatorsController : ElevatorCloudControllerBase
    {
        /// <summary>
        /// The _eccp base elevators app service.
        /// </summary>
        private readonly IEccpBaseElevatorsAppService _eccpBaseElevatorsAppService;

        /// <summary>
        /// The _edition repository.
        /// </summary>
        private readonly IRepository<ECCPEdition, int> _editionRepository;

        /// <summary>
        /// The _tenant repository.
        /// </summary>
        private readonly IRepository<Tenant, int> _tenantRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpBaseElevatorsController"/> class.
        /// </summary>
        /// <param name="eccpBaseElevatorsAppService">
        /// The eccp base elevators app service.
        /// </param>
        /// <param name="editionRepository">
        /// The edition repository.
        /// </param>
        /// <param name="tenantRepository">
        /// The tenant repository.
        /// </param>
        public EccpBaseElevatorsController(
            IEccpBaseElevatorsAppService eccpBaseElevatorsAppService,
            IRepository<ECCPEdition, int> editionRepository,
            IRepository<Tenant, int> tenantRepository)
        {
            this._eccpBaseElevatorsAppService = eccpBaseElevatorsAppService;
            this._editionRepository = editionRepository;
            this._tenantRepository = tenantRepository;
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
            AppPermissions.Pages_EccpElevator_EccpBaseElevators_Create,
            AppPermissions.Pages_EccpElevator_EccpBaseElevators_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(Guid? id)
        {
            GetEccpBaseElevatorForEditOutput getEccpBaseElevatorForEditOutput;

            if (id.HasValue)
            {
                getEccpBaseElevatorForEditOutput =
                    await this._eccpBaseElevatorsAppService.GetEccpBaseElevatorForEdit(
                        new EntityDto<Guid> { Id = (Guid)id });
            }
            else
            {
                getEccpBaseElevatorForEditOutput = new GetEccpBaseElevatorForEditOutput
                                                       {
                                                           EccpBaseElevator = new CreateOrEditEccpBaseElevatorDto(),
                                                           EccpBaseElevatorSubsidiaryInfo =
                                                               new CreateOrEditEccpBaseElevatorSubsidiaryInfoDto()
                                                       };
            }

            var viewModel = new CreateOrEditEccpBaseElevatorViewModel
                                {
                                    EccpBaseElevator = getEccpBaseElevatorForEditOutput.EccpBaseElevator,
                                    EccpDictPlaceTypeName = getEccpBaseElevatorForEditOutput.EccpDictPlaceTypeName,
                                    EccpDictElevatorTypeName =
                                        getEccpBaseElevatorForEditOutput.EccpDictElevatorTypeName,
                                    EccpDictElevatorStatusName =
                                        getEccpBaseElevatorForEditOutput.ECCPDictElevatorStatusName,
                                    EccpBaseCommunityName = getEccpBaseElevatorForEditOutput.ECCPBaseCommunityName,
                                    EccpBaseMaintenanceCompanyName =
                                        getEccpBaseElevatorForEditOutput.ECCPBaseMaintenanceCompanyName,
                                    EccpBaseAnnualInspectionUnitName =
                                        getEccpBaseElevatorForEditOutput.ECCPBaseAnnualInspectionUnitName,
                                    EccpBaseRegisterCompanyName =
                                        getEccpBaseElevatorForEditOutput.ECCPBaseRegisterCompanyName,
                                    EccpBaseProductionCompanyName =
                                        getEccpBaseElevatorForEditOutput.ECCPBaseProductionCompanyName,
                                    EccpBaseElevatorBrandName =
                                        getEccpBaseElevatorForEditOutput.EccpBaseElevatorBrandName,
                                    EccpBaseElevatorModelName =
                                        getEccpBaseElevatorForEditOutput.EccpBaseElevatorModelName,
                                    EccpBasePropertyCompanyName =
                                        getEccpBaseElevatorForEditOutput.ECCPBasePropertyCompanyName,
                                    ProvinceName = getEccpBaseElevatorForEditOutput.ProvinceName,
                                    CityName = getEccpBaseElevatorForEditOutput.CityName,
                                    DistrictName = getEccpBaseElevatorForEditOutput.DistrictName,
                                    StreetName = getEccpBaseElevatorForEditOutput.StreetName,
                                    EccpBaseElevatorSubsidiaryInfo =
                                        getEccpBaseElevatorForEditOutput.EccpBaseElevatorSubsidiaryInfo
                                };
            this.ViewBag.type = 0;
            if (this.AbpSession.TenantId != null)
            {
                var tenant = this._tenantRepository.Get(this.AbpSession.TenantId.Value);

                if (tenant.EditionId != null)
                {
                    var edition = this._editionRepository.Get(tenant.EditionId.Value);

                    this.ViewBag.type = edition.ECCPEditionsTypeId;
                }
            }

            return this.PartialView("_CreateOrEditModal", viewModel);
        }

        /// <summary>
        /// The eccp base annual inspection unit lookup table modal.
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
            AppPermissions.Pages_EccpElevator_EccpBaseElevators_Create,
            AppPermissions.Pages_EccpElevator_EccpBaseElevators_Edit)]
        public PartialViewResult EccpBaseAnnualInspectionUnitLookupTableModal(long? id, string displayName)
        {
            var viewModel = new ECCPBaseAnnualInspectionUnitLookupTableViewModel
                                {
                                    Id = id, DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_ECCPBaseAnnualInspectionUnitLookupTableModal", viewModel);
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
            AppPermissions.Pages_EccpElevator_EccpBaseElevators_Create,
            AppPermissions.Pages_EccpElevator_EccpBaseElevators_Edit)]
        public PartialViewResult EccpBaseAreaLookupTableModal(int? id, string displayName)
        {
            var viewModel = new ECCPBaseAreaLookupTableViewModel
                                {
                                    Id = id, DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_ECCPBaseAreaLookupTableModal", viewModel);
        }

        /// <summary>
        /// The eccp base community lookup table modal.
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
            AppPermissions.Pages_EccpElevator_EccpBaseElevators_Create,
            AppPermissions.Pages_EccpElevator_EccpBaseElevators_Edit)]
        public PartialViewResult EccpBaseCommunityLookupTableModal(long? id, string displayName)
        {
            var viewModel = new ECCPBaseCommunityLookupTableViewModel
                                {
                                    Id = id, DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_ECCPBaseCommunityLookupTableModal", viewModel);
        }

        /// <summary>
        /// The eccp base elevator brand lookup table modal.
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
            AppPermissions.Pages_EccpElevator_EccpBaseElevators_Create,
            AppPermissions.Pages_EccpElevator_EccpBaseElevators_Edit)]
        public PartialViewResult EccpBaseElevatorBrandLookupTableModal(int? id, string displayName)
        {
            var viewModel = new EccpBaseElevatorBrandLookupTableViewModel
                                {
                                    Id = id, DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_EccpBaseElevatorBrandLookupTableModal", viewModel);
        }

        /// <summary>
        /// The eccp base elevator model lookup table modal.
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
            AppPermissions.Pages_EccpElevator_EccpBaseElevators_Create,
            AppPermissions.Pages_EccpElevator_EccpBaseElevators_Edit)]
        public PartialViewResult EccpBaseElevatorModelLookupTableModal(int? id, string displayName)
        {
            var viewModel = new EccpBaseElevatorModelLookupTableViewModel
                                {
                                    Id = id, DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_EccpBaseElevatorModelLookupTableModal", viewModel);
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
            AppPermissions.Pages_EccpElevator_EccpBaseElevators_Create,
            AppPermissions.Pages_EccpElevator_EccpBaseElevators_Edit)]
        public PartialViewResult EccpBaseMaintenanceCompanyLookupTableModal(int? id, string displayName)
        {
            var viewModel = new ECCPBaseMaintenanceCompanyLookupTableViewModel
                                {
                                    Id = id, DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_ECCPBaseMaintenanceCompanyLookupTableModal", viewModel);
        }

        /// <summary>
        /// The eccp base production company lookup table modal.
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
            AppPermissions.Pages_EccpElevator_EccpBaseElevators_Create,
            AppPermissions.Pages_EccpElevator_EccpBaseElevators_Edit)]
        public PartialViewResult EccpBaseProductionCompanyLookupTableModal(long? id, string displayName)
        {
            var viewModel = new ECCPBaseProductionCompanyLookupTableViewModel
                                {
                                    Id = id, DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_ECCPBaseProductionCompanyLookupTableModal", viewModel);
        }

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
        [AbpMvcAuthorize(
            AppPermissions.Pages_EccpElevator_EccpBaseElevators_Create,
            AppPermissions.Pages_EccpElevator_EccpBaseElevators_Edit)]
        public PartialViewResult EccpBasePropertyCompanyLookupTableModal(int? id, string displayName)
        {
            var viewModel = new ECCPBasePropertyCompanyLookupTableViewModel
                                {
                                    Id = id, DisplayName = displayName, FilterText = string.Empty
                                };
            return this.PartialView("_ECCPBasePropertyCompanyLookupTableModal", viewModel);
        }

        /// <summary>
        /// The eccp base register company lookup table modal.
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
            AppPermissions.Pages_EccpElevator_EccpBaseElevators_Create,
            AppPermissions.Pages_EccpElevator_EccpBaseElevators_Edit)]
        public PartialViewResult EccpBaseRegisterCompanyLookupTableModal(long? id, string displayName)
        {
            var viewModel = new ECCPBaseRegisterCompanyLookupTableViewModel
                                {
                                    Id = id, DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_ECCPBaseRegisterCompanyLookupTableModal", viewModel);
        }

        /// <summary>
        /// The eccp dict elevator status lookup table modal.
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
            AppPermissions.Pages_EccpElevator_EccpBaseElevators_Create,
            AppPermissions.Pages_EccpElevator_EccpBaseElevators_Edit)]
        public PartialViewResult EccpDictElevatorStatusLookupTableModal(int? id, string displayName)
        {
            var viewModel = new ECCPDictElevatorStatusLookupTableViewModel
                                {
                                    Id = id, DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_ECCPDictElevatorStatusLookupTableModal", viewModel);
        }

        /// <summary>
        /// The eccp dict elevator type lookup table modal.
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
            AppPermissions.Pages_EccpElevator_EccpBaseElevators_Create,
            AppPermissions.Pages_EccpElevator_EccpBaseElevators_Edit)]
        public PartialViewResult EccpDictElevatorTypeLookupTableModal(int? id, string displayName)
        {
            var viewModel = new EccpDictElevatorTypeLookupTableViewModel
                                {
                                    Id = id, DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_EccpDictElevatorTypeLookupTableModal", viewModel);
        }

        /// <summary>
        /// The eccp dict place type lookup table modal.
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
            AppPermissions.Pages_EccpElevator_EccpBaseElevators_Create,
            AppPermissions.Pages_EccpElevator_EccpBaseElevators_Edit)]
        public PartialViewResult EccpDictPlaceTypeLookupTableModal(int? id, string displayName)
        {
            var viewModel = new EccpDictPlaceTypeLookupTableViewModel
                                {
                                    Id = id, DisplayName = displayName, FilterText = string.Empty
                                };

            return this.PartialView("_EccpDictPlaceTypeLookupTableModal", viewModel);
        }

        /// <summary>
        /// The eccp maintenance work order evaluations table modal.
        /// </summary>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult EccpMaintenanceWorkOrderEvaluationsTableModal()
        {
            return this.PartialView("_EccpMaintenanceWorkOrderEvaluationsTableModal");
        }

        public PartialViewResult EccpMaintenanceWorkOrdersTableModal()
        {
            return this.PartialView("_EccpMaintenanceWorkOrdersTableModal");
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var model = new EccpBaseElevatorsViewModel { FilterText = string.Empty };

            return this.View(model);
        }

        /// <summary>
        /// The view eccp base elevator modal.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult ViewEccpBaseElevatorModal(GetEccpBaseElevatorForView data)
        {
            var elevatorSubsidiaryInfoDto =
                this._eccpBaseElevatorsAppService.GetEccpBaseElevatorSubsidiaryInfoDto(
                    new EntityDto<Guid> { Id = data.EccpBaseElevator.Id });

           var getChangeLogByElevatorIdDto=this._eccpBaseElevatorsAppService.GetAllChangeLogByElevatorId(data.EccpBaseElevator.Id.ToString());


            var model = new EccpBaseElevatorViewModel
                            {
                                EccpBaseElevator = data.EccpBaseElevator,
                                EccpBaseElevatorSubsidiaryInfo = elevatorSubsidiaryInfoDto,
                                EccpDictPlaceTypeName = data.EccpDictPlaceTypeName,
                                EccpDictElevatorTypeName = data.EccpDictElevatorTypeName,
                                ECCPDictElevatorStatusName = data.ECCPDictElevatorStatusName,
                                ECCPBaseCommunityName = data.ECCPBaseCommunityName,
                                ECCPBasePropertyCompanyName = data.ECCPBasePropertyCompanyName,
                                ECCPBaseMaintenanceCompanyName = data.ECCPBaseMaintenanceCompanyName,
                                ECCPBaseAnnualInspectionUnitName = data.ECCPBaseAnnualInspectionUnitName,
                                ECCPBaseRegisterCompanyName = data.ECCPBaseRegisterCompanyName,
                                ECCPBaseProductionCompanyName = data.ECCPBaseProductionCompanyName,
                                EccpBaseElevatorBrandName = data.EccpBaseElevatorBrandName,
                                EccpBaseElevatorModelName = data.EccpBaseElevatorModelName,
                                ProvinceName = data.ProvinceName,
                                CityName = data.CityName,
                                DistrictName = data.DistrictName,
                                StreetName = data.StreetName,
                                EccpBaseElevatorChangeLogList = getChangeLogByElevatorIdDto
            };

            return this.PartialView("_ViewEccpBaseElevatorModal", model);
        }
    }
}