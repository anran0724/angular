// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpElevatorQrCodesAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpElevatorQrCodes
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.Authorization;
    using Abp.AutoMapper;
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Abp.Linq.Extensions;

    using Microsoft.EntityFrameworkCore;

    using QRCoder;

    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpBaseElevators;
    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;
    using Sinodom.ElevatorCloud.EccpElevatorQrCodes.Dtos;
    using Sinodom.ElevatorCloud.EccpElevatorQrCodes.Exporting;
    using Sinodom.ElevatorCloud.Editions;
    using Sinodom.ElevatorCloud.MultiTenancy;
    using Sinodom.ElevatorCloud.Storage;

    /// <summary>
    ///     The eccp elevator qr codes app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpElevatorQrCodes)]
    public class EccpElevatorQrCodesAppService : ElevatorCloudAppServiceBase, IEccpElevatorQrCodesAppService
    {
        /// <summary>
        ///     The _binary object manager.
        /// </summary>
        private readonly IBinaryObjectManager _binaryObjectManager;

        /// <summary>
        ///     The _binary object repository.
        /// </summary>
        private readonly IRepository<BinaryObject, Guid> _binaryObjectRepository;

        /// <summary>
        ///     The _eccp base elevator repository.
        /// </summary>
        private readonly IRepository<EccpBaseElevator, Guid> _eccpBaseElevatorRepository;

        /// <summary>
        ///     The _eccp elevator qr code bind log repository.
        /// </summary>
        private readonly IRepository<EccpElevatorQrCodeBindLog, Guid> _eccpElevatorQrCodeBindLogRepository;

        /// <summary>
        ///     The _eccp elevator qr code repository.
        /// </summary>
        private readonly IRepository<EccpElevatorQrCode, Guid> _eccpElevatorQrCodeRepository;

        /// <summary>
        ///     The _eccp elevator qr codes excel exporter.
        /// </summary>
        private readonly IEccpElevatorQrCodesExcelExporter _eccpElevatorQrCodesExcelExporter;
        private readonly IRepository<Tenant, int> _tenantRepository;
        private readonly IRepository<ECCPEdition, int> _editionRepository;
        private readonly IRepository<ECCPBaseMaintenanceCompany, int> _eccpBaseMaintenanceCompanyRepository;
        private readonly IRepository<ECCPBasePropertyCompany, int> _eccpBasePropertyCompanyRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="EccpElevatorQrCodesAppService"/> class.
        /// </summary>
        /// <param name="eccpElevatorQrCodeRepository">
        /// The eccp elevator qr code repository.
        /// </param>
        /// <param name="eccpElevatorQrCodesExcelExporter">
        /// The eccp elevator qr codes excel exporter.
        /// </param>
        /// <param name="eccpBaseElevatorRepository">
        /// The eccp base elevator repository.
        /// </param>
        /// <param name="binaryObjectManager">
        /// The binary object manager.
        /// </param>
        /// <param name="binaryObjectRepository">
        /// The binary object repository.
        /// </param>
        /// <param name="eccpElevatorQrCodeBindLogRepository">
        /// The eccp elevator qr code bind log repository.
        /// </param>
        public EccpElevatorQrCodesAppService(
            IRepository<EccpElevatorQrCode, Guid> eccpElevatorQrCodeRepository,
            IEccpElevatorQrCodesExcelExporter eccpElevatorQrCodesExcelExporter,
            IRepository<EccpBaseElevator, Guid> eccpBaseElevatorRepository,
            IBinaryObjectManager binaryObjectManager,
            IRepository<BinaryObject, Guid> binaryObjectRepository,
            IRepository<ECCPBaseMaintenanceCompany, int> eccpBaseMaintenanceCompanyRepository,
            IRepository<ECCPBasePropertyCompany, int> eccpBasePropertyCompanyRepository,
            IRepository<Tenant, int> tenantRepository,
            IRepository<EccpElevatorQrCodeBindLog, Guid> eccpElevatorQrCodeBindLogRepository,
            IRepository<ECCPEdition, int> editionRepository)
        {
            this._eccpElevatorQrCodeBindLogRepository = eccpElevatorQrCodeBindLogRepository;
            this._eccpElevatorQrCodeRepository = eccpElevatorQrCodeRepository;
            this._eccpElevatorQrCodesExcelExporter = eccpElevatorQrCodesExcelExporter;
            this._eccpBaseElevatorRepository = eccpBaseElevatorRepository;
            this._binaryObjectManager = binaryObjectManager;
            this._tenantRepository = tenantRepository;
            this._binaryObjectRepository = binaryObjectRepository;
            this._editionRepository = editionRepository;
            this._eccpBaseMaintenanceCompanyRepository = eccpBaseMaintenanceCompanyRepository;
            this._eccpBasePropertyCompanyRepository = eccpBasePropertyCompanyRepository;

        }

        /// <summary>
        /// The binding.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task Binding(CreateOrEditEccpElevatorQrCodeDto input)
        {
            var filteredEccpElevatorQrCodes = this._eccpElevatorQrCodeRepository.GetAll()
                .Where(e => e.ElevatorId == input.ElevatorId);
            if (input.Id != null)
            {
                var eccpElevatorQrCode = await this._eccpElevatorQrCodeRepository.FirstOrDefaultAsync((Guid)input.Id);
                var excessive = await filteredEccpElevatorQrCodes.FirstOrDefaultAsync();
                if (excessive == null)
                {
                    eccpElevatorQrCode.ElevatorId = input.ElevatorId;
                    this.ObjectMapper.Map(eccpElevatorQrCode, eccpElevatorQrCode);
                    var eccpElevatorQrCodeBindLogs = new EccpElevatorQrCodeBindLog
                                                         {
                                                             CreatorUserId = input.CreatorUserId,
                                                             LastModifierUserId = input.LastModifierUserId,
                                                             DeleterUserId = input.DeleterUserId,
                                                             NewElevatorId = input.ElevatorId,
                                                             NewQrCodeId = input.Id,
                                                             TenantId = this.AbpSession.TenantId,
                                                             Remark = this.L("BoundElevator")
                                                         };
                    await this._eccpElevatorQrCodeBindLogRepository.InsertAsync(eccpElevatorQrCodeBindLogs);
                }
                else
                {
                    var eccpElevatorQrCodes =
                        await this._eccpElevatorQrCodeRepository.FirstOrDefaultAsync(excessive.Id);
                    eccpElevatorQrCode.ElevatorId = input.ElevatorId;
                    this.ObjectMapper.Map(eccpElevatorQrCode, eccpElevatorQrCode);
                    eccpElevatorQrCodes.ElevatorId = null;
                    this.ObjectMapper.Map(eccpElevatorQrCodes, eccpElevatorQrCodes);
                    var eccpElevatorQrCodeBindLogs = new EccpElevatorQrCodeBindLog
                                                         {
                                                             CreatorUserId = input.CreatorUserId,
                                                             LastModifierUserId = input.LastModifierUserId,
                                                             DeleterUserId = input.DeleterUserId,
                                                             NewElevatorId = input.ElevatorId,
                                                             TenantId = this.AbpSession.TenantId,
                                                             NewQrCodeId = input.Id,
                                                             OldQrCodeId = eccpElevatorQrCodes.Id,
                                                             Remark = this.L("BoundElevators")
                                                         };
                    await this._eccpElevatorQrCodeBindLogRepository.InsertAsync(eccpElevatorQrCodeBindLogs);
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
        public async Task CreateOrEdit(CreateOrEditEccpElevatorQrCodeDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpElevatorQrCodes_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await this._eccpElevatorQrCodeRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetEccpElevatorQrCodeForView>> GetAll(GetAllEccpElevatorQrCodesInput input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                
                var filteredEccpElevatorQrCodes = this._eccpElevatorQrCodeRepository.GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.AreaName.Contains(input.Filter) || e.ElevatorNum.Contains(input.Filter)
                                                           || e.ImgPicture.Contains(input.Filter))
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.AreaNameFilter),
                    e => e.AreaName.ToLower() == input.AreaNameFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.ElevatorNumFilter),
                    e => e.ElevatorNum.ToLower() == input.ElevatorNumFilter.ToLower().Trim())
                .WhereIf(input.IsInstallFilter > -1, e => Convert.ToInt32(e.IsInstall) == input.IsInstallFilter)
                .WhereIf(input.IsGrantFilter > -1, e => Convert.ToInt32(e.IsGrant) == input.IsGrantFilter)
                .WhereIf(
                    input.MinInstallDateTimeFilter != null,
                    e => e.InstallDateTime >= input.MinInstallDateTimeFilter)
                .WhereIf(
                    input.MaxInstallDateTimeFilter != null,
                    e => e.InstallDateTime <= input.MaxInstallDateTimeFilter)
                .WhereIf(input.MinGrantDateTimeFilter != null, e => e.GrantDateTime >= input.MinGrantDateTimeFilter)
                .WhereIf(input.MaxGrantDateTimeFilter != null, e => e.GrantDateTime <= input.MaxGrantDateTimeFilter);

                if (this.AbpSession.TenantId != null)
                {
                    var tenantModel = await this._tenantRepository.GetAsync(this.AbpSession.TenantId.Value);

                    if (tenantModel.EditionId != null)
                    {
                        var edition = this._editionRepository.Get(tenantModel.EditionId.Value);

                        if (edition.ECCPEditionsTypeId == 2)
                        {
                            int ECCPBaseMaintenanceCompanyId = this._eccpBaseMaintenanceCompanyRepository
                               .FirstOrDefault(e => e.TenantId == this.AbpSession.TenantId.Value).Id;
                            var query1 = (from o in filteredEccpElevatorQrCodes
                                         join o1 in this._eccpBaseElevatorRepository.GetAll() on o.ElevatorId equals o1.Id into j1
                                         from s1 in j1.DefaultIfEmpty()
                                         join o2 in this._binaryObjectRepository.GetAll() on o.ImgPicture equals o2.Id.ToString() into
                                             j2
                                         from s2 in j2.DefaultIfEmpty()
                                         select new
                                         {
                                             s1.ECCPBaseMaintenanceCompanyId,
                                             EccpElevatorQrCode = o,
                                             EccpBaseElevatorName = s1 == null ? string.Empty : s1.Name,
                                             Bytess = s2 == null ? new byte[0] : s2.Bytes
                                         }).Where(e => e.ECCPBaseMaintenanceCompanyId == ECCPBaseMaintenanceCompanyId).WhereIf(
                    !string.IsNullOrWhiteSpace(input.EccpBaseElevatorNameFilter),
                    e => e.EccpBaseElevatorName.ToLower() == input.EccpBaseElevatorNameFilter.ToLower().Trim());

                            var totalCount1 = await query1.CountAsync();

                            var eccpElevatorQrCodes1 = new List<GetEccpElevatorQrCodeForView>();

                            query1.OrderBy(input.Sorting ?? "eccpElevatorQrCode.id asc").PageBy(input).MapTo(eccpElevatorQrCodes1);

                            return new PagedResultDto<GetEccpElevatorQrCodeForView>(totalCount1, eccpElevatorQrCodes1);
                        }
                        else if (edition.ECCPEditionsTypeId == 3)
                        {
                          int ECCPBasePropertyCompanyId = this._eccpBasePropertyCompanyRepository
                                .FirstOrDefault(e => e.TenantId == this.AbpSession.TenantId.Value).Id;
                            var query2 = (from o in filteredEccpElevatorQrCodes
                                         join o1 in this._eccpBaseElevatorRepository.GetAll() on o.ElevatorId equals o1.Id into j1
                                         from s1 in j1.DefaultIfEmpty()
                                         join o2 in this._binaryObjectRepository.GetAll() on o.ImgPicture equals o2.Id.ToString() into
                                             j2
                                         from s2 in j2.DefaultIfEmpty()
                                         select new
                                         {
                                             s1.ECCPBasePropertyCompanyId,
                                             EccpElevatorQrCode = o,
                                             EccpBaseElevatorName = s1 == null ? string.Empty : s1.Name,
                                             Bytess = s2 == null ? new byte[0] : s2.Bytes
                                         }).Where(e => e.ECCPBasePropertyCompanyId == ECCPBasePropertyCompanyId).WhereIf(
                    !string.IsNullOrWhiteSpace(input.EccpBaseElevatorNameFilter),
                    e => e.EccpBaseElevatorName.ToLower() == input.EccpBaseElevatorNameFilter.ToLower().Trim());

                            var totalCount2 = await query2.CountAsync();

                            var eccpElevatorQrCodes2 = new List<GetEccpElevatorQrCodeForView>();

                            query2.OrderBy(input.Sorting ?? "eccpElevatorQrCode.id asc").PageBy(input).MapTo(eccpElevatorQrCodes2);

                            return new PagedResultDto<GetEccpElevatorQrCodeForView>(totalCount2, eccpElevatorQrCodes2);

                        }
                    }
                    else
                    {
                        return new PagedResultDto<GetEccpElevatorQrCodeForView>(0, null);
                    }
                }
                
                    var query = (from o in filteredEccpElevatorQrCodes
                                 join o1 in this._eccpBaseElevatorRepository.GetAll() on o.ElevatorId equals o1.Id into j1
                                 from s1 in j1.DefaultIfEmpty()
                                 join o2 in this._binaryObjectRepository.GetAll() on o.ImgPicture equals o2.Id.ToString() into
                                     j2
                                 from s2 in j2.DefaultIfEmpty()
                                 select new
                                 {
                                     EccpElevatorQrCode = o,
                                     EccpBaseElevatorName = s1 == null ? string.Empty : s1.Name,
                                     Bytess = s2 == null ? new byte[0] : s2.Bytes
                                 }).WhereIf(
                   !string.IsNullOrWhiteSpace(input.EccpBaseElevatorNameFilter),
                   e => e.EccpBaseElevatorName.ToLower() == input.EccpBaseElevatorNameFilter.ToLower().Trim());

                    var totalCount = await query.CountAsync();

                    var eccpElevatorQrCodes = new List<GetEccpElevatorQrCodeForView>();

                    query.OrderBy(input.Sorting ?? "eccpElevatorQrCode.id asc").PageBy(input).MapTo(eccpElevatorQrCodes);

                    return new PagedResultDto<GetEccpElevatorQrCodeForView>(totalCount, eccpElevatorQrCodes);
            }
        }

        /// <summary>
        /// The get all bind.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<PagedResultDto<GetEccpElevatorQrCodeBindLogView>> GetAllbind(
            GetEccpElevatorQrCodeBindLogInput input)
        {
            var filteredEccpElevatorQrCodeBindLog = this._eccpElevatorQrCodeBindLogRepository.GetAll()
                .Where(e => e.NewQrCodeId == input.newId || e.OldQrCodeId == input.newId);
            var query = from o in filteredEccpElevatorQrCodeBindLog
                        join o1 in this._eccpBaseElevatorRepository.GetAll() on o.NewElevatorId equals o1.Id into j1
                        from s1 in j1.DefaultIfEmpty()
                        join o2 in this._eccpElevatorQrCodeRepository.GetAll() on o.NewQrCodeId equals o2.Id into j2
                        from s2 in j2.DefaultIfEmpty()
                        join o3 in this._eccpElevatorQrCodeRepository.GetAll() on o.OldQrCodeId equals o3.Id into j3
                        from s3 in j3.DefaultIfEmpty()
                        join o4 in this._eccpBaseElevatorRepository.GetAll() on o.OldElevatorId equals o4.Id into j4
                        from s4 in j4.DefaultIfEmpty()
                        select new GetEccpElevatorQrCodeBindLogView
                                   {
                                       newaename = s2.AreaName + s2.ElevatorNum,
                                       oleaename = s3.AreaName + s3.ElevatorNum,
                                       newCertificateNum = s1.CertificateNum,
                                       oleCertificateNum = s4.CertificateNum,
                                       Remark = o.Remark
                                   };

            var totalCount = await query.CountAsync();

            var eccpElevatorQrCodeBindLogViewList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<GetEccpElevatorQrCodeBindLogView>();
            foreach (var eccpElevatorQrCodeBindLogView in eccpElevatorQrCodeBindLogViewList)
            {
                lookupTableDtoList.Add(
                    new GetEccpElevatorQrCodeBindLogView
                        {
                            newaename = eccpElevatorQrCodeBindLogView.newaename,
                            oleaename = eccpElevatorQrCodeBindLogView.oleaename,
                            newCertificateNum = eccpElevatorQrCodeBindLogView.newCertificateNum,
                            oleCertificateNum = eccpElevatorQrCodeBindLogView.oleCertificateNum,
                            Remark = eccpElevatorQrCodeBindLogView.Remark
                        });
            }

            return new PagedResultDto<GetEccpElevatorQrCodeBindLogView>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get all eccp base elevator for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpElevatorQrCodes)]
        public async Task<PagedResultDto<EccpBaseElevatorLookupTableDto>> GetAllEccpBaseElevatorForLookupTable(
            GetAllForLookupTableInput input)
        {
            var query = this._eccpBaseElevatorRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpBaseElevatorList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<EccpBaseElevatorLookupTableDto>();
            foreach (var eccpBaseElevator in eccpBaseElevatorList)
            {
                lookupTableDtoList.Add(
                    new EccpBaseElevatorLookupTableDto
                        {
                            Id = eccpBaseElevator.Id.ToString(), DisplayName = eccpBaseElevator.Name
                        });
            }

            return new PagedResultDto<EccpBaseElevatorLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get eccp elevator qr code for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpElevatorQrCodes_Edit)]
        public async Task<GetEccpElevatorQrCodeForEditOutput> GetEccpElevatorQrCodeForEdit(EntityDto<Guid> input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {

                var eccpElevatorQrCode = await this._eccpElevatorQrCodeRepository.FirstOrDefaultAsync(input.Id);

                var output = new GetEccpElevatorQrCodeForEditOutput
                {
                    EccpElevatorQrCode =
                                         this.ObjectMapper.Map<CreateOrEditEccpElevatorQrCodeDto>(eccpElevatorQrCode)
                };

                if (output.EccpElevatorQrCode.ElevatorId != null)
                {
                    var eccpBaseElevator =
                        await this._eccpBaseElevatorRepository.FirstOrDefaultAsync(
                            (Guid)output.EccpElevatorQrCode.ElevatorId);
                    output.EccpBaseElevatorName = eccpBaseElevator.Name;
                    output.EccpBaseElevatorCertificateNum = eccpBaseElevator.CertificateNum;
                }

                return output;
            }

               
        }

        /// <summary>
        /// The get eccp elevator qr codes to excel.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<FileDto> GetEccpElevatorQrCodesToExcel(GetAllEccpElevatorQrCodesForExcelInput input)
        {
            var filteredEccpElevatorQrCodes = this._eccpElevatorQrCodeRepository.GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.AreaName.Contains(input.Filter) || e.ElevatorNum.Contains(input.Filter)
                                                           || e.ImgPicture.Contains(input.Filter))
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.AreaNameFilter),
                    e => e.AreaName.ToLower() == input.AreaNameFilter.ToLower().Trim())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.ElevatorNumFilter),
                    e => e.ElevatorNum.ToLower() == input.ElevatorNumFilter.ToLower().Trim())
                .WhereIf(input.IsInstallFilter > -1, e => Convert.ToInt32(e.IsInstall) == input.IsInstallFilter)
                .WhereIf(input.IsGrantFilter > -1, e => Convert.ToInt32(e.IsGrant) == input.IsGrantFilter)
                .WhereIf(
                    input.MinInstallDateTimeFilter != null,
                    e => e.InstallDateTime >= input.MinInstallDateTimeFilter)
                .WhereIf(
                    input.MaxInstallDateTimeFilter != null,
                    e => e.InstallDateTime <= input.MaxInstallDateTimeFilter)
                .WhereIf(input.MinGrantDateTimeFilter != null, e => e.GrantDateTime >= input.MinGrantDateTimeFilter)
                .WhereIf(input.MaxGrantDateTimeFilter != null, e => e.GrantDateTime <= input.MaxGrantDateTimeFilter);

            var query = (from o in filteredEccpElevatorQrCodes
                         join o1 in this._eccpBaseElevatorRepository.GetAll() on o.ElevatorId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         select new GetEccpElevatorQrCodeForView
                                    {
                                        EccpElevatorQrCode = this.ObjectMapper.Map<EccpElevatorQrCodeDto>(o),
                                        EccpBaseElevatorName = s1 == null ? string.Empty : s1.Name
                                    }).WhereIf(
                !string.IsNullOrWhiteSpace(input.EccpBaseElevatorNameFilter),
                e => e.EccpBaseElevatorName.ToLower() == input.EccpBaseElevatorNameFilter.ToLower().Trim());

            var eccpElevatorQrCodeListDtos = await query.ToListAsync();

            return this._eccpElevatorQrCodesExcelExporter.ExportToFile(eccpElevatorQrCodeListDtos);
        }

        /// <summary>
        /// The modify.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task Modify(CreateOrEditEccpElevatorQrCodeDto input)
        {
            if (input.Id != null)
            {
                var eccpElevatorQrCode = await this._eccpElevatorQrCodeRepository.FirstOrDefaultAsync((Guid)input.Id);
                var filteredEccpElevatorQrCodes = this._eccpElevatorQrCodeRepository.GetAll()
                    .Where(e => e.ElevatorId == input.ElevatorId);
                var excessive = await filteredEccpElevatorQrCodes.FirstOrDefaultAsync();
                if (excessive == null)
                {
                    eccpElevatorQrCode.ElevatorId = input.ElevatorId;
                    this.ObjectMapper.Map(eccpElevatorQrCode, eccpElevatorQrCode);
                }
                else
                {
                    var elevatorIds = eccpElevatorQrCode.ElevatorId;
                    var eccpElevatorQrCodes =
                        await this._eccpElevatorQrCodeRepository.FirstOrDefaultAsync(excessive.Id);
                    eccpElevatorQrCode.ElevatorId = eccpElevatorQrCodes.ElevatorId;
                    this.ObjectMapper.Map(eccpElevatorQrCode, eccpElevatorQrCode);
                    eccpElevatorQrCodes.ElevatorId = elevatorIds;
                    this.ObjectMapper.Map(eccpElevatorQrCodes, eccpElevatorQrCodes);
                    var eccpElevatorQrCodeBindLogs = new EccpElevatorQrCodeBindLog
                                                         {
                                                             NewElevatorId = input.ElevatorId,
                                                             NewQrCodeId = input.Id,
                                                             OldQrCodeId = eccpElevatorQrCodes.Id,
                                                             OldElevatorId = eccpElevatorQrCodes.ElevatorId,
                                                             TenantId = this.AbpSession.TenantId,
                                                             Remark = this.L("Permutation")
                                                         };
                    await this._eccpElevatorQrCodeBindLogRepository.InsertAsync(eccpElevatorQrCodeBindLogs);
                }
            }
        }

        /// <summary>
        /// The modify eccp.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task ModifyEccp(CreateOrEditEccpElevatorQrCodeDto input)
        {
            var filteredEccpElevatorQrCodes = this._eccpElevatorQrCodeRepository.GetAll()
                .Where(e => e.ElevatorId == input.ElevatorId);
            if (input.Id != null)
            {
                var eccpElevatorQrCode = await this._eccpElevatorQrCodeRepository.FirstOrDefaultAsync((Guid)input.Id);
                var excessive = await filteredEccpElevatorQrCodes.FirstOrDefaultAsync();
                if (excessive == null)
                {
                    eccpElevatorQrCode.ElevatorId = input.ElevatorId;
                    this.ObjectMapper.Map(eccpElevatorQrCode, eccpElevatorQrCode);
                    var eccpElevatorQrCodeBindLogs = new EccpElevatorQrCodeBindLog
                                                         {
                                                             NewElevatorId = input.ElevatorId,
                                                             NewQrCodeId = input.Id,
                                                             TenantId = this.AbpSession.TenantId,
                                                             Remark = this.L("ReplaceTheElevators")
                                                         };
                    await this._eccpElevatorQrCodeBindLogRepository.InsertAsync(eccpElevatorQrCodeBindLogs);
                }
                else
                {
                    var eccpElevatorQrCodes =
                        await this._eccpElevatorQrCodeRepository.FirstOrDefaultAsync(excessive.Id);
                    eccpElevatorQrCode.ElevatorId = input.ElevatorId;
                    this.ObjectMapper.Map(eccpElevatorQrCode, eccpElevatorQrCode);
                    eccpElevatorQrCodes.ElevatorId = null;
                    this.ObjectMapper.Map(eccpElevatorQrCodes, eccpElevatorQrCodes);
                    var eccpElevatorQrCodeBindLogs = new EccpElevatorQrCodeBindLog
                                                         {
                                                             TenantId = this.AbpSession.TenantId,
                                                             NewElevatorId = input.ElevatorId,
                                                             NewQrCodeId = input.Id,
                                                             OldQrCodeId = eccpElevatorQrCodes.Id,
                                                             Remark = this.L("ReplaceTheElevators")
                                                         };

                    await this._eccpElevatorQrCodeBindLogRepository.InsertAsync(eccpElevatorQrCodeBindLogs);
                }
            }
        }

        /// <summary>
        /// The modify qr code.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task ModifyQRCode(CreateOrEditEccpElevatorQrCodeDto input)
        {
            var filteredEccpElevatorQrCodes = this._eccpElevatorQrCodeRepository.GetAll()
                .Where(e => e.AreaName == input.AreaName && e.ElevatorNum == input.ElevatorNum);
            if (input.Id != null)
            {
                var eccpElevatorQrCode = await this._eccpElevatorQrCodeRepository.FirstOrDefaultAsync((Guid)input.Id);
                var excessive = await filteredEccpElevatorQrCodes.FirstOrDefaultAsync();
                var eccpElevatorQrCodes = await this._eccpElevatorQrCodeRepository.FirstOrDefaultAsync(excessive.Id);
                eccpElevatorQrCodes.ElevatorId = input.ElevatorId;
                this.ObjectMapper.Map(eccpElevatorQrCodes, eccpElevatorQrCodes);
                eccpElevatorQrCode.ElevatorId = null;
                this.ObjectMapper.Map(eccpElevatorQrCode, eccpElevatorQrCode);
                var eccpElevatorQrCodeBindLogs = new EccpElevatorQrCodeBindLog
                                                     {
                                                         TenantId = this.AbpSession.TenantId,
                                                         NewElevatorId = input.ElevatorId,
                                                         NewQrCodeId = eccpElevatorQrCodes.Id,
                                                         OldQrCodeId = input.Id
                                                     };
                eccpElevatorQrCodeBindLogs.TenantId = this.AbpSession.TenantId;
                eccpElevatorQrCodeBindLogs.Remark = this.L("ReplaceMentCode");
                await this._eccpElevatorQrCodeBindLogRepository.InsertAsync(eccpElevatorQrCodeBindLogs);
            }
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
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpElevatorQrCodes_Create)]
        private async Task Create(CreateOrEditEccpElevatorQrCodeDto input)
        {
            var idArray = input.AreaName + input.ElevatorNum;
            Config.Width = 614;
            Config.Height = 874;
            Config.Width2 = 543;
            Config.Height2 = 543;
            Config.TotalCount = 1;
            Config.Level = QRCodeGenerator.ECCLevel.Q;
            Config.ImageType = "png";
            Config.Bottom = "±¾µçÌÝ±àºÅ";
            Config.BottomSize = 48;
            var url = "http://Wap.dianti119.com/login.aspx";
            var width = Config.Width;
            var height = Config.Height;
            using (var bmpPng = new Bitmap(width, height))
            {
                using (var g = Graphics.FromImage(bmpPng))
                {
                    g.Clear(Color.White);

                    var qrGenerator = new QRCodeGenerator();
                    var content = $"{this.FormatUrl(url)}qrcode={idArray}";
                    var qrCodeData = qrGenerator.CreateQrCode(content, Config.Level);
                    var qrCode = new QRCode(qrCodeData);
                    var bmpCode = qrCode.GetGraphic(20);
                    var ms = new MemoryStream();
                    bmpCode.Save(ms, ImageFormat.Png);
                    var byteArray = ms.ToArray();
                    var storedFile = new BinaryObject(this.AbpSession.TenantId, byteArray);
                    await this._binaryObjectManager.SaveAsync(storedFile);
                    input.ImgPicture = storedFile.Id.ToString();
                    ms.Close();
                }
            }

            var eccpElevatorQrCode = this.ObjectMapper.Map<EccpElevatorQrCode>(input);
            if (this.AbpSession.TenantId != null)
            {
                eccpElevatorQrCode.TenantId = this.AbpSession.TenantId;
            }

            await this._eccpElevatorQrCodeRepository.InsertAsync(eccpElevatorQrCode);
        }

        /// <summary>
        /// The format url.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string FormatUrl(string url)
        {
            string temp;
            if (url.IndexOf('?') == -1)
            {
                temp = url + "?";
            }
            else
            {
                temp = url + "&";
            }

            return temp;
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
        [AbpAuthorize(AppPermissions.Pages_EccpElevator_EccpElevatorQrCodes_Edit)]
        private async Task Update(CreateOrEditEccpElevatorQrCodeDto input)
        {
            if (input.Id != null)
            {
                var eccpElevatorQrCode = await this._eccpElevatorQrCodeRepository.FirstOrDefaultAsync((Guid)input.Id);
                eccpElevatorQrCode.IsGrant = input.IsGrant;
                eccpElevatorQrCode.IsInstall = input.IsInstall;

                if (eccpElevatorQrCode.IsGrant)
                {
                    eccpElevatorQrCode.GrantDateTime = input.GrantDateTime;
                }
                else if (eccpElevatorQrCode.IsGrant == false)
                {
                    eccpElevatorQrCode.GrantDateTime = null;
                }

                if (eccpElevatorQrCode.IsInstall)
                {
                    eccpElevatorQrCode.InstallDateTime = input.InstallDateTime;
                }
                else if (eccpElevatorQrCode.IsInstall == false)
                {
                    eccpElevatorQrCode.InstallDateTime = null;
                }

                this.ObjectMapper.Map(eccpElevatorQrCode, eccpElevatorQrCode);
            }
        }
    }

    /// <summary>
    ///     The config.
    /// </summary>
    public class Config
    {
        /// <summary>
        ///     Gets or sets the bottom.
        /// </summary>
        public static string Bottom { get; set; }

        /// <summary>
        ///     Gets or sets the bottom size.
        /// </summary>
        public static float BottomSize { get; set; }

        /// <summary>
        ///     Gets or sets the height.
        /// </summary>
        public static int Height { get; set; }

        /// <summary>
        ///     Gets or sets the height 2.
        /// </summary>
        public static int Height2 { get; set; }

        /// <summary>
        ///     Gets or sets the image type.
        /// </summary>
        public static string ImageType { get; set; }

        /// <summary>
        ///     Gets or sets the level.
        /// </summary>
        public static QRCodeGenerator.ECCLevel Level { get; set; }

        /// <summary>
        ///     Gets or sets the total count.
        /// </summary>
        public static int TotalCount { get; set; }

        /// <summary>
        ///     Gets or sets the width.
        /// </summary>
        public static int Width { get; set; }

        /// <summary>
        ///     Gets or sets the width 2.
        /// </summary>
        public static int Width2 { get; set; }
    }
}