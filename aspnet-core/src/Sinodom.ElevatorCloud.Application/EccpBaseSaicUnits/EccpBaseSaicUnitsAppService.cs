using Sinodom.ElevatorCloud.ECCPBaseAreas;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Sinodom.ElevatorCloud.EccpBaseSaicUnits.Dtos;
using Sinodom.ElevatorCloud.Dto;
using Abp.Application.Services.Dto;
using Sinodom.ElevatorCloud.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Abp.Domain.Uow;
using Microsoft.EntityFrameworkCore;
using Sinodom.ElevatorCloud.MultiTenancy;
using Sinodom.ElevatorCloud.MultiTenancy.Dto;
using Sinodom.ElevatorCloud.MultiTenancy.Payments.Dto;

namespace Sinodom.ElevatorCloud.EccpBaseSaicUnits
{
    [AbpAuthorize(AppPermissions.Pages_Administration_EccpBaseSaicUnits)]
    public class EccpBaseSaicUnitsAppService : ElevatorCloudAppServiceBase, IEccpBaseSaicUnitsAppService
    {
        private readonly IRepository<EccpBaseSaicUnit> _eccpBaseSaicUnitRepository;
        private readonly IRepository<ECCPBaseArea, int> _eccpBaseAreaRepository;
        private readonly ITenantRegistrationAppService _tenantRegistrationAppService;

        public EccpBaseSaicUnitsAppService(IRepository<EccpBaseSaicUnit> eccpBaseSaicUnitRepository, IRepository<ECCPBaseArea, int> eccpBaseAreaRepository, ITenantRegistrationAppService tenantRegistrationAppService)
        {
            _eccpBaseSaicUnitRepository = eccpBaseSaicUnitRepository;
            _eccpBaseAreaRepository = eccpBaseAreaRepository;
            _tenantRegistrationAppService = tenantRegistrationAppService;
        }

        public async Task<PagedResultDto<GetEccpBaseSaicUnitForView>> GetAll(GetAllEccpBaseSaicUnitsInput input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var filteredEccpBaseSaicUnits = _eccpBaseSaicUnitRepository.GetAll()
                    .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                        e => false || e.Name.Contains(input.Filter) || e.Address.Contains(input.Filter) ||
                             e.Telephone.Contains(input.Filter) || e.Summary.Contains(input.Filter))
                    .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),
                        e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
                    .WhereIf(!string.IsNullOrWhiteSpace(input.AddressFilter),
                        e => e.Address.ToLower() == input.AddressFilter.ToLower().Trim())
                    .WhereIf(!string.IsNullOrWhiteSpace(input.TelephoneFilter),
                        e => e.Telephone.ToLower() == input.TelephoneFilter.ToLower().Trim());


                var query = (from o in filteredEccpBaseSaicUnits
                             join o1 in _eccpBaseAreaRepository.GetAll() on o.ProvinceId equals o1.Id into j1
                             from s1 in j1.DefaultIfEmpty()

                             join o2 in _eccpBaseAreaRepository.GetAll() on o.CityId equals o2.Id into j2
                             from s2 in j2.DefaultIfEmpty()

                             join o3 in _eccpBaseAreaRepository.GetAll() on o.DistrictId equals o3.Id into j3
                             from s3 in j3.DefaultIfEmpty()

                             join o4 in _eccpBaseAreaRepository.GetAll() on o.StreetId equals o4.Id into j4
                             from s4 in j4.DefaultIfEmpty()

                             select new GetEccpBaseSaicUnitForView()
                             {
                                 EccpBaseSaicUnit = ObjectMapper.Map<EccpBaseSaicUnitDto>(o),
                                 ECCPBaseAreaName = s1 == null ? "" : s1.Name.ToString(),
                                 ECCPBaseAreaName2 = s2 == null ? "" : s2.Name.ToString(),
                                 ECCPBaseAreaName3 = s3 == null ? "" : s3.Name.ToString(),
                                 ECCPBaseAreaName4 = s4 == null ? "" : s4.Name.ToString()
                             })
                    .WhereIf(!string.IsNullOrWhiteSpace(input.ECCPBaseAreaNameFilter),
                        e => e.ECCPBaseAreaName.ToLower() == input.ECCPBaseAreaNameFilter.ToLower().Trim())
                    .WhereIf(!string.IsNullOrWhiteSpace(input.ECCPBaseAreaName2Filter),
                        e => e.ECCPBaseAreaName2.ToLower() == input.ECCPBaseAreaName2Filter.ToLower().Trim())
                    .WhereIf(!string.IsNullOrWhiteSpace(input.ECCPBaseAreaName3Filter),
                        e => e.ECCPBaseAreaName3.ToLower() == input.ECCPBaseAreaName3Filter.ToLower().Trim())
                    .WhereIf(!string.IsNullOrWhiteSpace(input.ECCPBaseAreaName4Filter),
                        e => e.ECCPBaseAreaName4.ToLower() == input.ECCPBaseAreaName4Filter.ToLower().Trim());

                var totalCount = await query.CountAsync();

                var eccpBaseSaicUnits = await query
                    .OrderBy(input.Sorting ?? "eccpBaseSaicUnit.id asc")
                    .PageBy(input)
                    .ToListAsync();

