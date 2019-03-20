// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpBaseElevatorsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.Authorization;
    using Abp.AutoMapper;
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Abp.Linq.Extensions;
    using Abp.UI;

    using Microsoft.EntityFrameworkCore;

    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.ECCPBaseAnnualInspectionUnits;
    using Sinodom.ElevatorCloud.ECCPBaseAreas;
    using Sinodom.ElevatorCloud.ECCPBaseCommunities;
    using Sinodom.ElevatorCloud.EccpBaseElevatorBrands;
    using Sinodom.ElevatorCloud.EccpBaseElevatorModels;
    using Sinodom.ElevatorCloud.EccpBaseElevators.Dtos;
    using Sinodom.ElevatorCloud.EccpBaseElevators.Exporting;
    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
    using Sinodom.ElevatorCloud.ECCPBaseProductionCompanies;
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;
    using Sinodom.ElevatorCloud.ECCPBaseRegisterCompanies;
    using Sinodom.ElevatorCloud.EccpDict;
    using Sinodom.ElevatorCloud.EccpMaintenanceContracts;
    using Sinodom.ElevatorCloud.EccpMaintenancePlans;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders;
    using Sinodom.ElevatorCloud.Editions;
    using Sinodom.ElevatorCloud.MultiTenancy;

    /// <summary>
    ///     The eccp base elevators app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevators)]
    public class EccpBaseElevatorsAppService : ElevatorCloudAppServiceBase, IEccpBaseElevatorsAppService
    {
        /// <summary>
        ///     The eccp base annual inspection unit repository.
        /// </summary>
        private readonly IRepository<ECCPBaseAnnualInspectionUnit, long> _eccpBaseAnnualInspectionUnitRepository;

        /// <summary>
        ///     The eccp base area repository.
        /// </summary>
        private readonly IRepository<ECCPBaseArea, int> _eccpBaseAreaRepository;

        /// <summary>
        ///     The eccp base community repository.
        /// </summary>
        private readonly IRepository<ECCPBaseCommunity, long> _eccpBaseCommunityRepository;

        /// <summary>
        ///     The eccp base elevator brand repository.
        /// </summary>
        private readonly IRepository<EccpBaseElevatorBrand, int> _eccpBaseElevatorBrandRepository;

        /// <summary>
        ///     The eccp base elevator model repository.
        /// </summary>
        private readonly IRepository<EccpBaseElevatorModel, int> _eccpBaseElevatorModelRepository;

        /// <summary>
        ///     The eccp base elevator repository.
        /// </summary>
        private readonly IRepository<EccpBaseElevator, Guid> _eccpBaseElevatorRepository;

        /// <summary>
        ///     The eccp base elevators excel exporter.
        /// </summary>
        private readonly IEccpBaseElevatorsExcelExporter _eccpBaseElevatorsExcelExporter;

        /// <summary>
        ///     The eccp base elevator subsidiary info repository.
        /// </summary>
        private readonly IRepository<EccpBaseElevatorSubsidiaryInfo, Guid> _eccpBaseElevatorSubsidiaryInfoRepository;

        /// <summary>
        ///     The eccp base maintenance company repository.
        /// </summary>
        private readonly IRepository<ECCPBaseMaintenanceCompany, int> _eccpBaseMaintenanceCompanyRepository;

        /// <summary>
        ///     The eccp base production company repository.
        /// </summary>
        private readonly IRepository<ECCPBaseProductionCompany, long> _eccpBaseProductionCompanyRepository;

        /// <summary>
        ///     The eccp base property company repository.
        /// </summary>
        private readonly IRepository<ECCPBasePropertyCompany, int> _eccpBasePropertyCompanyRepository;

        /// <summary>
        ///     The eccp base register company repository.
        /// </summary>
        private readonly IRepository<ECCPBaseRegisterCompany, long> _eccpBaseRegisterCompanyRepository;

        /// <summary>
        ///     The eccp dict elevator status repository.
        /// </summary>
        private readonly IRepository<ECCPDictElevatorStatus, int> _eccpDictElevatorStatusRepository;

        /// <summary>
        ///     The eccp dict elevator type repository.
        /// </summary>
        private readonly IRepository<EccpDictElevatorType, int> _eccpDictElevatorTypeRepository;

        /// <summary>
        ///     The eccp dict place type repository.
        /// </summary>
        private readonly IRepository<EccpDictPlaceType, int> _eccpDictPlaceTypeRepository;

        /// <summary>
        ///     The eccp elevator change log repository.
        /// </summary>
        private readonly IRepository<EccpElevatorChangeLog, int> _eccpElevatorChangeLogRepository;

        /// <summary>
        /// The _eccp maintenance contract.
        /// </summary>
        private readonly IRepository<EccpMaintenanceContract, long> _eccpMaintenanceContract;

        /// <summary>
        /// The _eccp maintenance contract elevator link.
        /// </summary>
        private readonly IRepository<EccpMaintenanceContract_Elevator_Link, long> _eccpMaintenanceContractElevatorLink;

        /// <summary>
        ///     The eccp maintenance plan repository.
        /// </summary>
        private readonly IRepository<EccpMaintenancePlan, int> _eccpMaintenancePlanRepository;

        /// <summary>
        ///     The eccp maintenance work order evaluation repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkOrderEvaluation> _eccpMaintenanceWorkOrderEvaluationRepository;

        /// <summary>
        ///     The eccp maintenance work order repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkOrder> _eccpMaintenanceWorkOrderRepository;

        /// <summary>
        ///     The edition repository.
        /// </summary>
        private readonly IRepository<ECCPEdition, int> _editionRepository;

        /// <summary>
        ///     The _elevator claim logs app service.
        /// </summary>
        private readonly IElevatorClaimLogsAppService _elevatorClaimLogsAppService;

        /// <summary>
        ///     The tenant repository.
        /// </summary>
        private readonly IRepository<Tenant, int> _tenantRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpBaseElevatorsAppService"/> class.
        /// </summary>
        /// <param name="eccpBaseElevatorRepository">
        /// The eccp base elevator repository.
        /// </param>
        /// <param name="eccpBaseElevatorsExcelExporter">
        /// The eccp base elevators excel exporter.
        /// </param>
        /// <param name="eccpDictPlaceTypeRepository">
        /// The eccp dict place type repository.
        /// </param>
        /// <param name="eccpDictElevatorTypeRepository">
        /// The eccp dict elevator type repository.
        /// </param>
        /// <param name="eccpDictElevatorStatusRepository">
        /// The eccp dict elevator status repository.
        /// </param>
        /// <param name="eccpBaseCommunityRepository">
        /// The eccp base community repository.
        /// </param>
        /// <param name="eccpBaseMaintenanceCompanyRepository">
        /// The eccp base maintenance company repository.
        /// </param>
        /// <param name="eccpBasePropertyCompanyRepository">
        /// The eccp base property company repository.
        /// </param>
        /// <param name="eccpBaseAnnualInspectionUnitRepository">
        /// The eccp base annual inspection unit repository.
        /// </param>
        /// <param name="eccpBaseRegisterCompanyRepository">
        /// The eccp base register company repository.
        /// </param>
        /// <param name="eccpBaseProductionCompanyRepository">
        /// The eccp base production company repository.
        /// </param>
        /// <param name="eccpBaseElevatorBrandRepository">
        /// The eccp base elevator brand repository.
        /// </param>
        /// <param name="eccpBaseElevatorModelRepository">
        /// The eccp base elevator model repository.
        /// </param>
        /// <param name="eccpBaseAreaRepository">
        /// The eccp base area repository.
        /// </param>
        /// <param name="eccpBaseElevatorSubsidiaryInfoRepository">
        /// The eccp base elevator subsidiary info repository.
        /// </param>
        /// <param name="eccpElevatorChangeLogRepository">
        /// The eccp elevator change log repository.
        /// </param>
        /// <param name="tenantRepository">
        /// The tenant repository.
        /// </param>
        /// <param name="eccpMaintenancePlanRepository">
        /// The eccp maintenance plan repository.
        /// </param>
        /// <param name="eccpMaintenanceWorkOrderRepository">
        /// The eccp maintenance work order repository.
        /// </param>
        /// <param name="eccpMaintenanceWorkOrderEvaluationRepository">
        /// The eccp maintenance work order evaluation repository.
        /// </param>
        /// <param name="editionRepository">
        /// The edition repository.
        /// </param>
        /// <param name="eccpMaintenanceContractElevatorLink">
        /// The eccp Maintenance Contract Elevator Link.
        /// </param>
        /// <param name="eccpMaintenanceContract">
        /// The eccp Maintenance Contract.
        /// </param>
        /// <param name="elevatorClaimLogsAppService">
        /// The elevator Claim Logs App Service.
        /// </param>
        public EccpBaseElevatorsAppService(
            IRepository<EccpBaseElevator, Guid> eccpBaseElevatorRepository,
            IEccpBaseElevatorsExcelExporter eccpBaseElevatorsExcelExporter,
            IRepository<EccpDictPlaceType, int> eccpDictPlaceTypeRepository,
            IRepository<EccpDictElevatorType, int> eccpDictElevatorTypeRepository,
            IRepository<ECCPDictElevatorStatus, int> eccpDictElevatorStatusRepository,
            IRepository<ECCPBaseCommunity, long> eccpBaseCommunityRepository,
            IRepository<ECCPBaseMaintenanceCompany, int> eccpBaseMaintenanceCompanyRepository,
            IRepository<ECCPBasePropertyCompany, int> eccpBasePropertyCompanyRepository,
            IRepository<ECCPBaseAnnualInspectionUnit, long> eccpBaseAnnualInspectionUnitRepository,
            IRepository<ECCPBaseRegisterCompany, long> eccpBaseRegisterCompanyRepository,
            IRepository<ECCPBaseProductionCompany, long> eccpBaseProductionCompanyRepository,
            IRepository<EccpBaseElevatorBrand, int> eccpBaseElevatorBrandRepository,
            IRepository<EccpBaseElevatorModel, int> eccpBaseElevatorModelRepository,
            IRepository<ECCPBaseArea, int> eccpBaseAreaRepository,
            IRepository<EccpBaseElevatorSubsidiaryInfo, Guid> eccpBaseElevatorSubsidiaryInfoRepository,
            IRepository<EccpElevatorChangeLog, int> eccpElevatorChangeLogRepository,
            IRepository<Tenant, int> tenantRepository,
            IRepository<EccpMaintenancePlan, int> eccpMaintenancePlanRepository,
            IRepository<EccpMaintenanceWorkOrder> eccpMaintenanceWorkOrderRepository,
            IRepository<EccpMaintenanceWorkOrderEvaluation> eccpMaintenanceWorkOrderEvaluationRepository,
            IRepository<ECCPEdition, int> editionRepository,
            IRepository<EccpMaintenanceContract_Elevator_Link, long> eccpMaintenanceContractElevatorLink,
            IRepository<EccpMaintenanceContract, long> eccpMaintenanceContract,
            IElevatorClaimLogsAppService elevatorClaimLogsAppService)
        {
            this._eccpBaseElevatorRepository = eccpBaseElevatorRepository;
            this._eccpBaseElevatorsExcelExporter = eccpBaseElevatorsExcelExporter;
            this._eccpDictPlaceTypeRepository = eccpDictPlaceTypeRepository;
            this._eccpDictElevatorTypeRepository = eccpDictElevatorTypeRepository;
            this._eccpDictElevatorStatusRepository = eccpDictElevatorStatusRepository;
            this._eccpBaseCommunityRepository = eccpBaseCommunityRepository;
            this._eccpBaseMaintenanceCompanyRepository = eccpBaseMaintenanceCompanyRepository;
            this._eccpBasePropertyCompanyRepository = eccpBasePropertyCompanyRepository;
            this._eccpBaseAnnualInspectionUnitRepository = eccpBaseAnnualInspectionUnitRepository;
            this._eccpBaseRegisterCompanyRepository = eccpBaseRegisterCompanyRepository;
            this._eccpBaseProductionCompanyRepository = eccpBaseProductionCompanyRepository;
            this._eccpBaseElevatorBrandRepository = eccpBaseElevatorBrandRepository;
            this._eccpBaseElevatorModelRepository = eccpBaseElevatorModelRepository;
            this._eccpBaseAreaRepository = eccpBaseAreaRepository;
            this._eccpBaseElevatorSubsidiaryInfoRepository = eccpBaseElevatorSubsidiaryInfoRepository;
            this._eccpElevatorChangeLogRepository = eccpElevatorChangeLogRepository;
            this._tenantRepository = tenantRepository;
            this._eccpMaintenancePlanRepository = eccpMaintenancePlanRepository;
            this._eccpMaintenanceWorkOrderRepository = eccpMaintenanceWorkOrderRepository;
            this._eccpMaintenanceWorkOrderEvaluationRepository = eccpMaintenanceWorkOrderEvaluationRepository;
            this._editionRepository = editionRepository;
            this._eccpMaintenanceContractElevatorLink = eccpMaintenanceContractElevatorLink;
            this._eccpMaintenanceContract = eccpMaintenanceContract;
            this._elevatorClaimLogsAppService = elevatorClaimLogsAppService;
        }

        /// <summary>
        /// The claim elevators.
        ///     电梯认领
        /// </summary>
        /// <param name="ids">
        /// The ids.
        /// </param>
        /// <param name="eccpBaseMaintenanceCompanyId">
        /// The eccp base maintenance company id.
        /// </param>
        /// <param name="propertyCompanyId">
        /// The property company id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task ClaimElevators(
            List<Guid> ids,
            int? eccpBaseMaintenanceCompanyId = null,
            int? propertyCompanyId = null)
        {
            var tenantModel = await this._tenantRepository.GetAsync(this.AbpSession.TenantId.GetValueOrDefault(0));
            var edition = this._editionRepository.Get(tenantModel.EditionId.GetValueOrDefault(0));

            if (edition.ECCPEditionsTypeId == 2)
            {
                eccpBaseMaintenanceCompanyId = this._eccpBaseMaintenanceCompanyRepository
                    .FirstOrDefault(e => e.TenantId == this.AbpSession.TenantId.Value).Id;
            }
            else if (edition.ECCPEditionsTypeId == 3)
            {
                propertyCompanyId = this._eccpBasePropertyCompanyRepository
                    .FirstOrDefault(e => e.TenantId == this.AbpSession.TenantId.Value).Id;
            }

            var list = this._eccpBaseElevatorRepository.GetAll().Where(e => ids.Contains(e.Id));

            if (list.Count() != 0)
            {
                foreach (var item in list)
                {
                    if (eccpBaseMaintenanceCompanyId != 0)
                    {
                        item.ECCPBaseMaintenanceCompanyId = eccpBaseMaintenanceCompanyId;
                    }

                    if (propertyCompanyId != 0)
                    {
                        item.ECCPBasePropertyCompanyId = propertyCompanyId;
                    }

                    await this._eccpBaseElevatorRepository.UpdateAsync(item);
                    await this._elevatorClaimLogsAppService.CreateOrEdit(
                        new CreateOrEditElevatorClaimLogDto { ElevatorId = item.Id });
                }
            }
        }

        /// <summary>
        /// The create or edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task CreateOrEdit(CreateOrEditEccpBaseElevatorDto input)
        {
            if (input.Id == null)
            {
                await this.Create(input);
            }
            else
            {
                await this.Update(input);
            }
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevators_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            var eccpBaseElevatorSubsidiaryInfo =
                await this._eccpBaseElevatorSubsidiaryInfoRepository.FirstOrDefaultAsync(m => m.ElevatorId == input.Id);
            await this._eccpBaseElevatorSubsidiaryInfoRepository.DeleteAsync(eccpBaseElevatorSubsidiaryInfo.Id);
            await this._eccpBaseElevatorRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// The elevator data synchronization.
        ///     电梯数据同步
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int ElevatorDataSynchronization(ElevatorDataSynchronizationDto input)
        {
            // TODO:电梯数据同步暂时未完成
            return 1;
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<PagedResultDto<GetEccpBaseElevatorForView>> GetAll(GetAllEccpBaseElevatorsInput input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var filteredEccpBaseElevators = this._eccpBaseElevatorRepository.GetAll()
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.Filter),
                        e => e.Name.Contains(input.Filter) || e.CertificateNum.Contains(input.Filter)
                                                           || e.MachineNum.Contains(input.Filter)
                                                           || e.InstallationAddress.Contains(input.Filter))
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.CertificateNumFilter),
                        e => e.CertificateNum.ToLower() == input.CertificateNumFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.MachineNumFilter),
                        e => e.MachineNum.ToLower() == input.MachineNumFilter.ToLower().Trim()).WhereIf(
                        !string.IsNullOrWhiteSpace(input.InstallationAddressFilter),
                        e => e.InstallationAddress.ToLower() == input.InstallationAddressFilter.ToLower().Trim());

                if (this.AbpSession.TenantId != null)
                {
                    var tenantModel = await this._tenantRepository.GetAsync(this.AbpSession.TenantId.Value);

                    if (tenantModel.EditionId != null)
                    {
                        var edition = this._editionRepository.Get(tenantModel.EditionId.Value);

                        if (edition.ECCPEditionsTypeId == 2)
                        {
                            var maintenanceCompanyId = this._eccpBaseMaintenanceCompanyRepository
                                .FirstOrDefault(e => e.TenantId == this.AbpSession.TenantId.Value).Id;

                            filteredEccpBaseElevators = filteredEccpBaseElevators.Where(
                                e => e.ECCPBaseMaintenanceCompanyId == maintenanceCompanyId);
                        }
                        else if (edition.ECCPEditionsTypeId == 3)
                        {
                            var propertyCompanyId = this._eccpBasePropertyCompanyRepository
                                .FirstOrDefault(e => e.TenantId == this.AbpSession.TenantId.Value).Id;

                            filteredEccpBaseElevators = filteredEccpBaseElevators.Where(
                                e => e.ECCPBasePropertyCompanyId == propertyCompanyId);
                        }
                    }
                    else
                    {
                        return new PagedResultDto<GetEccpBaseElevatorForView>(0, null);
                    }
                }

                var query =
                    (from o in filteredEccpBaseElevators
                     join o1 in this._eccpDictPlaceTypeRepository.GetAll() on o.EccpDictPlaceTypeId equals o1.Id into j1
                     from s1 in j1.DefaultIfEmpty()
                     join o2 in this._eccpDictElevatorTypeRepository.GetAll() on o.EccpDictElevatorTypeId equals o2.Id
                         into j2
                     from s2 in j2.DefaultIfEmpty()
                     join o3 in this._eccpDictElevatorStatusRepository.GetAll() on o.ECCPDictElevatorStatusId equals
                         o3.Id into j3
                     from s3 in j3.DefaultIfEmpty()
                     join o4 in this._eccpBaseCommunityRepository.GetAll() on o.ECCPBaseCommunityId equals o4.Id into j4
                     from s4 in j4.DefaultIfEmpty()
                     join o5 in this._eccpBaseMaintenanceCompanyRepository.GetAll() on o.ECCPBaseMaintenanceCompanyId
                         equals o5.Id into j5
                     from s5 in j5.DefaultIfEmpty()
                     join o6 in this._eccpBaseAnnualInspectionUnitRepository.GetAll() on
                         o.ECCPBaseAnnualInspectionUnitId equals o6.Id into j6
                     from s6 in j6.DefaultIfEmpty()
                     join o7 in this._eccpBaseRegisterCompanyRepository.GetAll() on o.ECCPBaseRegisterCompanyId equals
                         o7.Id into j7
                     from s7 in j7.DefaultIfEmpty()
                     join o8 in this._eccpBaseProductionCompanyRepository.GetAll() on o.ECCPBaseProductionCompanyId
                         equals o8.Id into j8
                     from s8 in j8.DefaultIfEmpty()
                     join o9 in this._eccpBaseElevatorBrandRepository.GetAll() on o.EccpBaseElevatorBrandId equals o9.Id
                         into j9
                     from s9 in j9.DefaultIfEmpty()
                     join o10 in this._eccpBaseElevatorModelRepository.GetAll() on o.EccpBaseElevatorModelId equals
                         o10.Id into j10
                     from s10 in j10.DefaultIfEmpty()
                     join o11 in this._eccpBaseAreaRepository.GetAll() on o.ProvinceId equals o11.Id into j11
                     from s11 in j11.DefaultIfEmpty()
                     join o12 in this._eccpBaseAreaRepository.GetAll() on o.CityId equals o12.Id into j12
                     from s12 in j12.DefaultIfEmpty()
                     join o13 in this._eccpBaseAreaRepository.GetAll() on o.DistrictId equals o13.Id into j13
                     from s13 in j13.DefaultIfEmpty()
                     join o14 in this._eccpBaseAreaRepository.GetAll() on o.StreetId equals o14.Id into j14
                     from s14 in j14.DefaultIfEmpty()
                     join o15 in this._eccpBasePropertyCompanyRepository.GetAll() on o.ECCPBasePropertyCompanyId equals
                         o15.Id into j15
                     from s15 in j15.DefaultIfEmpty()
                     select new
                     {
                         EccpBaseElevator = o,
                         EccpDictPlaceTypeName = s1 == null ? string.Empty : s1.Name,
                         EccpDictElevatorTypeName = s2 == null ? string.Empty : s2.Name,
                         ECCPDictElevatorStatusName = s3 == null ? string.Empty : s3.Name,
                         ECCPBaseCommunityName = s4 == null ? string.Empty : s4.Name,
                         ECCPBaseMaintenanceCompanyName = s5 == null ? string.Empty : s5.Name,
                         ECCPBaseAnnualInspectionUnitName = s6 == null ? string.Empty : s6.Name,
                         ECCPBaseRegisterCompanyName = s7 == null ? string.Empty : s7.Name,
                         ECCPBaseProductionCompanyName = s8 == null ? string.Empty : s8.Name,
                         EccpBaseElevatorBrandName = s9 == null ? string.Empty : s9.Name,
                         EccpBaseElevatorModelName = s10 == null ? string.Empty : s10.Name,
                         ProvinceName = s11 == null ? string.Empty : s11.Name,
                         CityName = s12 == null ? string.Empty : s12.Name,
                         DistrictName = s13 == null ? string.Empty : s13.Name,
                         StreetName = s14 == null ? string.Empty : s14.Name,
                         ECCPBasePropertyCompanyName = s15 == null ? string.Empty : s15.Name,
                         EccpMaintenanceContractsID = 0,
                         EndDate = new DateTime().Date
                     })
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.EccpDictElevatorTypeNameFilter),
                        e => e.EccpDictElevatorTypeName.ToLower()
                             == input.EccpDictElevatorTypeNameFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.ECCPDictElevatorStatusNameFilter),
                        e => e.ECCPDictElevatorStatusName.ToLower()
                             == input.ECCPDictElevatorStatusNameFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.ECCPBaseCommunityNameFilter),
                        e => e.ECCPBaseCommunityName.ToLower() == input.ECCPBaseCommunityNameFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.ECCPBaseMaintenanceCompanyNameFilter),
                        e => e.ECCPBaseMaintenanceCompanyName.ToLower()
                             == input.ECCPBaseMaintenanceCompanyNameFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.EccpBaseElevatorBrandNameFilter),
                        e => e.EccpBaseElevatorBrandName.ToLower()
                             == input.EccpBaseElevatorBrandNameFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.EccpBaseElevatorModelNameFilter),
                        e => e.EccpBaseElevatorModelName.ToLower()
                             == input.EccpBaseElevatorModelNameFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.ProvinceNameFilter),
                        e => e.ProvinceName.ToLower() == input.ProvinceNameFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.CityNameFilter),
                        e => e.CityName.ToLower() == input.CityNameFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.DistrictNameFilter),
                        e => e.DistrictName.ToLower() == input.DistrictNameFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.StreetNameFilter),
                        e => e.StreetName.ToLower() == input.StreetNameFilter.ToLower().Trim()).WhereIf(
                        !string.IsNullOrWhiteSpace(input.ECCPBasePropertyCompanyNameFilter),
                        e => e.ECCPBasePropertyCompanyName.ToLower()
                             == input.ECCPBasePropertyCompanyNameFilter.ToLower().Trim());

                var totalCount = await query.CountAsync();

                var eccpBaseElevators = new List<GetEccpBaseElevatorForView>();

                query.OrderBy(input.Sorting ?? "eccpBaseElevator.lastModificationTime desc").PageBy(input).MapTo(eccpBaseElevators);

                return new PagedResultDto<GetEccpBaseElevatorForView>(totalCount, eccpBaseElevators);
            }
        }

        /// <summary>
        /// The get all eccp base annual inspection unit for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevators)]
        public async Task<PagedResultDto<ECCPBaseAnnualInspectionUnitLookupTableDto>> GetAllECCPBaseAnnualInspectionUnitForLookupTable(GetAllForLookupTableInput input)
        {
            var query = this._eccpBaseAnnualInspectionUnitRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpBaseAnnualInspectionUnitList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<ECCPBaseAnnualInspectionUnitLookupTableDto>();
            foreach (var eccpBaseAnnualInspectionUnit in eccpBaseAnnualInspectionUnitList)
            {
                lookupTableDtoList.Add(
                    new ECCPBaseAnnualInspectionUnitLookupTableDto
                    {
                        Id = eccpBaseAnnualInspectionUnit.Id,
                        DisplayName = eccpBaseAnnualInspectionUnit.Name
                    });
            }

            return new PagedResultDto<ECCPBaseAnnualInspectionUnitLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get all eccp base area for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevators)]
        public async Task<PagedResultDto<ECCPBaseAreaLookupTableDto>> GetAllECCPBaseAreaForLookupTable(
            GetAllForLookupTableInput input)
        {
            var query = this._eccpBaseAreaRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter)).Where(e => e.ParentId == input.ParentId);

            var totalCount = await query.CountAsync();

            var eccpBaseAreaList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<ECCPBaseAreaLookupTableDto>();
            foreach (var eccpBaseArea in eccpBaseAreaList)
            {
                lookupTableDtoList.Add(
                    new ECCPBaseAreaLookupTableDto { Id = eccpBaseArea.Id, DisplayName = eccpBaseArea.Name });
            }

            return new PagedResultDto<ECCPBaseAreaLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get all eccp base community for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevators)]
        public async Task<PagedResultDto<ECCPBaseCommunityLookupTableDto>> GetAllECCPBaseCommunityForLookupTable(
            GetAllForLookupTableInput input)
        {
            var query = this._eccpBaseCommunityRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter)).Where(e => e.DistrictId == input.ParentId);

            var totalCount = await query.CountAsync();

            var eccpBaseCommunityList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<ECCPBaseCommunityLookupTableDto>();
            foreach (var eccpBaseCommunity in eccpBaseCommunityList)
            {
                lookupTableDtoList.Add(
                    new ECCPBaseCommunityLookupTableDto
                    {
                        Id = eccpBaseCommunity.Id,
                        DisplayName = eccpBaseCommunity.Name
                    });
            }

            return new PagedResultDto<ECCPBaseCommunityLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get all eccp base elevator brand for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevators)]
        public async Task<PagedResultDto<EccpBaseElevatorBrandLookupTableDto>> GetAllEccpBaseElevatorBrandForLookupTable(GetAllForLookupTableInput input)
        {
            var query = this._eccpBaseElevatorBrandRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter)).Where(e => e.ProductionCompanyId == input.ParentId);

            var totalCount = await query.CountAsync();

            var eccpBaseElevatorBrandList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<EccpBaseElevatorBrandLookupTableDto>();
            foreach (var eccpBaseElevatorBrand in eccpBaseElevatorBrandList)
            {
                lookupTableDtoList.Add(
                    new EccpBaseElevatorBrandLookupTableDto
                    {
                        Id = eccpBaseElevatorBrand.Id,
                        DisplayName = eccpBaseElevatorBrand.Name
                    });
            }

            return new PagedResultDto<EccpBaseElevatorBrandLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get all eccp base elevator model for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevators)]
        public async Task<PagedResultDto<EccpBaseElevatorModelLookupTableDto>> GetAllEccpBaseElevatorModelForLookupTable(GetAllForLookupTableInput input)
        {
            var query = this._eccpBaseElevatorModelRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter)).Where(e => e.ElevatorBrandId == input.ParentId);

            var totalCount = await query.CountAsync();

            var eccpBaseElevatorModelList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<EccpBaseElevatorModelLookupTableDto>();
            foreach (var eccpBaseElevatorModel in eccpBaseElevatorModelList)
            {
                lookupTableDtoList.Add(
                    new EccpBaseElevatorModelLookupTableDto
                    {
                        Id = eccpBaseElevatorModel.Id,
                        DisplayName = eccpBaseElevatorModel.Name
                    });
            }

            return new PagedResultDto<EccpBaseElevatorModelLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get all eccp base maintenance company for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevators)]
        public async Task<PagedResultDto<ECCPBaseMaintenanceCompanyLookupTableDto>> GetAllECCPBaseMaintenanceCompanyForLookupTable(GetAllForLookupTableInput input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var query = this._eccpBaseMaintenanceCompanyRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Name.ToString().Contains(input.Filter));

                var totalCount = await query.CountAsync();

                var eccpBaseMaintenanceCompanyList = await query.PageBy(input).ToListAsync();

                var lookupTableDtoList = new List<ECCPBaseMaintenanceCompanyLookupTableDto>();
                foreach (var eccpBaseMaintenanceCompany in eccpBaseMaintenanceCompanyList)
                {
                    lookupTableDtoList.Add(
                        new ECCPBaseMaintenanceCompanyLookupTableDto
                        {
                            Id = eccpBaseMaintenanceCompany.Id,
                            DisplayName = eccpBaseMaintenanceCompany.Name
                        });
                }

                return new PagedResultDto<ECCPBaseMaintenanceCompanyLookupTableDto>(totalCount, lookupTableDtoList);
            }
        }

        /// <summary>
        /// The get all eccp base production company for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevators)]
        public async Task<PagedResultDto<ECCPBaseProductionCompanyLookupTableDto>> GetAllECCPBaseProductionCompanyForLookupTable(GetAllForLookupTableInput input)
        {
            var query = this._eccpBaseProductionCompanyRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpBaseProductionCompanyList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<ECCPBaseProductionCompanyLookupTableDto>();
            foreach (var eccpBaseProductionCompany in eccpBaseProductionCompanyList)
            {
                lookupTableDtoList.Add(
                    new ECCPBaseProductionCompanyLookupTableDto
                    {
                        Id = eccpBaseProductionCompany.Id,
                        DisplayName = eccpBaseProductionCompany.Name
                    });
            }

            return new PagedResultDto<ECCPBaseProductionCompanyLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get all eccp base property company for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevators)]
        public async Task<PagedResultDto<ECCPBasePropertyCompanyLookupTableDto>> GetAllECCPBasePropertyCompanyForLookupTable(GetAllForLookupTableInput input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var query = this._eccpBasePropertyCompanyRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Name.ToString().Contains(input.Filter));

                var totalCount = await query.CountAsync();

                var eccpBasePropertyCompanyList = await query.PageBy(input).ToListAsync();

                var lookupTableDtoList = new List<ECCPBasePropertyCompanyLookupTableDto>();
                foreach (var eccpBasePropertyCompany in eccpBasePropertyCompanyList)
                {
                    lookupTableDtoList.Add(
                        new ECCPBasePropertyCompanyLookupTableDto
                        {
                            Id = eccpBasePropertyCompany.Id,
                            DisplayName = eccpBasePropertyCompany.Name
                        });
                }

                return new PagedResultDto<ECCPBasePropertyCompanyLookupTableDto>(totalCount, lookupTableDtoList);
            }
        }

        /// <summary>
        /// The get all eccp base register company for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevators)]
        public async Task<PagedResultDto<ECCPBaseRegisterCompanyLookupTableDto>> GetAllECCPBaseRegisterCompanyForLookupTable(GetAllForLookupTableInput input)
        {
            var query = this._eccpBaseRegisterCompanyRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpBaseRegisterCompanyList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<ECCPBaseRegisterCompanyLookupTableDto>();
            foreach (var eccpBaseRegisterCompany in eccpBaseRegisterCompanyList)
            {
                lookupTableDtoList.Add(
                    new ECCPBaseRegisterCompanyLookupTableDto
                    {
                        Id = eccpBaseRegisterCompany.Id,
                        DisplayName = eccpBaseRegisterCompany.Name
                    });
            }

            return new PagedResultDto<ECCPBaseRegisterCompanyLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get all eccp dict elevator status for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevators)]
        public async Task<PagedResultDto<ECCPDictElevatorStatusLookupTableDto>> GetAllECCPDictElevatorStatusForLookupTable(GetAllForLookupTableInput input)
        {
            var query = this._eccpDictElevatorStatusRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpDictElevatorStatusList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<ECCPDictElevatorStatusLookupTableDto>();
            foreach (var eccpDictElevatorStatus in eccpDictElevatorStatusList)
            {
                lookupTableDtoList.Add(
                    new ECCPDictElevatorStatusLookupTableDto
                    {
                        Id = eccpDictElevatorStatus.Id,
                        DisplayName = eccpDictElevatorStatus.Name
                    });
            }

            return new PagedResultDto<ECCPDictElevatorStatusLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get all eccp dict elevator type for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevators)]
        public async Task<PagedResultDto<EccpDictElevatorTypeLookupTableDto>> GetAllEccpDictElevatorTypeForLookupTable(
            GetAllForLookupTableInput input)
        {
            var query = this._eccpDictElevatorTypeRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpDictElevatorTypeList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<EccpDictElevatorTypeLookupTableDto>();
            foreach (var eccpDictElevatorType in eccpDictElevatorTypeList)
            {
                lookupTableDtoList.Add(
                    new EccpDictElevatorTypeLookupTableDto
                    {
                        Id = eccpDictElevatorType.Id,
                        DisplayName = eccpDictElevatorType.Name
                    });
            }

            return new PagedResultDto<EccpDictElevatorTypeLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get all eccp dict place type for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevators)]
        public async Task<PagedResultDto<EccpDictPlaceTypeLookupTableDto>> GetAllEccpDictPlaceTypeForLookupTable(
            GetAllForLookupTableInput input)
        {
            var query = this._eccpDictPlaceTypeRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpDictPlaceTypeList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<EccpDictPlaceTypeLookupTableDto>();
            foreach (var eccpDictPlaceType in eccpDictPlaceTypeList)
            {
                lookupTableDtoList.Add(
                    new EccpDictPlaceTypeLookupTableDto
                    {
                        Id = eccpDictPlaceType.Id,
                        DisplayName = eccpDictPlaceType.Name
                    });
            }

            return new PagedResultDto<EccpDictPlaceTypeLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get all work order evaluation by elevator id.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevators_ViewEvaluations)]
        public async Task<PagedResultDto<GetEccpElevatorMaintenanceWorkOrderEvaluationForView>> GetAllWorkOrderEvaluationByElevatorId(GetAllByElevatorIdEccpMaintenanceWorkOrderEvaluationsInput input)
        {
            IQueryable<EccpBaseElevator> filteredEccpBaseElevatorRepository;
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                // this.UnitOfWorkManager.Current.SetTenantId(3);
                filteredEccpBaseElevatorRepository = this._eccpBaseElevatorRepository.GetAll()
                    .Where(e => e.Id.ToString() == input.ElevatorIdFilter);
            }

            var query = from o in filteredEccpBaseElevatorRepository
                        join o1 in this._eccpMaintenancePlanRepository.GetAll() on o.Id equals o1.ElevatorId
                        join o2 in this._eccpMaintenanceWorkOrderRepository.GetAll() on o1.Id equals
                            o2.MaintenancePlanId
                        join o3 in this._eccpMaintenanceWorkOrderEvaluationRepository.GetAll() on o2.Id equals
                            o3.WorkOrderId
                        select new
                        {
                            EccpMaintenanceWorkOrderEvaluation = o3,
                            EccpDictMaintenanceTypeName =
                                           o2.MaintenanceType == null ? string.Empty : o2.MaintenanceType.Name,
                            EccpMaintenanceWorkOrderComplateDate = o2.ComplateDate
                        };

            var eccpMaintenanceWorkOrderEvaluations = new List<GetEccpElevatorMaintenanceWorkOrderEvaluationForView>();

            var totalCount = await query.CountAsync();

            query.OrderBy(input.Sorting ?? "eccpMaintenanceWorkOrderEvaluation.id asc").PageBy(input)
                .MapTo(eccpMaintenanceWorkOrderEvaluations);

            return new PagedResultDto<GetEccpElevatorMaintenanceWorkOrderEvaluationForView>(
                totalCount,
                eccpMaintenanceWorkOrderEvaluations);
        }
        /// <summary>
        /// The get claim all.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<PagedResultDto<GetEccpBaseElevatorForView>> GetClaimAll(GetAllEccpBaseElevatorsInput input)
        {
            if (this.AbpSession.TenantId == null)
            {
                return new PagedResultDto<GetEccpBaseElevatorForView>(0, null);
            }

            int? type = 0;
            var tenant = this._tenantRepository.Get(this.AbpSession.TenantId.Value);

            if (tenant.EditionId != null)
            {
                var edition = this._editionRepository.Get(tenant.EditionId.Value);

                type = edition.ECCPEditionsTypeId;
            }

            if (type == 0 || type == null)
            {
                return new PagedResultDto<GetEccpBaseElevatorForView>(0, null);
            }

            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var filteredEccpBaseElevators = this._eccpBaseElevatorRepository.GetAll()
                    .WhereIf(type == 2, e => e.ECCPBaseMaintenanceCompany.TenantId != this.AbpSession.TenantId)
                    .WhereIf(type == 3, e => e.ECCPBasePropertyCompany.TenantId != this.AbpSession.TenantId)
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.Filter),
                        e => e.Name.Contains(input.Filter) || e.CertificateNum.Contains(input.Filter)
                                                           || e.MachineNum.Contains(input.Filter)
                                                           || e.InstallationAddress.Contains(input.Filter)
                                                           || e.Longitude.Contains(input.Filter)
                                                           || e.Latitude.Contains(input.Filter))
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.CertificateNumFilter),
                        e => e.CertificateNum.ToLower() == input.CertificateNumFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.MachineNumFilter),
                        e => e.MachineNum.ToLower() == input.MachineNumFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.InstallationAddressFilter),
                        e => e.InstallationAddress.ToLower() == input.InstallationAddressFilter.ToLower().Trim())
                    .WhereIf(
                        input.MinInstallationDatetimeFilter != null,
                        e => e.InstallationDatetime >= input.MinInstallationDatetimeFilter).WhereIf(
                        input.MaxInstallationDatetimeFilter != null,
                        e => e.InstallationDatetime <= input.MaxInstallationDatetimeFilter);


                var query =
                    (from o in filteredEccpBaseElevators
                     join o1 in this._eccpDictPlaceTypeRepository.GetAll() on o.EccpDictPlaceTypeId equals o1.Id into j1
                     from s1 in j1.DefaultIfEmpty()
                     join o2 in this._eccpDictElevatorTypeRepository.GetAll() on o.EccpDictElevatorTypeId equals o2.Id
                         into j2
                     from s2 in j2.DefaultIfEmpty()
                     join o3 in this._eccpDictElevatorStatusRepository.GetAll() on o.ECCPDictElevatorStatusId equals
                         o3.Id into j3
                     from s3 in j3.DefaultIfEmpty()
                     join o4 in this._eccpBaseCommunityRepository.GetAll() on o.ECCPBaseCommunityId equals o4.Id into j4
                     from s4 in j4.DefaultIfEmpty()
                     join o5 in this._eccpBaseMaintenanceCompanyRepository.GetAll() on o.ECCPBaseMaintenanceCompanyId
                         equals o5.Id into j5
                     from s5 in j5.DefaultIfEmpty()
                     join o6 in this._eccpBaseAnnualInspectionUnitRepository.GetAll() on
                         o.ECCPBaseAnnualInspectionUnitId equals o6.Id into j6
                     from s6 in j6.DefaultIfEmpty()
                     join o7 in this._eccpBaseRegisterCompanyRepository.GetAll() on o.ECCPBaseRegisterCompanyId equals
                         o7.Id into j7
                     from s7 in j7.DefaultIfEmpty()
                     join o8 in this._eccpBaseProductionCompanyRepository.GetAll() on o.ECCPBaseProductionCompanyId
                         equals o8.Id into j8
                     from s8 in j8.DefaultIfEmpty()
                     join o9 in this._eccpBaseElevatorBrandRepository.GetAll() on o.EccpBaseElevatorBrandId equals o9.Id
                         into j9
                     from s9 in j9.DefaultIfEmpty()
                     join o10 in this._eccpBaseElevatorModelRepository.GetAll() on o.EccpBaseElevatorModelId equals
                         o10.Id into j10
                     from s10 in j10.DefaultIfEmpty()
                     join o11 in this._eccpBaseAreaRepository.GetAll() on o.ProvinceId equals o11.Id into j11
                     from s11 in j11.DefaultIfEmpty()
                     join o12 in this._eccpBaseAreaRepository.GetAll() on o.CityId equals o12.Id into j12
                     from s12 in j12.DefaultIfEmpty()
                     join o13 in this._eccpBaseAreaRepository.GetAll() on o.DistrictId equals o13.Id into j13
                     from s13 in j13.DefaultIfEmpty()
                     join o14 in this._eccpBaseAreaRepository.GetAll() on o.StreetId equals o14.Id into j14
                     from s14 in j14.DefaultIfEmpty()
                     join o15 in this._eccpBasePropertyCompanyRepository.GetAll() on o.ECCPBasePropertyCompanyId equals
                         o15.Id into j15
                     from s15 in j15.DefaultIfEmpty()
                     join o16 in this._eccpMaintenanceContractElevatorLink.GetAll() on o.Id equals o16.ElevatorId into
                         j16
                     from s16 in j16.DefaultIfEmpty()
                     join o17 in this._eccpMaintenanceContract.GetAll() on s16.MaintenanceContractId equals o17.Id into
                      j17
                     from s17 in j17.DefaultIfEmpty()
                     select new
                     {
                         EccpBaseElevator = o,
                         EccpDictPlaceTypeName = s1 == null ? string.Empty : s1.Name,
                         EccpDictElevatorTypeName = s2 == null ? string.Empty : s2.Name,
                         ECCPDictElevatorStatusName = s3 == null ? string.Empty : s3.Name,
                         ECCPBaseCommunityName = s4 == null ? string.Empty : s4.Name,
                         ECCPBaseMaintenanceCompanyName = s5 == null ? string.Empty : s5.Name,
                         ECCPBaseAnnualInspectionUnitName = s6 == null ? string.Empty : s6.Name,
                         ECCPBaseRegisterCompanyName = s7 == null ? string.Empty : s7.Name,
                         ECCPBaseProductionCompanyName = s8 == null ? string.Empty : s8.Name,
                         EccpBaseElevatorBrandName = s9 == null ? string.Empty : s9.Name,
                         EccpBaseElevatorModelName = s10 == null ? string.Empty : s10.Name,
                         ProvinceName = s11 == null ? string.Empty : s11.Name,
                         CityName = s12 == null ? string.Empty : s12.Name,
                         DistrictName = s13 == null ? string.Empty : s13.Name,
                         StreetName = s14 == null ? string.Empty : s14.Name,
                         ECCPBasePropertyCompanyName = s15 == null ? string.Empty : s15.Name,
                         EccpMaintenanceContractsID = s16 == null ? null : s16.MaintenanceContractId,
                         EndDate = s17 == null ? new DateTime().Date : s17.EndDate,
                         ContractPictureId = s17 == null ? null : s17.ContractPictureId
                     })
                    .Where(e => e.EccpMaintenanceContractsID == null || e.EndDate < DateTime.Now || (e.EndDate > DateTime.Now && e.ContractPictureId == null))
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.EccpDictPlaceTypeNameFilter),
                        e => e.EccpDictPlaceTypeName.ToLower() == input.EccpDictPlaceTypeNameFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.EccpDictElevatorTypeNameFilter),
                        e => e.EccpDictElevatorTypeName.ToLower()
                             == input.EccpDictElevatorTypeNameFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.ECCPDictElevatorStatusNameFilter),
                        e => e.ECCPDictElevatorStatusName.ToLower()
                             == input.ECCPDictElevatorStatusNameFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.ECCPBaseCommunityNameFilter),
                        e => e.ECCPBaseCommunityName.ToLower() == input.ECCPBaseCommunityNameFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.ECCPBaseMaintenanceCompanyNameFilter),
                        e => e.ECCPBaseMaintenanceCompanyName.ToLower()
                             == input.ECCPBaseMaintenanceCompanyNameFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.ECCPBaseAnnualInspectionUnitNameFilter),
                        e => e.ECCPBaseAnnualInspectionUnitName.ToLower()
                             == input.ECCPBaseAnnualInspectionUnitNameFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.ECCPBaseRegisterCompanyNameFilter),
                        e => e.ECCPBaseRegisterCompanyName.ToLower()
                             == input.ECCPBaseRegisterCompanyNameFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.ECCPBaseProductionCompanyNameFilter),
                        e => e.ECCPBaseProductionCompanyName.ToLower()
                             == input.ECCPBaseProductionCompanyNameFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.EccpBaseElevatorBrandNameFilter),
                        e => e.EccpBaseElevatorBrandName.ToLower()
                             == input.EccpBaseElevatorBrandNameFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.EccpBaseElevatorModelNameFilter),
                        e => e.EccpBaseElevatorModelName.ToLower()
                             == input.EccpBaseElevatorModelNameFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.ProvinceNameFilter),
                        e => e.ProvinceName.ToLower() == input.ProvinceNameFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.CityNameFilter),
                        e => e.CityName.ToLower() == input.CityNameFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.DistrictNameFilter),
                        e => e.DistrictName.ToLower() == input.DistrictNameFilter.ToLower().Trim())
                    .WhereIf(
                        !string.IsNullOrWhiteSpace(input.StreetNameFilter),
                        e => e.StreetName.ToLower() == input.StreetNameFilter.ToLower().Trim()).WhereIf(
                        !string.IsNullOrWhiteSpace(input.ECCPBasePropertyCompanyNameFilter),
                        e => e.ECCPBasePropertyCompanyName.ToLower()
                             == input.ECCPBasePropertyCompanyNameFilter.ToLower().Trim());

                var totalCount = await query.CountAsync();

                var eccpBaseElevators = new List<GetEccpBaseElevatorForView>();

                query.OrderBy(input.Sorting ?? "eccpBaseElevator.id asc").PageBy(input).MapTo(eccpBaseElevators);

                return new PagedResultDto<GetEccpBaseElevatorForView>(totalCount, eccpBaseElevators);
            }
        }

        /// <summary>
        /// The get eccp base elevator for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevators_Edit)]
        public async Task<GetEccpBaseElevatorForEditOutput> GetEccpBaseElevatorForEdit(EntityDto<Guid> input)
        {
            var eccpBaseElevator = await this._eccpBaseElevatorRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpBaseElevatorForEditOutput
            {
                EccpBaseElevator =
                                     this.ObjectMapper.Map<CreateOrEditEccpBaseElevatorDto>(eccpBaseElevator)
            };

            if (output.EccpBaseElevator.EccpDictPlaceTypeId != null)
            {
                var eccpDictPlaceType =
                    await this._eccpDictPlaceTypeRepository.FirstOrDefaultAsync(
                        (int)output.EccpBaseElevator.EccpDictPlaceTypeId);
                output.EccpDictPlaceTypeName = eccpDictPlaceType.Name;
            }

            if (output.EccpBaseElevator.EccpDictElevatorTypeId != null)
            {
                var eccpDictElevatorType =
                    await this._eccpDictElevatorTypeRepository.FirstOrDefaultAsync(
                        (int)output.EccpBaseElevator.EccpDictElevatorTypeId);
                output.EccpDictElevatorTypeName = eccpDictElevatorType.Name;
            }

            if (output.EccpBaseElevator.ECCPDictElevatorStatusId != null)
            {
                var eccpDictElevatorStatus =
                    await this._eccpDictElevatorStatusRepository.FirstOrDefaultAsync(
                        (int)output.EccpBaseElevator.ECCPDictElevatorStatusId);
                output.ECCPDictElevatorStatusName = eccpDictElevatorStatus.Name;
            }

            if (output.EccpBaseElevator.ECCPBaseCommunityId != null)
            {
                var eccpBaseCommunity =
                    await this._eccpBaseCommunityRepository.FirstOrDefaultAsync(
                        (long)output.EccpBaseElevator.ECCPBaseCommunityId);
                output.ECCPBaseCommunityName = eccpBaseCommunity.Name;
            }

            if (output.EccpBaseElevator.ECCPBaseMaintenanceCompanyId != null)
            {
                using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                {
                    var eccpBaseMaintenanceCompany =
                        await this._eccpBaseMaintenanceCompanyRepository.FirstOrDefaultAsync(
                            (int)output.EccpBaseElevator.ECCPBaseMaintenanceCompanyId);
                    output.ECCPBaseMaintenanceCompanyName = eccpBaseMaintenanceCompany.Name;
                }
            }

            if (output.EccpBaseElevator.ECCPBasePropertyCompanyId != null)
            {
                using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                {
                    var eccpBasePropertyCompany =
                        await this._eccpBasePropertyCompanyRepository.FirstOrDefaultAsync(
                            (int)output.EccpBaseElevator.ECCPBasePropertyCompanyId);
                    output.ECCPBasePropertyCompanyName = eccpBasePropertyCompany.Name;
                }
            }

            if (output.EccpBaseElevator.ECCPBaseAnnualInspectionUnitId != null)
            {
                var eccpBaseAnnualInspectionUnit =
                    await this._eccpBaseAnnualInspectionUnitRepository.FirstOrDefaultAsync(
                        (long)output.EccpBaseElevator.ECCPBaseAnnualInspectionUnitId);
                output.ECCPBaseAnnualInspectionUnitName = eccpBaseAnnualInspectionUnit.Name;
            }

            if (output.EccpBaseElevator.ECCPBaseRegisterCompanyId != null)
            {
                var eccpBaseRegisterCompany =
                    await this._eccpBaseRegisterCompanyRepository.FirstOrDefaultAsync(
                        (long)output.EccpBaseElevator.ECCPBaseRegisterCompanyId);
                output.ECCPBaseRegisterCompanyName = eccpBaseRegisterCompany.Name;
            }

            if (output.EccpBaseElevator.ECCPBaseProductionCompanyId != null)
            {
                var eccpBaseProductionCompany =
                    await this._eccpBaseProductionCompanyRepository.FirstOrDefaultAsync(
                        (long)output.EccpBaseElevator.ECCPBaseProductionCompanyId);
                output.ECCPBaseProductionCompanyName = eccpBaseProductionCompany.Name;
            }

            if (output.EccpBaseElevator.EccpBaseElevatorBrandId != null)
            {
                var eccpBaseElevatorBrand =
                    await this._eccpBaseElevatorBrandRepository.FirstOrDefaultAsync(
                        (int)output.EccpBaseElevator.EccpBaseElevatorBrandId);
                output.EccpBaseElevatorBrandName = eccpBaseElevatorBrand.Name;
            }

            if (output.EccpBaseElevator.EccpBaseElevatorModelId != null)
            {
                var eccpBaseElevatorModel =
                    await this._eccpBaseElevatorModelRepository.FirstOrDefaultAsync(
                        (int)output.EccpBaseElevator.EccpBaseElevatorModelId);
                output.EccpBaseElevatorModelName = eccpBaseElevatorModel.Name;
            }

            if (output.EccpBaseElevator.ProvinceId != null)
            {
                var province =
                    await this._eccpBaseAreaRepository.FirstOrDefaultAsync((int)output.EccpBaseElevator.ProvinceId);
                output.ProvinceName = province.Name;
            }

            if (output.EccpBaseElevator.CityId != null)
            {
                var city = await this._eccpBaseAreaRepository.FirstOrDefaultAsync((int)output.EccpBaseElevator.CityId);
                output.CityName = city.Name;
            }

            if (output.EccpBaseElevator.DistrictId != null)
            {
                var district =
                    await this._eccpBaseAreaRepository.FirstOrDefaultAsync((int)output.EccpBaseElevator.DistrictId);
                output.DistrictName = district.Name;
            }

            if (output.EccpBaseElevator.StreetId != null)
            {
                var street =
                    await this._eccpBaseAreaRepository.FirstOrDefaultAsync((int)output.EccpBaseElevator.StreetId);
                output.StreetName = street.Name;
            }

            var eccpBaseElevatorSubsidiaryInfo =
                await this._eccpBaseElevatorSubsidiaryInfoRepository.FirstOrDefaultAsync(
                    m => m.ElevatorId == (Guid)output.EccpBaseElevator.Id);
            if (eccpBaseElevatorSubsidiaryInfo != null)
            {
                output.EccpBaseElevatorSubsidiaryInfo =
                    this.ObjectMapper.Map<CreateOrEditEccpBaseElevatorSubsidiaryInfoDto>(
                        eccpBaseElevatorSubsidiaryInfo);
            }
            else
            {
                output.EccpBaseElevatorSubsidiaryInfo = new CreateOrEditEccpBaseElevatorSubsidiaryInfoDto();
            }

            return output;
        }

        /// <summary>
        /// The get eccp base elevators to excel.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<FileDto> GetEccpBaseElevatorsToExcel(GetAllEccpBaseElevatorsForExcelInput input)
        {
            var filteredEccpBaseElevators = this._eccpBaseElevatorRepository.GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Name.Contains(input.Filter) || e.CertificateNum.Contains(input.Filter)
                                                       || e.MachineNum.Contains(input.Filter)
                                                       || e.InstallationAddress.Contains(input.Filter)
                                                       || e.Longitude.Contains(input.Filter)
                                                       || e.Latitude.Contains(input.Filter))
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.CertificateNumFilter),
                    e => e.CertificateNum.ToLower() == input.CertificateNumFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.MachineNumFilter),
                    e => e.MachineNum.ToLower() == input.MachineNumFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.InstallationAddressFilter),
                    e => e.InstallationAddress.ToLower() == input.InstallationAddressFilter.ToLower().Trim()).WhereIf(
                    input.MinInstallationDatetimeFilter != null,
                    e => e.InstallationDatetime >= input.MinInstallationDatetimeFilter).WhereIf(
                    input.MaxInstallationDatetimeFilter != null,
                    e => e.InstallationDatetime <= input.MaxInstallationDatetimeFilter);

            var query =
                (from o in filteredEccpBaseElevators
                 join o1 in this._eccpDictPlaceTypeRepository.GetAll() on o.EccpDictPlaceTypeId equals o1.Id into j1
                 from s1 in j1.DefaultIfEmpty()
                 join o2 in this._eccpDictElevatorTypeRepository.GetAll() on o.EccpDictElevatorTypeId equals o2.Id into
                     j2
                 from s2 in j2.DefaultIfEmpty()
                 join o3 in this._eccpDictElevatorStatusRepository.GetAll() on o.ECCPDictElevatorStatusId equals o3.Id
                     into j3
                 from s3 in j3.DefaultIfEmpty()
                 join o4 in this._eccpBaseCommunityRepository.GetAll() on o.ECCPBaseCommunityId equals o4.Id into j4
                 from s4 in j4.DefaultIfEmpty()
                 join o5 in this._eccpBaseMaintenanceCompanyRepository.GetAll() on o.ECCPBaseMaintenanceCompanyId equals
                     o5.Id into j5
                 from s5 in j5.DefaultIfEmpty()
                 join o6 in this._eccpBaseAnnualInspectionUnitRepository.GetAll() on o.ECCPBaseAnnualInspectionUnitId
                     equals o6.Id into j6
                 from s6 in j6.DefaultIfEmpty()
                 join o7 in this._eccpBaseRegisterCompanyRepository.GetAll() on o.ECCPBaseRegisterCompanyId equals o7.Id
                     into j7
                 from s7 in j7.DefaultIfEmpty()
                 join o8 in this._eccpBaseProductionCompanyRepository.GetAll() on o.ECCPBaseProductionCompanyId equals
                     o8.Id into j8
                 from s8 in j8.DefaultIfEmpty()
                 join o9 in this._eccpBaseElevatorBrandRepository.GetAll() on o.EccpBaseElevatorBrandId equals o9.Id
                     into j9
                 from s9 in j9.DefaultIfEmpty()
                 join o10 in this._eccpBaseElevatorModelRepository.GetAll() on o.EccpBaseElevatorModelId equals o10.Id
                     into j10
                 from s10 in j10.DefaultIfEmpty()
                 join o11 in this._eccpBaseAreaRepository.GetAll() on o.ProvinceId equals o11.Id into j11
                 from s11 in j11.DefaultIfEmpty()
                 join o12 in this._eccpBaseAreaRepository.GetAll() on o.CityId equals o12.Id into j12
                 from s12 in j12.DefaultIfEmpty()
                 join o13 in this._eccpBaseAreaRepository.GetAll() on o.DistrictId equals o13.Id into j13
                 from s13 in j13.DefaultIfEmpty()
                 join o14 in this._eccpBaseAreaRepository.GetAll() on o.StreetId equals o14.Id into j14
                 from s14 in j14.DefaultIfEmpty()
                 join o15 in this._eccpBasePropertyCompanyRepository.GetAll() on o.ECCPBasePropertyCompanyId equals
                     o15.Id into j15
                 from s15 in j15.DefaultIfEmpty()
                 select new GetEccpBaseElevatorForView
                 {
                     EccpBaseElevator = this.ObjectMapper.Map<EccpBaseElevatorDto>(o),
                     EccpDictPlaceTypeName = s1 == null ? string.Empty : s1.Name,
                     EccpDictElevatorTypeName = s2 == null ? string.Empty : s2.Name,
                     ECCPDictElevatorStatusName = s3 == null ? string.Empty : s3.Name,
                     ECCPBaseCommunityName = s4 == null ? string.Empty : s4.Name,
                     ECCPBaseMaintenanceCompanyName = s5 == null ? string.Empty : s5.Name,
                     ECCPBaseAnnualInspectionUnitName = s6 == null ? string.Empty : s6.Name,
                     ECCPBaseRegisterCompanyName = s7 == null ? string.Empty : s7.Name,
                     ECCPBaseProductionCompanyName = s8 == null ? string.Empty : s8.Name,
                     EccpBaseElevatorBrandName = s9 == null ? string.Empty : s9.Name,
                     EccpBaseElevatorModelName = s10 == null ? string.Empty : s10.Name,
                     ProvinceName = s11 == null ? string.Empty : s11.Name,
                     CityName = s12 == null ? string.Empty : s12.Name,
                     DistrictName = s13 == null ? string.Empty : s13.Name,
                     StreetName = s14 == null ? string.Empty : s14.Name,
                     ECCPBasePropertyCompanyName = s15 == null ? string.Empty : s15.Name
                 })
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.EccpDictPlaceTypeNameFilter),
                    e => e.EccpDictPlaceTypeName.ToLower() == input.EccpDictPlaceTypeNameFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.EccpDictElevatorTypeNameFilter),
                    e => e.EccpDictElevatorTypeName.ToLower() == input.EccpDictElevatorTypeNameFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.ECCPDictElevatorStatusNameFilter),
                    e => e.ECCPDictElevatorStatusName.ToLower()
                         == input.ECCPDictElevatorStatusNameFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.ECCPBaseCommunityNameFilter),
                    e => e.ECCPBaseCommunityName.ToLower() == input.ECCPBaseCommunityNameFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.ECCPBaseMaintenanceCompanyNameFilter),
                    e => e.ECCPBaseMaintenanceCompanyName.ToLower()
                         == input.ECCPBaseMaintenanceCompanyNameFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.ECCPBaseAnnualInspectionUnitNameFilter),
                    e => e.ECCPBaseAnnualInspectionUnitName.ToLower()
                         == input.ECCPBaseAnnualInspectionUnitNameFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.ECCPBaseRegisterCompanyNameFilter),
                    e => e.ECCPBaseRegisterCompanyName.ToLower()
                         == input.ECCPBaseRegisterCompanyNameFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.ECCPBaseProductionCompanyNameFilter),
                    e => e.ECCPBaseProductionCompanyName.ToLower()
                         == input.ECCPBaseProductionCompanyNameFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.EccpBaseElevatorBrandNameFilter),
                    e => e.EccpBaseElevatorBrandName.ToLower()
                         == input.EccpBaseElevatorBrandNameFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.EccpBaseElevatorModelNameFilter),
                    e => e.EccpBaseElevatorModelName.ToLower()
                         == input.EccpBaseElevatorModelNameFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.ProvinceNameFilter),
                    e => e.ProvinceName.ToLower() == input.ProvinceNameFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.CityNameFilter),
                    e => e.CityName.ToLower() == input.CityNameFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.DistrictNameFilter),
                    e => e.DistrictName.ToLower() == input.DistrictNameFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.StreetNameFilter),
                    e => e.StreetName.ToLower() == input.StreetNameFilter.ToLower().Trim()).WhereIf(
                    !string.IsNullOrWhiteSpace(input.ECCPBasePropertyCompanyNameFilter),
                    e => e.ECCPBasePropertyCompanyName.ToLower()
                         == input.ECCPBasePropertyCompanyNameFilter.ToLower().Trim());

            var eccpBaseElevatorListDtos = await query.ToListAsync();

            return this._eccpBaseElevatorsExcelExporter.ExportToFile(eccpBaseElevatorListDtos);
        }

        /// <summary>
        /// The get eccp base elevator subsidiary info dto.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="EccpBaseElevatorSubsidiaryInfoDto"/>.
        /// </returns>
        public EccpBaseElevatorSubsidiaryInfoDto GetEccpBaseElevatorSubsidiaryInfoDto(EntityDto<Guid> input)
        {
            var eccpBaseElevatorSubsidiaryInfo =
                this._eccpBaseElevatorSubsidiaryInfoRepository.FirstOrDefault(m => m.ElevatorId == input.Id);
            var eccpBaseElevatorSubsidiaryInfoDto =
                this.ObjectMapper.Map<EccpBaseElevatorSubsidiaryInfoDto>(eccpBaseElevatorSubsidiaryInfo);
            return eccpBaseElevatorSubsidiaryInfoDto;
        }

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevators_Create)]
        private async Task Create(CreateOrEditEccpBaseElevatorDto input)
        {
            var eccpBaseElevator = this.ObjectMapper.Map<EccpBaseElevator>(input);
            var eccpBaseElevatorSubsidiaryInfo =
                this.ObjectMapper.Map<EccpBaseElevatorSubsidiaryInfo>(
                    input.createOrEditEccpBaseElevatorSubsidiaryInfoDto);
            if (this.AbpSession.TenantId != null)
            {
                var tenantModel = await this._tenantRepository.GetAsync(this.AbpSession.TenantId.Value);
                if (tenantModel.EditionId != null)
                {
                    var edition = this._editionRepository.Get(tenantModel.EditionId.Value);

                    if (edition.ECCPEditionsTypeId == 2)
                    {
                        eccpBaseElevator.ECCPBaseMaintenanceCompanyId = this._eccpBaseMaintenanceCompanyRepository
                            .FirstOrDefault(e => e.TenantId == this.AbpSession.TenantId.Value).Id;
                    }
                    else if (edition.ECCPEditionsTypeId == 3)
                    {
                        eccpBaseElevator.ECCPBasePropertyCompanyId = this._eccpBasePropertyCompanyRepository
                            .FirstOrDefault(e => e.TenantId == this.AbpSession.TenantId.Value).Id;
                    }
                }
            }

            var id = this._eccpBaseElevatorRepository.InsertAndGetId(eccpBaseElevator);
            eccpBaseElevatorSubsidiaryInfo.ElevatorId = id;
            await this._eccpBaseElevatorSubsidiaryInfoRepository.InsertAsync(eccpBaseElevatorSubsidiaryInfo);
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="UserFriendlyException">
        /// The user friendly exception
        /// </exception>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpBaseElevators_Edit)]
        private async Task Update(CreateOrEditEccpBaseElevatorDto input)
        {
            try
            {
                if (input.Id != null)
                {
                    var eccpBaseElevator = await this._eccpBaseElevatorRepository.FirstOrDefaultAsync((Guid)input.Id);
                    var eccpBaseElevatorSubsidiaryInfo =
                        await this._eccpBaseElevatorSubsidiaryInfoRepository.FirstOrDefaultAsync(
                            m => m.ElevatorId == (Guid)input.Id);

                    if (eccpBaseElevator.Name != input.Name)
                    {
                        await this._eccpElevatorChangeLogRepository.InsertAsync(
                            new EccpElevatorChangeLog
                            {
                                FieldName = "Name",
                                OldValue = eccpBaseElevator.Name ?? string.Empty,
                                NewValue = input.Name,
                                ElevatorId = input.Id.Value
                            });
                    }

                    if (eccpBaseElevator.CertificateNum != input.CertificateNum)
                    {
                        await this._eccpElevatorChangeLogRepository.InsertAsync(
                            new EccpElevatorChangeLog
                            {
                                FieldName = "CertificateNum",
                                OldValue = eccpBaseElevator.CertificateNum,
                                NewValue = input.CertificateNum,
                                ElevatorId = input.Id.Value
                            });
                    }

                    if (eccpBaseElevator.MachineNum != input.MachineNum)
                    {
                        await this._eccpElevatorChangeLogRepository.InsertAsync(
                            new EccpElevatorChangeLog
                            {
                                FieldName = "MachineNum",
                                OldValue = eccpBaseElevator.MachineNum ?? string.Empty,
                                NewValue = input.MachineNum,
                                ElevatorId = input.Id.Value
                            });
                    }

                    if (eccpBaseElevator.InstallationAddress != input.InstallationAddress)
                    {
                        await this._eccpElevatorChangeLogRepository.InsertAsync(
                            new EccpElevatorChangeLog
                            {
                                FieldName = "InstallationAddress",
                                OldValue = eccpBaseElevator.InstallationAddress ?? string.Empty,
                                NewValue = input.InstallationAddress,
                                ElevatorId = input.Id.Value
                            });
                    }

                    if (eccpBaseElevator.InstallationDatetime != input.InstallationDatetime)
                    {
                        await this._eccpElevatorChangeLogRepository.InsertAsync(
                            new EccpElevatorChangeLog
                            {
                                FieldName = "InstallationDatetime",
                                OldValue =
                                        eccpBaseElevator.InstallationDatetime == null
                                            ? string.Empty
                                            : eccpBaseElevator.InstallationDatetime.ToString(),
                                NewValue = input.InstallationDatetime.ToString(),
                                ElevatorId = input.Id.Value
                            });
                    }

                    if (eccpBaseElevator.Longitude != input.Longitude)
                    {
                        await this._eccpElevatorChangeLogRepository.InsertAsync(
                            new EccpElevatorChangeLog
                            {
                                FieldName = "Longitude",
                                OldValue = eccpBaseElevator.Longitude,
                                NewValue = input.Longitude,
                                ElevatorId = input.Id.Value
                            });
                    }

                    if (eccpBaseElevator.Latitude != input.Latitude)
                    {
                        await this._eccpElevatorChangeLogRepository.InsertAsync(
                            new EccpElevatorChangeLog
                            {
                                FieldName = "Latitude",
                                OldValue = eccpBaseElevator.Latitude,
                                NewValue = input.Latitude,
                                ElevatorId = input.Id.Value
                            });
                    }

                    if (eccpBaseElevator.EccpDictPlaceTypeId != input.EccpDictPlaceTypeId)
                    {
                        await this._eccpElevatorChangeLogRepository.InsertAsync(
                            new EccpElevatorChangeLog
                            {
                                FieldName = "EccpDictPlaceTypeId",
                                OldValue =
                                        eccpBaseElevator.EccpDictPlaceTypeId == null
                                            ? string.Empty
                                            : eccpBaseElevator.EccpDictPlaceTypeId.ToString(),
                                NewValue = input.EccpDictPlaceTypeId.ToString(),
                                ElevatorId = input.Id.Value
                            });
                    }

                    if (eccpBaseElevator.EccpDictElevatorTypeId != input.EccpDictElevatorTypeId)
                    {
                        await this._eccpElevatorChangeLogRepository.InsertAsync(
                            new EccpElevatorChangeLog
                            {
                                FieldName = "EccpDictElevatorTypeId",
                                OldValue =
                                        eccpBaseElevator.EccpDictElevatorTypeId == null
                                            ? string.Empty
                                            : eccpBaseElevator.EccpDictElevatorTypeId.ToString(),
                                NewValue = input.EccpDictElevatorTypeId.ToString(),
                                ElevatorId = input.Id.Value
                            });
                    }

                    if (eccpBaseElevator.ECCPDictElevatorStatusId != input.ECCPDictElevatorStatusId)
                    {
                        await this._eccpElevatorChangeLogRepository.InsertAsync(
                            new EccpElevatorChangeLog
                            {
                                FieldName = "ECCPDictElevatorStatusId",
                                OldValue =
                                        eccpBaseElevator.ECCPDictElevatorStatusId == null
                                            ? string.Empty
                                            : eccpBaseElevator.ECCPDictElevatorStatusId.ToString(),
                                NewValue = input.ECCPDictElevatorStatusId.ToString(),
                                ElevatorId = input.Id.Value
                            });
                    }

                    if (eccpBaseElevator.ECCPBaseCommunityId != input.ECCPBaseCommunityId)
                    {
                        await this._eccpElevatorChangeLogRepository.InsertAsync(
                            new EccpElevatorChangeLog
                            {
                                FieldName = "ECCPBaseCommunityId",
                                OldValue =
                                        eccpBaseElevator.ECCPBaseCommunityId == null
                                            ? string.Empty
                                            : eccpBaseElevator.ECCPBaseCommunityId.ToString(),
                                NewValue = input.ECCPBaseCommunityId.ToString(),
                                ElevatorId = input.Id.Value
                            });
                    }

                    if (eccpBaseElevator.ECCPBaseMaintenanceCompanyId != input.ECCPBaseMaintenanceCompanyId)
                    {
                        await this._eccpElevatorChangeLogRepository.InsertAsync(
                            new EccpElevatorChangeLog
                            {
                                FieldName = "ECCPBaseMaintenanceCompanyId",
                                OldValue = eccpBaseElevator.ECCPBaseMaintenanceCompanyId == null
                                                   ? string.Empty
                                                   : eccpBaseElevator.ECCPBaseMaintenanceCompanyId.ToString(),
                                NewValue = input.ECCPBaseMaintenanceCompanyId.ToString(),
                                ElevatorId = input.Id.Value
                            });
                    }

                    if (eccpBaseElevator.ECCPBasePropertyCompanyId != input.ECCPBasePropertyCompanyId)
                    {
                        await this._eccpElevatorChangeLogRepository.InsertAsync(
                            new EccpElevatorChangeLog
                            {
                                FieldName = "ECCPBasePropertyCompanyId",
                                OldValue =
                                        eccpBaseElevator.ECCPBasePropertyCompanyId == null
                                            ? string.Empty
                                            : eccpBaseElevator.ECCPBasePropertyCompanyId.ToString(),
                                NewValue = input.ECCPBasePropertyCompanyId.ToString(),
                                ElevatorId = input.Id.Value
                            });
                    }

                    if (eccpBaseElevator.ECCPBaseAnnualInspectionUnitId != input.ECCPBaseAnnualInspectionUnitId)
                    {
                        await this._eccpElevatorChangeLogRepository.InsertAsync(
                            new EccpElevatorChangeLog
                            {
                                FieldName = "ECCPBaseAnnualInspectionUnitId",
                                OldValue = eccpBaseElevator.ECCPBaseAnnualInspectionUnitId == null
                                                   ? string.Empty
                                                   : eccpBaseElevator.ECCPBaseAnnualInspectionUnitId.ToString(),
                                NewValue = input.ECCPBaseAnnualInspectionUnitId.ToString(),
                                ElevatorId = input.Id.Value
                            });
                    }

                    if (eccpBaseElevator.ECCPBaseRegisterCompanyId != input.ECCPBaseRegisterCompanyId)
                    {
                        await this._eccpElevatorChangeLogRepository.InsertAsync(
                            new EccpElevatorChangeLog
                            {
                                FieldName = "ECCPBaseRegisterCompanyId",
                                OldValue =
                                        eccpBaseElevator.ECCPBaseRegisterCompanyId == null
                                            ? string.Empty
                                            : eccpBaseElevator.ECCPBaseRegisterCompanyId.ToString(),
                                NewValue = input.ECCPBaseRegisterCompanyId.ToString(),
                                ElevatorId = input.Id.Value
                            });
                    }

                    if (eccpBaseElevator.ECCPBaseProductionCompanyId != input.ECCPBaseProductionCompanyId)
                    {
                        await this._eccpElevatorChangeLogRepository.InsertAsync(
                            new EccpElevatorChangeLog
                            {
                                FieldName = "ECCPBaseProductionCompanyId",
                                OldValue = eccpBaseElevator.ECCPBaseProductionCompanyId == null
                                                   ? string.Empty
                                                   : eccpBaseElevator.ECCPBaseProductionCompanyId.ToString(),
                                NewValue = input.ECCPBaseProductionCompanyId.ToString(),
                                ElevatorId = input.Id.Value
                            });
                    }

                    if (eccpBaseElevator.EccpBaseElevatorBrandId != input.EccpBaseElevatorBrandId)
                    {
                        await this._eccpElevatorChangeLogRepository.InsertAsync(
                            new EccpElevatorChangeLog
                            {
                                FieldName = "EccpBaseElevatorBrandId",
                                OldValue =
                                        eccpBaseElevator.EccpBaseElevatorBrandId == null
                                            ? string.Empty
                                            : eccpBaseElevator.EccpBaseElevatorBrandId.ToString(),
                                NewValue = input.EccpBaseElevatorBrandId.ToString(),
                                ElevatorId = input.Id.Value
                            });
                    }

                    if (eccpBaseElevator.EccpBaseElevatorModelId != input.EccpBaseElevatorModelId)
                    {
                        await this._eccpElevatorChangeLogRepository.InsertAsync(
                            new EccpElevatorChangeLog
                            {
                                FieldName = "EccpBaseElevatorModelId",
                                OldValue =
                                        eccpBaseElevator.EccpBaseElevatorModelId == null
                                            ? string.Empty
                                            : eccpBaseElevator.EccpBaseElevatorModelId.ToString(),
                                NewValue = input.EccpBaseElevatorModelId.ToString(),
                                ElevatorId = input.Id.Value
                            });
                    }

                    if (eccpBaseElevator.ProvinceId != input.ProvinceId)
                    {
                        await this._eccpElevatorChangeLogRepository.InsertAsync(
                            new EccpElevatorChangeLog
                            {
                                FieldName = "ProvinceId",
                                OldValue = eccpBaseElevator.ProvinceId == null
                                                   ? string.Empty
                                                   : eccpBaseElevator.ProvinceId.ToString(),
                                NewValue = input.ProvinceId.ToString(),
                                ElevatorId = input.Id.Value
                            });
                    }

                    if (eccpBaseElevator.CityId != input.CityId)
                    {
                        await this._eccpElevatorChangeLogRepository.InsertAsync(
                            new EccpElevatorChangeLog
                            {
                                FieldName = "CityId",
                                OldValue = eccpBaseElevator.CityId == null
                                                   ? string.Empty
                                                   : eccpBaseElevator.CityId.ToString(),
                                NewValue = input.CityId.ToString(),
                                ElevatorId = input.Id.Value
                            });
                    }

                    if (eccpBaseElevator.DistrictId != input.DistrictId)
                    {
                        await this._eccpElevatorChangeLogRepository.InsertAsync(
                            new EccpElevatorChangeLog
                            {
                                FieldName = "DistrictId",
                                OldValue = eccpBaseElevator.DistrictId == null
                                                   ? string.Empty
                                                   : eccpBaseElevator.DistrictId.ToString(),
                                NewValue = input.DistrictId.ToString(),
                                ElevatorId = input.Id.Value
                            });
                    }

                    if (eccpBaseElevator.StreetId != input.StreetId)
                    {
                        await this._eccpElevatorChangeLogRepository.InsertAsync(
                            new EccpElevatorChangeLog
                            {
                                FieldName = "StreetId",
                                OldValue = eccpBaseElevator.StreetId == null
                                                   ? string.Empty
                                                   : eccpBaseElevator.StreetId.ToString(),
                                NewValue = input.StreetId.ToString(),
                                ElevatorId = input.Id.Value
                            });
                    }

                    if (eccpBaseElevatorSubsidiaryInfo != null)
                    {
                        if (eccpBaseElevatorSubsidiaryInfo.CustomNum
                            != input.createOrEditEccpBaseElevatorSubsidiaryInfoDto.CustomNum)
                        {
                            await this._eccpElevatorChangeLogRepository.InsertAsync(
                                new EccpElevatorChangeLog
                                {
                                    FieldName = "XCustomNum",
                                    OldValue = eccpBaseElevatorSubsidiaryInfo.CustomNum ?? string.Empty,
                                    NewValue = input.createOrEditEccpBaseElevatorSubsidiaryInfoDto.CustomNum,
                                    ElevatorId = input.Id.Value
                                });
                        }

                        if (eccpBaseElevatorSubsidiaryInfo.ManufacturingLicenseNumber
                            != input.createOrEditEccpBaseElevatorSubsidiaryInfoDto.ManufacturingLicenseNumber)
                        {
                            await this._eccpElevatorChangeLogRepository.InsertAsync(
                                new EccpElevatorChangeLog
                                {
                                    FieldName = "XManufacturingLicenseNumber",
                                    OldValue =
                                            eccpBaseElevatorSubsidiaryInfo.ManufacturingLicenseNumber ?? string.Empty,
                                    NewValue = input.createOrEditEccpBaseElevatorSubsidiaryInfoDto
                                            .ManufacturingLicenseNumber,
                                    ElevatorId = input.Id.Value
                                });
                        }

                        if (eccpBaseElevatorSubsidiaryInfo.FloorNumber
                            != input.createOrEditEccpBaseElevatorSubsidiaryInfoDto.FloorNumber)
                        {
                            await this._eccpElevatorChangeLogRepository.InsertAsync(
                                new EccpElevatorChangeLog
                                {
                                    FieldName = "XFloorNumber",
                                    OldValue =
                                            eccpBaseElevatorSubsidiaryInfo.FloorNumber == null
                                                ? string.Empty
                                                : eccpBaseElevatorSubsidiaryInfo.FloorNumber.ToString(),
                                    NewValue = input.createOrEditEccpBaseElevatorSubsidiaryInfoDto.FloorNumber
                                            .ToString(),
                                    ElevatorId = input.Id.Value
                                });
                        }

                        if (eccpBaseElevatorSubsidiaryInfo.GateNumber
                            != input.createOrEditEccpBaseElevatorSubsidiaryInfoDto.GateNumber)
                        {
                            await this._eccpElevatorChangeLogRepository.InsertAsync(
                                new EccpElevatorChangeLog
                                {
                                    FieldName = "XGateNumber",
                                    OldValue =
                                            eccpBaseElevatorSubsidiaryInfo.GateNumber == null
                                                ? string.Empty
                                                : eccpBaseElevatorSubsidiaryInfo.GateNumber.ToString(),
                                    NewValue = input.createOrEditEccpBaseElevatorSubsidiaryInfoDto.GateNumber
                                            .ToString(),
                                    ElevatorId = input.Id.Value
                                });
                        }

                        if (eccpBaseElevatorSubsidiaryInfo.RatedSpeed != input.createOrEditEccpBaseElevatorSubsidiaryInfoDto.RatedSpeed)
                        {
                            await this._eccpElevatorChangeLogRepository.InsertAsync(
                                new EccpElevatorChangeLog
                                {
                                    FieldName = "XRatedSpeed",
                                    OldValue =
                                            eccpBaseElevatorSubsidiaryInfo.RatedSpeed == null
                                                ? string.Empty
                                                : eccpBaseElevatorSubsidiaryInfo.RatedSpeed.ToString(),
                                    NewValue = input.createOrEditEccpBaseElevatorSubsidiaryInfoDto.RatedSpeed
                                            .ToString(),
                                    ElevatorId = input.Id.Value
                                });
                        }

                        if (eccpBaseElevatorSubsidiaryInfo.Deadweight != input
                                    .createOrEditEccpBaseElevatorSubsidiaryInfoDto.Deadweight)

                        {
                            await this._eccpElevatorChangeLogRepository.InsertAsync(
                                new EccpElevatorChangeLog
                                {
                                    FieldName = "XDeadweight",
                                    OldValue =
                                            eccpBaseElevatorSubsidiaryInfo.Deadweight == null
                                                ? string.Empty
                                                : eccpBaseElevatorSubsidiaryInfo.Deadweight.ToString(),
                                    NewValue = input.createOrEditEccpBaseElevatorSubsidiaryInfoDto.Deadweight
                                            .ToString(),
                                    ElevatorId = input.Id.Value
                                });
                        }

                        input.createOrEditEccpBaseElevatorSubsidiaryInfoDto.Id = eccpBaseElevatorSubsidiaryInfo.Id;
                    }

                    input.createOrEditEccpBaseElevatorSubsidiaryInfoDto.ElevatorId = eccpBaseElevator.Id;
                    this.ObjectMapper.Map(input, eccpBaseElevator);
                    if (eccpBaseElevatorSubsidiaryInfo != null)
                    {
                        this.ObjectMapper.Map(
                            input.createOrEditEccpBaseElevatorSubsidiaryInfoDto,
                            eccpBaseElevatorSubsidiaryInfo);
                    }
                    else
                    {
                        var eccpBaseElevatorSubsidiaryInfoInsert =
                            this.ObjectMapper.Map<EccpBaseElevatorSubsidiaryInfo>(
                                input.createOrEditEccpBaseElevatorSubsidiaryInfoDto);
                        await this._eccpBaseElevatorSubsidiaryInfoRepository.InsertAsync(
                            eccpBaseElevatorSubsidiaryInfoInsert);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.ToString());
            }
        }

        /// <summary>
        /// 根据电梯Id获取该电梯的更改信息
        /// </summary>
        /// <param name="elevatorId"></param>
        /// <returns></returns>
        public List<GetChangeLogByElevatorIdDto> GetAllChangeLogByElevatorId(string elevatorId)
        {
            var changeLogQuerys = this._eccpElevatorChangeLogRepository.GetAll().Where(e => e.ElevatorId.ToString() == elevatorId);
            var users = this.UserManager.Users;
            var tenantQuerys = this._tenantRepository.GetAll();

            var query = from changeLogQuery in changeLogQuerys
                        join user in users on changeLogQuery.CreatorUserId equals user.Id
                        into j1
                        from s1 in j1.DefaultIfEmpty()
                        join tenantQuery in tenantQuerys on s1.TenantId equals tenantQuery.Id
                        into j2
                        from s2 in j2.DefaultIfEmpty()
                        select new GetChangeLogByElevatorIdDto
                        {
                            FieldName = this.L(""+changeLogQuery.FieldName+""),
                            NewValue = changeLogQuery.NewValue,
                            OldValue = changeLogQuery.OldValue,
                            CreationTime = changeLogQuery.CreationTime,
                            CreatorUserName = s1 == null ? string.Empty : s1.Name,
                            ProfilePictureId = s1 == null ? null : s1.ProfilePictureId,
                            TenantName = s2 == null ? string.Empty : s2.Name
                        };

            return query.OrderBy(e => e.CreationTime).ToList();
        }
    }
}