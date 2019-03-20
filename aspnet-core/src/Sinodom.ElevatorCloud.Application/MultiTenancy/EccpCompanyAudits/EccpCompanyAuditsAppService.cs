// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpCompanyAuditsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.MultiTenancy.EccpCompanyAudits
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.Authorization;
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Abp.Linq.Extensions;
    using Abp.UI;

    using Microsoft.EntityFrameworkCore;

    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.ECCPBaseAreas;
    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;
    using Sinodom.ElevatorCloud.Editions;
    using Sinodom.ElevatorCloud.MultiTenancy.CompanyAudits;
    using Sinodom.ElevatorCloud.MultiTenancy.CompanyAudits.Dtos;
    using Sinodom.ElevatorCloud.MultiTenancy.CompanyExtensions;

    /// <summary>
    /// The eccp company audits app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_Administration_EccpCompanyAudits)]
    public class EccpCompanyAuditsAppService : ElevatorCloudAppServiceBase, IEccpCompanyAuditsAppService
    {
        /// <summary>
        /// The _e ccp base area repository.
        /// </summary>
        private readonly IRepository<ECCPBaseArea, int> _eccpBaseAreaRepository;

        /// <summary>
        /// The _eccp editions type repository.
        /// </summary>
        private readonly IRepository<ECCPEditionsType> _eccpEditionsTypeRepository;

        /// <summary>
        /// The _eccp maintenance company audit log repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceCompanyAuditLog, int> _eccpMaintenanceCompanyAuditLogRepository;

        /// <summary>
        /// The _eccp maintenance company extension repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceCompanyExtension> _eccpMaintenanceCompanyExtensionRepository;

        /// <summary>
        /// The _eccp maintenance company repository.
        /// </summary>
        private readonly IRepository<ECCPBaseMaintenanceCompany> _eccpMaintenanceCompanyRepository;

        /// <summary>
        /// The _eccp property company audit log repository.
        /// </summary>
        private readonly IRepository<EccpPropertyCompanyAuditLog, int> _eccpPropertyCompanyAuditLogRepository;

        /// <summary>
        /// The _eccp property company extension repository.
        /// </summary>
        private readonly IRepository<EccpPropertyCompanyExtension> _eccpPropertyCompanyExtensionRepository;

        /// <summary>
        /// The _eccp property company repository.
        /// </summary>
        private readonly IRepository<ECCPBasePropertyCompany> _eccpPropertyCompanyRepository;

        /// <summary>
        /// The _subscribable edition repository.
        /// </summary>
        private readonly IRepository<SubscribableEdition> _subscribableEditionRepository;

        /// <summary>
        /// The _tenant manager.
        /// </summary>
        private readonly TenantManager _tenantManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpCompanyAuditsAppService"/> class.
        /// </summary>
        /// <param name="tenantManager">
        /// The tenant manager.
        /// </param>
        /// <param name="subscribableEditionRepository">
        /// The subscribable edition repository.
        /// </param>
        /// <param name="eccpMaintenanceCompanyRepository">
        /// The eccp maintenance company repository.
        /// </param>
        /// <param name="eccpPropertyCompanyRepository">
        /// The eccp property company repository.
        /// </param>
        /// <param name="eccpMaintenanceCompanyExtensionRepository">
        /// The eccp maintenance company extension repository.
        /// </param>
        /// <param name="eccpPropertyCompanyExtensionRepository">
        /// The eccp property company extension repository.
        /// </param>
        /// <param name="eccpEditionsTypeRepository">
        /// The eccp editions type repository.
        /// </param>
        /// <param name="eccpMaintenanceCompanyAuditLogRepository">
        /// The eccp maintenance company audit log repository.
        /// </param>
        /// <param name="eccpPropertyCompanyAuditLogRepository">
        /// The eccp property company audit log repository.
        /// </param>
        /// <param name="eccpBaseAreaRepository">
        /// The e ccp base area repository.
        /// </param>
        public EccpCompanyAuditsAppService(
            TenantManager tenantManager,
            IRepository<SubscribableEdition> subscribableEditionRepository,
            IRepository<ECCPBaseMaintenanceCompany> eccpMaintenanceCompanyRepository,
            IRepository<ECCPBasePropertyCompany> eccpPropertyCompanyRepository,
            IRepository<EccpMaintenanceCompanyExtension> eccpMaintenanceCompanyExtensionRepository,
            IRepository<EccpPropertyCompanyExtension> eccpPropertyCompanyExtensionRepository,
            IRepository<ECCPEditionsType> eccpEditionsTypeRepository,
            IRepository<EccpMaintenanceCompanyAuditLog> eccpMaintenanceCompanyAuditLogRepository,
            IRepository<EccpPropertyCompanyAuditLog> eccpPropertyCompanyAuditLogRepository,
            IRepository<ECCPBaseArea, int> eccpBaseAreaRepository)
        {
            this._subscribableEditionRepository = subscribableEditionRepository;
            this._tenantManager = tenantManager;
            this._eccpBaseAreaRepository = eccpBaseAreaRepository;
            this._eccpEditionsTypeRepository = eccpEditionsTypeRepository;
            this._eccpMaintenanceCompanyRepository = eccpMaintenanceCompanyRepository;
            this._eccpPropertyCompanyRepository = eccpPropertyCompanyRepository;
            this._eccpMaintenanceCompanyExtensionRepository = eccpMaintenanceCompanyExtensionRepository;
            this._eccpPropertyCompanyExtensionRepository = eccpPropertyCompanyExtensionRepository;
            this._eccpMaintenanceCompanyAuditLogRepository = eccpMaintenanceCompanyAuditLogRepository;
            this._eccpPropertyCompanyAuditLogRepository = eccpPropertyCompanyAuditLogRepository;
        }

        /// <summary>
        /// The edit company audit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="UserFriendlyException">
        /// The userFriendlyException.
        /// </exception>
        [AbpAuthorize(AppPermissions.Pages_Administration_EccpCompanyAudits_Edit)]
        public async Task EditCompanyAudit(EditCompanyAuditDto input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var tenant = await this._tenantManager.GetByIdAsync(input.Id);
                if (tenant == null)
                {
                    throw new UserFriendlyException(this.L("TenantIsNull"));
                }

                var edition = await this._subscribableEditionRepository.GetAsync(tenant.EditionId.GetValueOrDefault(0));
                if (edition == null)
                {
                    throw new UserFriendlyException(this.L("EditionIsNull"));
                }

                var editionType =
                    await this._eccpEditionsTypeRepository.GetAsync(edition.ECCPEditionsTypeId.GetValueOrDefault(0));
                if (editionType == null)
                {
                    throw new UserFriendlyException(this.L("EditionsTypeIsNull"));
                }

                if (editionType.Name.Contains("维保"))
                {
                    var companyInfo =
                        await this._eccpMaintenanceCompanyRepository.FirstOrDefaultAsync(e => e.TenantId == input.Id);
                    if (companyInfo == null)
                    {
                        throw new UserFriendlyException(this.L("CompanyInfoIsNull"));
                    }

                    companyInfo.IsAudit = input.CheckState;
                    companyInfo.LastModificationTime = DateTime.Now;
                    await this._eccpMaintenanceCompanyRepository.UpdateAsync(companyInfo);

                    var auditLog = new EccpMaintenanceCompanyAuditLog
                                       {
                                           CheckState = input.CheckState,
                                           Remarks = input.Remarks,
                                           MaintenanceCompanyId = companyInfo.Id
                                       };
                    await this._eccpMaintenanceCompanyAuditLogRepository.InsertAsync(auditLog);
                    if (input.CheckState)
                    {
                        await this._tenantManager.UpdateTenantAsync(
                            input.Id,
                            input.CheckState,
                            false,
                            null,
                            edition.Id,
                            EditionPaymentType.Extend);
                    }
                }
                else if (editionType.Name.Contains("物业"))
                {
                    var companyInfo =
                        await this._eccpPropertyCompanyRepository.FirstOrDefaultAsync(e => e.TenantId == input.Id);
                    if (companyInfo == null)
                    {
                        throw new UserFriendlyException(this.L("CompanyInfoIsNull"));
                    }

                    companyInfo.IsAudit = input.CheckState;
                    companyInfo.LastModificationTime = DateTime.Now;
                    await this._eccpPropertyCompanyRepository.UpdateAsync(companyInfo);

                    var auditLog = new EccpPropertyCompanyAuditLog
                                       {
                                           CheckState = input.CheckState,
                                           Remarks = input.Remarks,
                                           PropertyCompanyId = companyInfo.Id
                                       };
                    await this._eccpPropertyCompanyAuditLogRepository.InsertAsync(auditLog);
                    if (input.CheckState)
                    {
                        await this._tenantManager.UpdateTenantAsync(
                            input.Id,
                            input.CheckState,
                            false,
                            null,
                            edition.Id,
                            EditionPaymentType.Extend);
                    }
                }
            }
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
        public async Task<PagedResultDto<GetEccpCompanyAuditForView>> GetAll(GetAllEccpCompanyAuditInput input)
        {
            // TODO: 此处查询进行分页时待优化。

            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var filteredTenants = this._tenantManager.Tenants.Include(t => t.Edition)
                    .Where(e => e.IsActive == false).WhereIf(
                        !string.IsNullOrWhiteSpace(input.Filter),
                        t => t.Name.Contains(input.Filter) || t.TenancyName.Contains(input.Filter));

                var query =
                    (from o in filteredTenants
                     join o1 in this._subscribableEditionRepository.GetAll() on o.EditionId equals o1.Id into j1
                     from s1 in j1.DefaultIfEmpty()
                     join o2 in this._eccpEditionsTypeRepository.GetAll() on s1.ECCPEditionsTypeId equals o2.Id into j2
                     from s2 in j2.DefaultIfEmpty()
                     join o3 in this._eccpMaintenanceCompanyRepository.GetAll() on o.Id equals o3.TenantId into j3
                     from s3 in j3.DefaultIfEmpty()
                     join o4 in this._eccpMaintenanceCompanyExtensionRepository.GetAll() on s3.Id equals
                         o4.MaintenanceCompanyId into j4
                     from s4 in j4.DefaultIfEmpty()
                     join o5 in this._eccpBaseAreaRepository.GetAll() on s3.ProvinceId equals o5.Id into j5
                     from s5 in j5.DefaultIfEmpty()
                     join o6 in this._eccpBaseAreaRepository.GetAll() on s3.CityId equals o6.Id into j6
                     from s6 in j6.DefaultIfEmpty()
                     join o7 in this._eccpBaseAreaRepository.GetAll() on s3.DistrictId equals o7.Id into j7
                     from s7 in j7.DefaultIfEmpty()
                     select new GetEccpCompanyAuditForView
                                {
                                    EccpCompanyInfo = this.ObjectMapper.Map<EccpCompanyInfoDto>(s3),
                                    EccpCompanyInfoExtension = this.ObjectMapper.Map<EccpCompanyInfoExtensionDto>(s4),
                                    CheckStateName = s3.IsAudit == null ? "待审核" : "未通过",
                                    CityName = s6.Name,
                                    DistrictName = s7.Name,
                                    ProvinceName = s5.Name,
                                    EditionTypeName = s2.Name,
                                    Id = o.Id
                                }).Where(e => e.EditionTypeName == "维保公司").Concat(
                        (from o in filteredTenants
                         join o1 in this._subscribableEditionRepository.GetAll() on o.EditionId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         join o2 in this._eccpEditionsTypeRepository.GetAll() on s1.ECCPEditionsTypeId equals o2.Id into
                             j2
                         from s2 in j2.DefaultIfEmpty()
                         join o3 in this._eccpPropertyCompanyRepository.GetAll() on o.Id equals o3.TenantId into j3
                         from s3 in j3.DefaultIfEmpty()
                         join o4 in this._eccpPropertyCompanyExtensionRepository.GetAll() on s3.Id equals
                             o4.PropertyCompanyId into j4
                         from s4 in j4.DefaultIfEmpty()
                         join o5 in this._eccpBaseAreaRepository.GetAll() on s3.ProvinceId equals o5.Id into j5
                         from s5 in j5.DefaultIfEmpty()
                         join o6 in this._eccpBaseAreaRepository.GetAll() on s3.CityId equals o6.Id into j6
                         from s6 in j6.DefaultIfEmpty()
                         join o7 in this._eccpBaseAreaRepository.GetAll() on s3.DistrictId equals o7.Id into j7
                         from s7 in j7.DefaultIfEmpty()
                         select new GetEccpCompanyAuditForView
                                    {
                                        EccpCompanyInfo = this.ObjectMapper.Map<EccpCompanyInfoDto>(s3),
                                        EccpCompanyInfoExtension =
                                            this.ObjectMapper.Map<EccpCompanyInfoExtensionDto>(s4),
                                        CheckStateName = s3.IsAudit == null ? "待审核" : "未通过",
                                        CityName = s6.Name,
                                        DistrictName = s7.Name,
                                        ProvinceName = s5.Name,
                                        EditionTypeName = s2.Name,
                                        Id = o.Id
                                    }).Where(e => e.EditionTypeName == "物业公司"));

                var totalCount = await query.CountAsync();

                var eccpCompanyAudits = await query.OrderBy(input.Sorting ?? "eccpCompanyInfo.id asc").PageBy(input)
                                            .ToListAsync();

                return new PagedResultDto<GetEccpCompanyAuditForView>(totalCount, eccpCompanyAudits);
            }
        }

        /// <summary>
        /// The get company audit for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="UserFriendlyException">
        /// The userFriendlyException.
        /// </exception>
        [AbpAuthorize(AppPermissions.Pages_Administration_EccpCompanyAudits_Edit)]
        public async Task<GetEccpCompanyAuditForView> GetCompanyAuditForEdit(EntityDto input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var tenant = await this._tenantManager.GetByIdAsync(input.Id);
                if (tenant == null)
                {
                    throw new UserFriendlyException(this.L("TenantIsNull"));
                }

                var edition = await this._subscribableEditionRepository.GetAsync(tenant.EditionId.GetValueOrDefault(0));
                if (edition == null)
                {
                    throw new UserFriendlyException(this.L("EditionIsNull"));
                }

                var editionType =
                    await this._eccpEditionsTypeRepository.GetAsync(edition.ECCPEditionsTypeId.GetValueOrDefault(0));
                if (editionType == null)
                {
                    throw new UserFriendlyException(this.L("EditionsTypeIsNull"));
                }

                var model = new GetEccpCompanyAuditForView();
                if (editionType.Name.Contains("维保"))
                {
                    var companyInfo =
                        await this._eccpMaintenanceCompanyRepository.FirstOrDefaultAsync(e => e.TenantId == input.Id);
                    if (companyInfo == null)
                    {
                        throw new UserFriendlyException(this.L("CompanyInfoIsNull"));
                    }

                    var companyInfoExtension =
                        await this._eccpMaintenanceCompanyExtensionRepository.FirstOrDefaultAsync(
                            e => e.MaintenanceCompanyId == companyInfo.Id);
                    if (companyInfoExtension == null)
                    {
                        throw new UserFriendlyException(this.L("CompanyInfoExtensionIsNull"));
                    }

                    model.EccpCompanyInfo = this.ObjectMapper.Map<EccpCompanyInfoDto>(companyInfo);
                    model.EccpCompanyInfoExtension =
                        this.ObjectMapper.Map<EccpCompanyInfoExtensionDto>(companyInfoExtension);
                    model.Id = input.Id;
                    model.CheckStateName = tenant.IsActive ? "已审核" : "未审核";
                    model.EditionTypeName = editionType.Name;
                    model.ProvinceName = companyInfo.ProvinceId.HasValue
                                             ? (await this._eccpBaseAreaRepository.GetAsync(
                                                    companyInfo.ProvinceId.GetValueOrDefault(0))).Name
                                             : string.Empty;
                    model.CityName = companyInfo.CityId.HasValue
                                         ? (await this._eccpBaseAreaRepository.GetAsync(
                                                companyInfo.CityId.GetValueOrDefault(0))).Name
                                         : string.Empty;
                    model.ProvinceName = companyInfo.DistrictId.HasValue
                                             ? (await this._eccpBaseAreaRepository.GetAsync(
                                                    companyInfo.DistrictId.GetValueOrDefault(0))).Name
                                             : string.Empty;
                }
                else if (editionType.Name.Contains("物业"))
                {
                    var companyInfo =
                        await this._eccpPropertyCompanyRepository.FirstOrDefaultAsync(e => e.TenantId == input.Id);
                    if (companyInfo == null)
                    {
                        throw new UserFriendlyException(this.L("CompanyInfoIsNull"));
                    }

                    var companyInfoExtension =
                        await this._eccpPropertyCompanyExtensionRepository.FirstOrDefaultAsync(
                            e => e.PropertyCompanyId == companyInfo.Id);
                    if (companyInfoExtension == null)
                    {
                        throw new UserFriendlyException(this.L("CompanyInfoExtensionIsNull"));
                    }

                    model.EccpCompanyInfo = this.ObjectMapper.Map<EccpCompanyInfoDto>(companyInfo);
                    model.EccpCompanyInfoExtension =
                        this.ObjectMapper.Map<EccpCompanyInfoExtensionDto>(companyInfoExtension);
                    model.Id = input.Id;
                    model.CheckStateName = tenant.IsActive ? "已审核" : "未审核";
                    model.EditionTypeName = editionType.Name;
                    model.ProvinceName = companyInfo.ProvinceId.HasValue
                                             ? (await this._eccpBaseAreaRepository.GetAsync(
                                                    companyInfo.ProvinceId.GetValueOrDefault(0))).Name
                                             : string.Empty;
                    model.CityName = companyInfo.CityId.HasValue
                                         ? (await this._eccpBaseAreaRepository.GetAsync(
                                                companyInfo.CityId.GetValueOrDefault(0))).Name
                                         : string.Empty;
                    model.ProvinceName = companyInfo.DistrictId.HasValue
                                             ? (await this._eccpBaseAreaRepository.GetAsync(
                                                    companyInfo.DistrictId.GetValueOrDefault(0))).Name
                                             : string.Empty;
                }

                return model;
            }
        }

        /// <summary>
        /// The get company audit logs.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="UserFriendlyException">
        /// The userFriendlyException.
        /// </exception>
        public async Task<PagedResultDto<GetEccpCompanyAuditLogForView>> GetCompanyAuditLogs(
            GetAllEccpCompanyAuditLogInput input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var tenant = await this._tenantManager.GetByIdAsync(input.TenantId);
                if (tenant == null)
                {
                    throw new UserFriendlyException(this.L("TenantIsNull"));
                }

                var edition = await this._subscribableEditionRepository.GetAsync(tenant.EditionId.GetValueOrDefault(0));
                if (edition == null)
                {
                    throw new UserFriendlyException(this.L("EditionIsNull"));
                }

                var editionType =
                    await this._eccpEditionsTypeRepository.GetAsync(edition.ECCPEditionsTypeId.GetValueOrDefault(0));
                if (editionType == null)
                {
                    throw new UserFriendlyException(this.L("EditionsTypeIsNull"));
                }

                if (editionType.Name.Contains("维保"))
                {
                    var companyInfo =
                        await this._eccpMaintenanceCompanyRepository.FirstOrDefaultAsync(
                            e => e.TenantId == input.TenantId);
                    if (companyInfo == null)
                    {
                        throw new UserFriendlyException(this.L("CompanyInfoIsNull"));
                    }

                    var filteredAuditLogs = this._eccpMaintenanceCompanyAuditLogRepository.GetAll().WhereIf(
                            !string.IsNullOrWhiteSpace(input.Filter),
                            e => e.Remarks.Contains(input.Filter))
                        .Where(e => e.MaintenanceCompanyId == companyInfo.Id);

                    var query = from o in filteredAuditLogs
                                select new GetEccpCompanyAuditLogForView
                                           {
                                               Id = o.Id,
                                               CheckStateName = o.CheckState ? "已通过" : "未通过",
                                               Remarks = o.Remarks
                                           };

                    var totalCount = await query.CountAsync();

                    var eccpCompanyAuditLogs =
                        await query.OrderBy(input.Sorting ?? "id asc").PageBy(input).ToListAsync();

                    return new PagedResultDto<GetEccpCompanyAuditLogForView>(totalCount, eccpCompanyAuditLogs);
                }

                if (editionType.Name.Contains("物业"))
                {
                    var companyInfo =
                        await this._eccpPropertyCompanyRepository.FirstOrDefaultAsync(
                            e => e.TenantId == input.TenantId);
                    if (companyInfo == null)
                    {
                        throw new UserFriendlyException(this.L("CompanyInfoIsNull"));
                    }

                    var filteredAuditLogs = this._eccpPropertyCompanyAuditLogRepository.GetAll().WhereIf(
                            !string.IsNullOrWhiteSpace(input.Filter),
                            e => e.Remarks.Contains(input.Filter))
                        .Where(e => e.PropertyCompanyId == companyInfo.Id);

                    var query = from o in filteredAuditLogs
                                select new GetEccpCompanyAuditLogForView
                                           {
                                               Id = o.Id,
                                               CheckStateName = o.CheckState ? "已通过" : "未通过",
                                               Remarks = o.Remarks
                                           };

                    var totalCount = await query.CountAsync();

                    var eccpCompanyAuditLogs =
                        await query.OrderBy(input.Sorting ?? "id asc").PageBy(input).ToListAsync();

                    return new PagedResultDto<GetEccpCompanyAuditLogForView>(totalCount, eccpCompanyAuditLogs);
                }

                throw new UserFriendlyException(this.L("CompanyAuditLogIsNull"));
            }
        }
    }
}