                return new PagedResultDto<GetEccpBaseSaicUnitForView>(
                    totalCount,
                    eccpBaseSaicUnits
                );
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_EccpBaseSaicUnits_Edit)]
        public async Task<GetEccpBaseSaicUnitForEditOutput> GetEccpBaseSaicUnitForEdit(EntityDto input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var eccpBaseSaicUnit = await _eccpBaseSaicUnitRepository.FirstOrDefaultAsync(input.Id);

                var output = new GetEccpBaseSaicUnitForEditOutput
                { EccpBaseSaicUnit = ObjectMapper.Map<EditEccpBaseSaicUnitDto>(eccpBaseSaicUnit) };

                if (output.EccpBaseSaicUnit.ProvinceId != null)
                {
                    var eccpBaseArea =
                        await _eccpBaseAreaRepository.FirstOrDefaultAsync((int)output.EccpBaseSaicUnit.ProvinceId);
                    output.ECCPBaseAreaName = eccpBaseArea.Name.ToString();
                }

                if (output.EccpBaseSaicUnit.CityId != null)
                {
                    var eccpBaseArea =
                        await _eccpBaseAreaRepository.FirstOrDefaultAsync((int)output.EccpBaseSaicUnit.CityId);
                    output.ECCPBaseAreaName2 = eccpBaseArea.Name.ToString();
                }

                if (output.EccpBaseSaicUnit.DistrictId != null)
                {
                    var eccpBaseArea =
                        await _eccpBaseAreaRepository.FirstOrDefaultAsync((int)output.EccpBaseSaicUnit.DistrictId);
                    output.ECCPBaseAreaName3 = eccpBaseArea.Name.ToString();
                }

                if (output.EccpBaseSaicUnit.StreetId != null)
                {
                    var eccpBaseArea =
                        await _eccpBaseAreaRepository.FirstOrDefaultAsync((int)output.EccpBaseSaicUnit.StreetId);
                    output.ECCPBaseAreaName4 = eccpBaseArea.Name.ToString();
                }

                return output;
            }
        }

        public async Task CreateOrEdit(CreateOrEditEccpBaseSaicUnitDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_EccpBaseSaicUnits_Create)]
        private async Task Create(CreateOrEditEccpBaseSaicUnitDto input)
        {
            RegisterTenantInput tenant = new RegisterTenantInput
            {
                EditionId = 1,
                SubscriptionStartType = SubscriptionStartType.Free,
                TenancyName = input.TenancyName,
                Name = input.Name,
                AdminPassword = input.AdminPassword,
                AdminEmailAddress = input.AdminEmailAddress,
                LegalPerson = input.LegalPerson,
                Mobile = input.Mobile,
                IsMember = false
            };

            var result = await _tenantRegistrationAppService.RegisterTenant(tenant);
            CurrentUnitOfWork.SetTenantId(result.TenantId);

            var eccpBaseSaicUnit = this._eccpBaseSaicUnitRepository.FirstOrDefault(m => m.TenantId == result.TenantId);
            input.Id = eccpBaseSaicUnit.Id;
            this.ObjectMapper.Map(input, eccpBaseSaicUnit);

            //var eccpBaseSaicUnit = ObjectMapper.Map<EccpBaseSaicUnit>(input);


            //if (AbpSession.TenantId != null)
            //{
            //    eccpBaseSaicUnit.TenantId = (int?)AbpSession.TenantId;
            //}


            //await _eccpBaseSaicUnitRepository.InsertAsync(eccpBaseSaicUnit);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_EccpBaseSaicUnits_Edit)]
        private async Task Update(CreateOrEditEccpBaseSaicUnitDto input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var eccpBaseSaicUnit = await _eccpBaseSaicUnitRepository.FirstOrDefaultAsync((int)input.Id);
                ObjectMapper.Map(input, eccpBaseSaicUnit);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_EccpBaseSaicUnits_Delete)]
        public async Task Delete(EntityDto input)
        {
            using (this.UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                await _eccpBaseSaicUnitRepository.DeleteAsync(input.Id);
                var eccpBaseSaicUnit = await _eccpBaseSaicUnitRepository.FirstOrDefaultAsync(input.Id);
                if (eccpBaseSaicUnit != null && eccpBaseSaicUnit.TenantId != null)
                {
                    var tenant = await this.TenantManager.GetByIdAsync(eccpBaseSaicUnit.TenantId.Value);
                    await this.TenantManager.DeleteAsync(tenant);
                }
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_EccpBaseSaicUnits)]
        public async Task<PagedResultDto<ECCPBaseAreaLookupTableDto>> GetAllECCPBaseAreaForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _eccpBaseAreaRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.Name.ToString().Contains(input.Filter)
               ).Where(e => e.ParentId == input.ParentId);

            var totalCount = await query.CountAsync();

            var eccpBaseAreaList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<ECCPBaseAreaLookupTableDto>();
            foreach (var eccpBaseArea in eccpBaseAreaList)
            {
                lookupTableDtoList.Add(new ECCPBaseAreaLookupTableDto
                {
                    Id = eccpBaseArea.Id,
                    DisplayName = eccpBaseArea.Name?.ToString()
                });
            }

            return new PagedResultDto<ECCPBaseAreaLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
    }
}