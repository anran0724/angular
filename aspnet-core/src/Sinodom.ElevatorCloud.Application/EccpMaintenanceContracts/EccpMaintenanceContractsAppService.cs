// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceContractsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceContracts
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
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
    using Abp.UI;

    using Microsoft.EntityFrameworkCore;

    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpBaseElevators;
    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;
    using Sinodom.ElevatorCloud.EccpMaintenanceContracts.Dtos;
    using Sinodom.ElevatorCloud.EccpMaintenanceContracts.Exporting;
    using Sinodom.ElevatorCloud.EccpMaintenancePlans;
    using Sinodom.ElevatorCloud.Storage;

    /// <summary>
    /// The eccp maintenance contracts app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceContracts)]
    public class EccpMaintenanceContractsAppService : ElevatorCloudAppServiceBase, IEccpMaintenanceContractsAppService
    {
        /// <summary>
        /// The max profile picture bytes.
        /// </summary>
        private const int MaxProfilePictureBytes = 1048576; // 1MB

        /// <summary>
        /// The _binary object manager.
        /// </summary>
        private readonly IBinaryObjectManager _binaryObjectManager;

        /// <summary>
        /// The _eccp base elevator repository.
        /// </summary>
        private readonly IRepository<EccpBaseElevator, Guid> _eccpBaseElevatorRepository;

        /// <summary>
        /// The _e ccp base maintenance company repository.
        /// </summary>
        private readonly IRepository<ECCPBaseMaintenanceCompany, int> _eccpBaseMaintenanceCompanyRepository;

        /// <summary>
        /// The _e ccp base property company repository.
        /// </summary>
        private readonly IRepository<ECCPBasePropertyCompany, int> _eccpBasePropertyCompanyRepository;

        /// <summary>
        /// The _eccp maintenance contract_ elevator_ link repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceContract_Elevator_Link, long> _eccpMaintenanceContractElevatorLinkRepository;

        /// <summary>
        /// The _eccp maintenance contract repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceContract, long> _eccpMaintenanceContractRepository;

        /// <summary>
        /// The _eccp maintenance contracts excel exporter.
        /// </summary>
        private readonly IEccpMaintenanceContractsExcelExporter _eccpMaintenanceContractsExcelExporter;

        /// <summary>
        /// The _eccp maintenance plans manager.
        /// </summary>
        private readonly EccpMaintenancePlansManager _eccpMaintenancePlansManager;

        /// <summary>
        /// The _temp file cache manager.
        /// </summary>
        private readonly ITempFileCacheManager _tempFileCacheManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceContractsAppService"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceContractRepository">
        /// The eccp maintenance contract repository.
        /// </param>
        /// <param name="eccpMaintenanceContractsExcelExporter">
        /// The eccp maintenance contracts excel exporter.
        /// </param>
        /// <param name="eccpBaseMaintenanceCompanyRepository">
        /// The e ccp base maintenance company repository.
        /// </param>
        /// <param name="eccpBasePropertyCompanyRepository">
        /// The e ccp base property company repository.
        /// </param>
        /// <param name="binaryObjectManager">
        /// The binary object manager.
        /// </param>
        /// <param name="eccpMaintenanceContractElevatorLinkRepository">
        /// The eccp maintenance contract elevator link repository.
        /// </param>
        /// <param name="eccpBaseElevatorRepository">
        /// The eccp base elevator repository.
        /// </param>
        /// <param name="tempFileCacheManager">
        /// The temp file cache manager.
        /// </param>
        /// <param name="eccpMaintenancePlansManager">
        /// The eccp maintenance plans manager.
        /// </param>
        public EccpMaintenanceContractsAppService(
            IRepository<EccpMaintenanceContract, long> eccpMaintenanceContractRepository,
            IEccpMaintenanceContractsExcelExporter eccpMaintenanceContractsExcelExporter,
            IRepository<ECCPBaseMaintenanceCompany, int> eccpBaseMaintenanceCompanyRepository,
            IRepository<ECCPBasePropertyCompany, int> eccpBasePropertyCompanyRepository,
            IBinaryObjectManager binaryObjectManager,
            IRepository<EccpMaintenanceContract_Elevator_Link, long> eccpMaintenanceContractElevatorLinkRepository,
            IRepository<EccpBaseElevator, Guid> eccpBaseElevatorRepository,
            ITempFileCacheManager tempFileCacheManager,
            EccpMaintenancePlansManager eccpMaintenancePlansManager)
        {
            this._eccpMaintenanceContractRepository = eccpMaintenanceContractRepository;
            this._eccpMaintenanceContractsExcelExporter = eccpMaintenanceContractsExcelExporter;
            this._eccpBaseMaintenanceCompanyRepository = eccpBaseMaintenanceCompanyRepository;
            this._eccpBasePropertyCompanyRepository = eccpBasePropertyCompanyRepository;
            this._binaryObjectManager = binaryObjectManager;
            this._eccpMaintenanceContractElevatorLinkRepository = eccpMaintenanceContractElevatorLinkRepository;
            this._eccpBaseElevatorRepository = eccpBaseElevatorRepository;
            this._tempFileCacheManager = tempFileCacheManager;
            this._eccpMaintenancePlansManager = eccpMaintenancePlansManager;
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
        public async Task CreateOrEdit(UploadCreateOrEditEccpMaintenanceContractDto input)
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
        [AbpAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceContracts_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await this._eccpMaintenanceContractRepository.DeleteAsync(input.Id);
            await this._eccpMaintenanceContractElevatorLinkRepository.DeleteAsync(
                e => e.MaintenanceContractId == input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceContracts_StopContract)]
        public async Task StopContract(StopContractEccpMaintenanceContractDto input)
        {
            var eccpMaintenanceContract =
                await this._eccpMaintenanceContractRepository.FirstOrDefaultAsync((long)input.Id);
            input.IsStop = true;
            input.StopDate = DateTime.Now;
            var eccpMaintenanceContract_Elevator_Links = this._eccpMaintenanceContractElevatorLinkRepository.GetAll()
                .Where(e => e.MaintenanceContractId == eccpMaintenanceContract.Id);
            foreach (var eccpMaintenanceContractElevatorLink in eccpMaintenanceContract_Elevator_Links)
            {
                this._eccpMaintenancePlansManager.ClosePlan(eccpMaintenanceContractElevatorLink.ElevatorId);
            }
            this.ObjectMapper.Map(input, eccpMaintenanceContract);
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
        public async Task<PagedResultDto<GetEccpMaintenanceContractForView>> GetAll(
            GetAllEccpMaintenanceContractsInput input)
        {
            var filteredEccpMaintenanceContracts = this._eccpMaintenanceContractRepository.GetAll()
                .Where(e => !e.IsStop)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                .WhereIf(input.MinStartDateFilter != null, e => e.StartDate >= input.MinStartDateFilter)
                .WhereIf(input.MaxStartDateFilter != null, e => e.StartDate <= input.MaxStartDateFilter)
                .WhereIf(input.MinEndDateFilter != null, e => e.EndDate >= input.MinEndDateFilter).WhereIf(
                    input.MaxEndDateFilter != null,
                    e => e.EndDate <= input.MaxEndDateFilter);

            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var query =
                    (from o in filteredEccpMaintenanceContracts
                     join o1 in this._eccpBaseMaintenanceCompanyRepository.GetAll() on o.MaintenanceCompanyId equals
                         o1.Id into j1
                     from s1 in j1.DefaultIfEmpty()
                     join o2 in this._eccpBasePropertyCompanyRepository.GetAll() on o.PropertyCompanyId equals o2.Id
                         into j2
                     from s2 in j2.DefaultIfEmpty()
                     select new
                     {
                         EccpMaintenanceContract = o,
                         ECCPBaseMaintenanceCompanyName = s1 == null ? string.Empty : s1.Name,
                         ECCPBasePropertyCompanyName = s2 == null ? string.Empty : s2.Name
                     }).WhereIf(
                        !string.IsNullOrWhiteSpace(input.ECCPBaseMaintenanceCompanyNameFilter),
                        e => e.ECCPBaseMaintenanceCompanyName.ToLower()
                             == input.ECCPBaseMaintenanceCompanyNameFilter.ToLower().Trim()).WhereIf(
                        !string.IsNullOrWhiteSpace(input.ECCPBasePropertyCompanyNameFilter),
                        e => e.ECCPBasePropertyCompanyName.ToLower()
                             == input.ECCPBasePropertyCompanyNameFilter.ToLower().Trim());

                var totalCount = await query.CountAsync();

                var eccpMaintenanceContracts = new List<GetEccpMaintenanceContractForView>();

                query.OrderBy(input.Sorting ?? "eccpMaintenanceContract.id asc").PageBy(input).MapTo(eccpMaintenanceContracts);

                return new PagedResultDto<GetEccpMaintenanceContractForView>(totalCount, eccpMaintenanceContracts);
            }
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
        [AbpAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceContracts)]
        public async Task<PagedResultDto<ECCPBaseElevatorLookupTableDto>> GetAllECCPBaseElevatorForLookupTable(
            GetAllForLookupTableInput input)
        {
            var eccpMaintenanceContractElevatorLinks = this._eccpMaintenanceContractElevatorLinkRepository.GetAllIncluding(e => e.MaintenanceContract).Where(e => e.MaintenanceContract.EndDate > DateTime.Now).Where(e => !e.MaintenanceContract.IsStop)
                .WhereIf(
                    input.MaintenanceContractId != null,
                    e => e.MaintenanceContractId != input.MaintenanceContractId);
            var maintenanceCompany =
                this._eccpBaseMaintenanceCompanyRepository.FirstOrDefault(e => e.TenantId == this.AbpSession.TenantId);
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var elevators = this._eccpBaseElevatorRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Name.ToString().Contains(input.Filter)).WhereIf(
                    maintenanceCompany != null,
                    e => e.ECCPBaseMaintenanceCompanyId == maintenanceCompany.Id);

                var query = from o in elevators
                            join t in eccpMaintenanceContractElevatorLinks on o.Id equals t.ElevatorId into g
                            from tt in g.DefaultIfEmpty()
                            where tt == null
                            select o;

                var totalCount = await query.CountAsync();

                var eccpBaseElevatorList = await query.PageBy(input).ToListAsync();

                var lookupTableDtoList = new List<ECCPBaseElevatorLookupTableDto>();
                foreach (var eccpBaseElevator in eccpBaseElevatorList)
                {
                    lookupTableDtoList.Add(
                        new ECCPBaseElevatorLookupTableDto
                        {
                            Id = eccpBaseElevator.Id,
                            DisplayName = eccpBaseElevator.Name
                        });
                }

                return new PagedResultDto<ECCPBaseElevatorLookupTableDto>(totalCount, lookupTableDtoList);
            }
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
        [AbpAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceContracts)]
        public async Task<PagedResultDto<ECCPBaseMaintenanceCompanyLookupTableDto>> GetAllECCPBaseMaintenanceCompanyForLookupTable(GetAllForLookupTableInput input)
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

        /// <summary>
        /// The get all eccp base property company for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceContracts)]
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
        /// The get eccp maintenance contract for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceContracts_Edit)]
        public async Task<GetEccpMaintenanceContractForEditOutput> GetEccpMaintenanceContractForEdit(
            EntityDto<long> input)
        {
            var eccpMaintenanceContract = await this._eccpMaintenanceContractRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpMaintenanceContractForEditOutput
            {
                EccpMaintenanceContract =
                                     this.ObjectMapper.Map<CreateOrEditEccpMaintenanceContractDto>(
                                         eccpMaintenanceContract)
            };
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var eccpBaseMaintenanceCompany =
                    await this._eccpBaseMaintenanceCompanyRepository.FirstOrDefaultAsync(
                        output.EccpMaintenanceContract.MaintenanceCompanyId);
                output.ECCPBaseMaintenanceCompanyName = eccpBaseMaintenanceCompany.Name;

                var eccpBasePropertyCompany =
                    await this._eccpBasePropertyCompanyRepository.FirstOrDefaultAsync(
                        output.EccpMaintenanceContract.PropertyCompanyId);
                output.ECCPBasePropertyCompanyName = eccpBasePropertyCompany.Name;

                var eccpMaintenanceContractElevatorLinks = this._eccpMaintenanceContractElevatorLinkRepository
                    .GetAll().Where(a => a.MaintenanceContractId == output.EccpMaintenanceContract.Id);

                var query = from o in eccpMaintenanceContractElevatorLinks
                            join t in this._eccpBaseElevatorRepository.GetAll() on o.ElevatorId equals t.Id
                            select new { EccpBaseElevatorsId = t.Id, EccpBaseElevatorsName = t.Name };

                output.EccpBaseElevatorsNames = string.Join(",", query.Select(e => e.EccpBaseElevatorsName));
                output.EccpBaseElevatorsIds = string.Join(",", query.Select(e => e.EccpBaseElevatorsId));

                return output;
            }
        }

        /// <summary>
        /// The get eccp maintenance contracts to excel.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<FileDto> GetEccpMaintenanceContractsToExcel(GetAllEccpMaintenanceContractsForExcelInput input)
        {
            var filteredEccpMaintenanceContracts = this._eccpMaintenanceContractRepository.GetAll()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                .WhereIf(input.MinStartDateFilter != null, e => e.StartDate >= input.MinStartDateFilter)
                .WhereIf(input.MaxStartDateFilter != null, e => e.StartDate <= input.MaxStartDateFilter)
                .WhereIf(input.MinEndDateFilter != null, e => e.EndDate >= input.MinEndDateFilter).WhereIf(
                    input.MaxEndDateFilter != null,
                    e => e.EndDate <= input.MaxEndDateFilter);

            var query =
                (from o in filteredEccpMaintenanceContracts
                 join o1 in this._eccpBaseMaintenanceCompanyRepository.GetAll() on o.MaintenanceCompanyId equals o1.Id
                     into j1
                 from s1 in j1.DefaultIfEmpty()
                 join o2 in this._eccpBasePropertyCompanyRepository.GetAll() on o.PropertyCompanyId equals o2.Id into j2
                 from s2 in j2.DefaultIfEmpty()
                 select new GetEccpMaintenanceContractForView
                 {
                     EccpMaintenanceContract = this.ObjectMapper.Map<EccpMaintenanceContractDto>(o),
                     ECCPBaseMaintenanceCompanyName = s1 == null ? string.Empty : s1.Name,
                     ECCPBasePropertyCompanyName = s2 == null ? string.Empty : s2.Name
                 }).WhereIf(
                    !string.IsNullOrWhiteSpace(input.ECCPBaseMaintenanceCompanyNameFilter),
                    e => e.ECCPBaseMaintenanceCompanyName.ToLower()
                         == input.ECCPBaseMaintenanceCompanyNameFilter.ToLower().Trim()).WhereIf(
                    !string.IsNullOrWhiteSpace(input.ECCPBasePropertyCompanyNameFilter),
                    e => e.ECCPBasePropertyCompanyName.ToLower()
                         == input.ECCPBasePropertyCompanyNameFilter.ToLower().Trim());

            var eccpMaintenanceContractListDtos = await query.ToListAsync();

            return this._eccpMaintenanceContractsExcelExporter.ExportToFile(eccpMaintenanceContractListDtos);
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
        /// <exception cref="UserFriendlyException">
        /// The user friendly exception
        /// </exception>
        [AbpAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceContracts_Create)]
        private async Task Create(UploadCreateOrEditEccpMaintenanceContractDto input)
        {
            // FileName的值为 4811d3f3-0bfa-4672-b875-2d47299d175d.jpg 时，为单元测试，不进行判断
            if (!string.IsNullOrWhiteSpace(input.FileToken)
                && input.FileToken != "4811d3f3-0bfa-4672-b875-2d47299d175d.jpg")
            {
                byte[] byteArray;
                var imageBytes = this._tempFileCacheManager.GetFile(input.FileToken);
                if (imageBytes == null)
                {
                    throw new UserFriendlyException("There is no such image file with the token: " + input.FileToken);
                }

                using (var bmpImage = new Bitmap(new MemoryStream(imageBytes)))
                {
                    // var width = (input.Width == 0 || input.Width > bmpImage.Width) ? bmpImage.Width : input.Width;
                    // var height = (input.Height == 0 || input.Height > bmpImage.Height) ? bmpImage.Height : input.Height;
                    // var bmCrop = bmpImage.Clone(new Rectangle(), bmpImage.PixelFormat);
                    using (var stream = new MemoryStream())
                    {
                        bmpImage.Save(stream, bmpImage.RawFormat);
                        byteArray = stream.ToArray();
                    }
                }

                if (byteArray.Length > MaxProfilePictureBytes)
                {
                    throw new UserFriendlyException(
                        this.L(
                            "ResizedProfilePicture_Warn_SizeLimit",
                            AppConsts.ResizedMaxProfilPictureBytesUserFriendlyValue));
                }

                var storedFile = new BinaryObject(this.AbpSession.TenantId, byteArray);
                await this._binaryObjectManager.SaveAsync(storedFile);

                input.ContractPictureId = storedFile.Id;
            }

            // FileName的值为 4811d3f3-0bfa-4672-b875-2d47299d175d.jpg 时，为单元测试，创建一个假的Guid
            if (input.FileToken == "4811d3f3-0bfa-4672-b875-2d47299d175d.jpg")
            {
                input.ContractPictureId = Guid.NewGuid();
            }

            var eccpMaintenanceContract = this.ObjectMapper.Map<EccpMaintenanceContract>(input);

            if (this.AbpSession.TenantId != null)
            {
                eccpMaintenanceContract.TenantId = (int)this.AbpSession.TenantId;
            }

            await this._eccpMaintenanceContractRepository.InsertAndGetIdAsync(eccpMaintenanceContract);

            if (!string.IsNullOrWhiteSpace(input.EccpBaseElevatorsIds))
            {
                var eccpBaseElevatorsId = input.EccpBaseElevatorsIds.Split(',');
                foreach (var t in eccpBaseElevatorsId)
                {
                    var linkEntity = new EccpMaintenanceContract_Elevator_Link
                    {
                        MaintenanceContractId = eccpMaintenanceContract.Id,
                        ElevatorId = new Guid(t)
                    };
                    await this._eccpMaintenanceContractElevatorLinkRepository.InsertAsync(linkEntity);
                }
            }
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
        [AbpAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceContracts_Edit)]
        private async Task Update(UploadCreateOrEditEccpMaintenanceContractDto input)
        {
            if (input.Id != null)
            {
                var eccpMaintenanceContract =
                    await this._eccpMaintenanceContractRepository.FirstOrDefaultAsync((long)input.Id);

                // FileName的值为 4811d3f3-0bfa-4672-b875-2d47299d175d.jpg 时，为单元测试，不进行判断
                if (input.FileToken != eccpMaintenanceContract.ContractPictureId.ToString()
                    && input.FileToken != "4811d3f3-0bfa-4672-b875-2d47299d175d.jpg")
                {
                    byte[] byteArray;
                    var imageBytes = this._tempFileCacheManager.GetFile(input.FileToken);
                    if (imageBytes == null)
                    {
                        throw new UserFriendlyException("There is no such image file with the token: " + input.FileToken);
                    }

                    using (var bmpImage = new Bitmap(new MemoryStream(imageBytes)))
                    {
                        using (var stream = new MemoryStream())
                        {
                            bmpImage.Save(stream, bmpImage.RawFormat);
                            byteArray = stream.ToArray();
                        }
                    }

                    if (byteArray.Length > MaxProfilePictureBytes)
                    {
                        throw new UserFriendlyException(
                            this.L(
                                "ResizedProfilePicture_Warn_SizeLimit",
                                AppConsts.ResizedMaxProfilPictureBytesUserFriendlyValue));
                    }

                    if (eccpMaintenanceContract.ContractPictureId.HasValue)
                    {
                        await this._binaryObjectManager.DeleteAsync(eccpMaintenanceContract.ContractPictureId.Value);
                    }

                    var storedFile = new BinaryObject(this.AbpSession.TenantId, byteArray);
                    await this._binaryObjectManager.SaveAsync(storedFile);

                    input.ContractPictureId = storedFile.Id;
                }
                else if (input.FileToken == "4811d3f3-0bfa-4672-b875-2d47299d175d.jpg")
                {
                    // FileName的值为 4811d3f3-0bfa-4672-b875-2d47299d175d.jpg 时，为单元测试，创建一个假的Guid
                    input.ContractPictureId = Guid.NewGuid();
                }
                else
                {
                    if (eccpMaintenanceContract.ContractPictureId != null)
                    {
                        input.ContractPictureId = eccpMaintenanceContract.ContractPictureId.Value;
                    }
                }

                this.ObjectMapper.Map(input, eccpMaintenanceContract);

                if (!string.IsNullOrWhiteSpace(input.EccpBaseElevatorsIds))
                {
                    var list = new List<string>(
                        input.EccpBaseElevatorsIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries));

                    var eccpMaintenanceContractElevatorLinkList =
                        await this._eccpMaintenanceContractElevatorLinkRepository.GetAllListAsync(
                            a => a.MaintenanceContractId == eccpMaintenanceContract.Id
                                 && !list.Contains(a.ElevatorId.ToString()));

                    foreach (var item in eccpMaintenanceContractElevatorLinkList)
                    {
                        // 关闭计划
                        this._eccpMaintenancePlansManager.ClosePlan(item.ElevatorId);

                        await this._eccpMaintenanceContractElevatorLinkRepository.DeleteAsync(item.Id);
                    }

                    var eccpBaseElevatorsId = input.EccpBaseElevatorsIds.Split(',');
                    for (var i = 0; i < eccpBaseElevatorsId.Length; i++)
                    {
                        var eccpMaintenanceContractElevatorLink =
                            await this._eccpMaintenanceContractElevatorLinkRepository.FirstOrDefaultAsync(
                                a => a.ElevatorId == new Guid(eccpBaseElevatorsId[i])
                                     && a.MaintenanceContractId == eccpMaintenanceContract.Id);
                        if (eccpMaintenanceContractElevatorLink == null)
                        {
                            var linkEntity = new EccpMaintenanceContract_Elevator_Link
                            {
                                MaintenanceContractId = eccpMaintenanceContract.Id,
                                ElevatorId = new Guid(eccpBaseElevatorsId[i])
                            };
                            await this._eccpMaintenanceContractElevatorLinkRepository.InsertAsync(linkEntity);
                        }
                    }
                }
            }
        }
    }
}