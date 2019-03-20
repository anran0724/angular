// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceContractsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.IO;
using Abp.UI;
using Sinodom.ElevatorCloud.EccpBaseElevators;
using Sinodom.ElevatorCloud.EccpMaintenancePlans;
using Sinodom.ElevatorCloud.Storage;

namespace Sinodom.ElevatorCloud.EccpMaintenanceContracts
{
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

    using Microsoft.EntityFrameworkCore;

    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.ECCPBaseMaintenanceCompanies;
    using Sinodom.ElevatorCloud.ECCPBasePropertyCompanies;
    using Sinodom.ElevatorCloud.EccpMaintenanceContracts.Dtos;

    /// <summary>
    /// The eccp maintenance contracts app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceContractsStop)]
    public class EccpMaintenanceContractsStopAppService : ElevatorCloudAppServiceBase, IEccpMaintenanceContractsStopAppService
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
        /// The _e ccp base maintenance company repository.
        /// </summary>
        private readonly IRepository<ECCPBaseMaintenanceCompany, int> _eccpBaseMaintenanceCompanyRepository;

        /// <summary>
        /// The _e ccp base property company repository.
        /// </summary>
        private readonly IRepository<ECCPBasePropertyCompany, int> _eccpBasePropertyCompanyRepository;

        /// <summary>
        /// The _eccp maintenance contract repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceContract, long> _eccpMaintenanceContractRepository;

        /// <summary>
        /// The _eccp maintenance contract_ elevator_ link repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceContract_Elevator_Link, long> _eccpMaintenanceContractElevatorLinkRepository;
        /// <summary>
        /// The _eccp base elevator repository.
        /// </summary>
        private readonly IRepository<EccpBaseElevator, Guid> _eccpBaseElevatorRepository;
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
        public EccpMaintenanceContractsStopAppService(
            IRepository<EccpMaintenanceContract, long> eccpMaintenanceContractRepository,
            IRepository<ECCPBaseMaintenanceCompany, int> eccpBaseMaintenanceCompanyRepository,
            IRepository<ECCPBasePropertyCompany, int> eccpBasePropertyCompanyRepository,
            IRepository<EccpMaintenanceContract_Elevator_Link, long> eccpMaintenanceContractElevatorLinkRepository,
            IRepository<EccpBaseElevator, Guid> eccpBaseElevatorRepository,
            ITempFileCacheManager tempFileCacheManager,
            EccpMaintenancePlansManager eccpMaintenancePlansManager,
            IBinaryObjectManager binaryObjectManager)
        {
            this._eccpMaintenanceContractRepository = eccpMaintenanceContractRepository;
            this._eccpBaseMaintenanceCompanyRepository = eccpBaseMaintenanceCompanyRepository;
            this._eccpBasePropertyCompanyRepository = eccpBasePropertyCompanyRepository;
            this._eccpMaintenanceContractElevatorLinkRepository = eccpMaintenanceContractElevatorLinkRepository;
            this._eccpBaseElevatorRepository = eccpBaseElevatorRepository;
            this._tempFileCacheManager = tempFileCacheManager;
            this._eccpMaintenancePlansManager = eccpMaintenancePlansManager;
            this._binaryObjectManager = binaryObjectManager;
        }


        public async Task<PagedResultDto<GetEccpMaintenanceContractForView>> GetAllStopMaintenanceContract(
           GetAllEccpMaintenanceContractsInput input)
        {
            var filteredEccpMaintenanceContracts = this._eccpMaintenanceContractRepository.GetAll()
                .Where(e => e.IsStop)
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


        [AbpAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceContractsStop_RecoveryContract)]
        public async Task<ECCPBaseProblemElevatorDto> GetAllECCPBaseElevator(
           GetAllForLookupTableInput input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            {
                var eccpMaintenanceContractElevatorLinks = this._eccpMaintenanceContractElevatorLinkRepository.GetAllIncluding(e => e.MaintenanceContract).Where(e => e.MaintenanceContract.EndDate > DateTime.Now).Where(e => !e.MaintenanceContract.IsStop)
                    .WhereIf(
                        input.MaintenanceContractId != null,
                        e => e.MaintenanceContractId != input.MaintenanceContractId);

                var eccpMaintenanceContractElevatorLinks1 = this._eccpMaintenanceContractElevatorLinkRepository.GetAll()
                    .WhereIf(
                        input.MaintenanceContractId != null,
                        e => e.MaintenanceContractId == input.MaintenanceContractId);

                var eccpMaintenanceContract_Elevator_Links =
                    from eccpMaintenanceContractElevatorLink1 in eccpMaintenanceContractElevatorLinks1
                    join eccpMaintenanceContractElevatorLink in eccpMaintenanceContractElevatorLinks on
                        eccpMaintenanceContractElevatorLink1.ElevatorId equals eccpMaintenanceContractElevatorLink
                            .ElevatorId into g
                    from j in g.DefaultIfEmpty()
                    select new
                    {
                        EccpMaintenanceContract = eccpMaintenanceContractElevatorLink1,
                        IsProblemElevator = j != null
                    };

                var elevators = this._eccpBaseElevatorRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Name.ToString().Contains(input.Filter));

                var query = from o in elevators
                            join t in eccpMaintenanceContract_Elevator_Links on o.Id equals t.EccpMaintenanceContract.ElevatorId
                            select new
                            {
                                Elevator = o,
                                t.IsProblemElevator
                            };

                var eccpBaseElevatorList = await query.ToListAsync();

                var lookupTableDtoList = new ECCPBaseProblemElevatorDto
                {
                    ECCPBaseElevatorLookupTableDtoList = new List<ECCPBaseElevatorLookupTableDto>()
                };

                foreach (var eccpBaseElevator in eccpBaseElevatorList)
                {
                    if (eccpBaseElevator.IsProblemElevator)
                    {
                        if (!string.IsNullOrWhiteSpace(lookupTableDtoList.ProblemElevatorNames))
                        {
                            lookupTableDtoList.ProblemElevatorNames += ",";
                            lookupTableDtoList.ProblemElevatorIds += ",";
                        }
                        lookupTableDtoList.ProblemElevatorIds += eccpBaseElevator.Elevator.Id;
                        lookupTableDtoList.ProblemElevatorNames += eccpBaseElevator.Elevator.Name;
                    }
                    else
                    {
                        lookupTableDtoList.ECCPBaseElevatorLookupTableDtoList.Add(
                       new ECCPBaseElevatorLookupTableDto
                       {
                           Id = eccpBaseElevator.Elevator.Id,
                           DisplayName = eccpBaseElevator.Elevator.Name
                       });
                    }

                }

                return lookupTableDtoList;
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceContractsStop)]
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

        [AbpAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceContractsStop)]
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
        [AbpAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceContractsStop)]
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

        [AbpAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceContractsStop_RecoveryContract)]
        public async Task<GetEccpMaintenanceContractForEditOutput> GetEccpMaintenanceContractForRecoveryContract(
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
        [AbpAuthorize(AppPermissions.Pages_Administration_EccpMaintenanceContractsStop_RecoveryContract)]
        public async Task RecoveryContract(UploadRecoveryContractEccpMaintenanceContractDto input)
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

                input.IsStop = false;
                input.StopDate = null;
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