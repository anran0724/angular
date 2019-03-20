using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Sinodom.ElevatorCloud.Authorization;
using Sinodom.ElevatorCloud.Editions.Dto;

namespace Sinodom.ElevatorCloud.Editions
{
    using System;

    using Abp.MultiTenancy;

    using Sinodom.ElevatorCloud.Authorization.Permissions;
    using Sinodom.ElevatorCloud.Editions.Dtos;

    [AbpAuthorize(AppPermissions.Pages_Editions)]
    public class EditionAppService : ElevatorCloudAppServiceBase, IEditionAppService
    {
        private readonly EditionManager _editionManager;
        private readonly IRepository<SubscribableEdition> _subscribableEditionRepository;
        private readonly IRepository<ECCPEditionPermission> _eccpEditionPermissionRepository;
        private readonly IRepository<ECCPEditionsType> _eCCPEditionsTypeRepository;

        public EditionAppService(
            EditionManager editionManager,
            IRepository<SubscribableEdition> subscribableEditionRepository,
            IRepository<ECCPEditionPermission> eccpEditionPermissionRepository,
            IRepository<ECCPEditionsType> eCCPEditionsTypeRepository)
        {
            _editionManager = editionManager;
            _subscribableEditionRepository = subscribableEditionRepository;
            _eccpEditionPermissionRepository = eccpEditionPermissionRepository;
            _eCCPEditionsTypeRepository = eCCPEditionsTypeRepository;
        }

        public async Task<ListResultDto<EditionListDto>> GetEditions()
        {
            var editions = (await this._editionManager.Editions.Cast<SubscribableEdition>().ToListAsync()).OrderBy(e => e.MonthlyPrice);

            var dictEditionTypes = this._eCCPEditionsTypeRepository.GetAll().ToDictionary(e => e.Id);

            var resultDto = ObjectMapper.Map<List<EditionListDto>>(editions);

            foreach (EditionListDto listDto in resultDto)
            {
                if (listDto.ECCPEditionsTypeId.HasValue && dictEditionTypes.ContainsKey(listDto.ECCPEditionsTypeId.Value))
                {
                    listDto.ECCPEditionsTypeName = dictEditionTypes[listDto.ECCPEditionsTypeId.Value].Name;
                }
            }
            
            return new ListResultDto<EditionListDto>(resultDto);
        }

        [AbpAuthorize(AppPermissions.Pages_Editions_Create, AppPermissions.Pages_Editions_Edit)]
        public async Task<GetEditionEditOutput> GetEditionForEdit(NullableIdDto input)
        {
            var features = FeatureManager.GetAll().Where(f=>f.Scope.HasFlag(FeatureScopes.Edition));
            var permissions = PermissionManager.GetAllPermissions(MultiTenancySides.Tenant);
            var grantedPermissions = new ECCPEditionPermission[0];

            EditionEditDto editionEditDto;
            List<NameValue> featureValues;

            if (input.Id.HasValue) //Editing existing edition?
            {
                var edition = (SubscribableEdition)await _editionManager.FindByIdAsync(input.Id.Value);
                featureValues = (await _editionManager.GetFeatureValuesAsync(input.Id.Value)).ToList();
                editionEditDto = ObjectMapper.Map<EditionEditDto>(edition);
                if (edition.ECCPEditionsTypeId.HasValue)
                {
                    var editionsType = await this._eCCPEditionsTypeRepository.FirstOrDefaultAsync(edition.ECCPEditionsTypeId.Value);

                    editionEditDto.ECCPEditionsTypeId = edition.ECCPEditionsTypeId;
                    editionEditDto.EditionTypeName = editionsType.Name;
                }

                grantedPermissions = this._eccpEditionPermissionRepository.GetAll().Where(e => e.EditionId == input.Id.Value && e.IsGranted).ToArray();
            }
            else
            {
                editionEditDto = new EditionEditDto();
                featureValues = features.Select(f => new NameValue(f.Name, f.DefaultValue)).ToList();
            }

            var featureDtos = ObjectMapper.Map<List<FlatFeatureDto>>(features).OrderBy(f => f.DisplayName).ToList();

            return new GetEditionEditOutput
            {
                Edition = editionEditDto,
                Features = featureDtos,
                FeatureValues = featureValues.Select(fv => new NameValueDto(fv)).ToList(),
                Permissions = ObjectMapper.Map<List<FlatEditionPermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList(),
                GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            };
        }

        [AbpAuthorize(AppPermissions.Pages_Editions_Create, AppPermissions.Pages_Editions_Edit)]
        public async Task CreateOrUpdateEdition(CreateOrUpdateEditionDto input)
        {
            if (!input.Edition.Id.HasValue)
            {
                await CreateEditionAsync(input);
            }
            else
            {
                await UpdateEditionAsync(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Editions_Delete)]
        public async Task DeleteEdition(EntityDto input)
        {
            var edition = await _editionManager.GetByIdAsync(input.Id);
            await _editionManager.DeleteAsync(edition);
        }

        public async Task<List<SubscribableEditionComboboxItemDto>> GetEditionComboboxItems(int? selectedEditionId = null, bool addAllItem = false, bool onlyFreeItems = false)
        {
            var editions = await _editionManager.Editions.ToListAsync();
            var subscribableEditions = editions.Cast<SubscribableEdition>()
                .WhereIf(onlyFreeItems, e => e.IsFree)
                .OrderBy(e => e.MonthlyPrice);

            var editionItems =
                new ListResultDto<SubscribableEditionComboboxItemDto>(subscribableEditions
                    .Select(e => new SubscribableEditionComboboxItemDto(e.Id.ToString(), e.DisplayName, e.IsFree)).ToList()).Items.ToList();

            var defaultItem = new SubscribableEditionComboboxItemDto("", L("NotAssigned"), null);
            editionItems.Insert(0, defaultItem);

            if (addAllItem)
            {
                editionItems.Insert(0, new SubscribableEditionComboboxItemDto("-1", "- " + L("All") + " -", null));
            }

            if (selectedEditionId.HasValue)
            {
                var selectedEdition = editionItems.FirstOrDefault(e => e.Value == selectedEditionId.Value.ToString());
                if (selectedEdition != null)
                {
                    selectedEdition.IsSelected = true;
                }
            }
            else
            {
                editionItems[0].IsSelected = true;
            }

            return editionItems;
        }

        [AbpAuthorize(AppPermissions.Pages_Editions_Create)]
        protected virtual async Task CreateEditionAsync(CreateOrUpdateEditionDto input)
        {
            var edition = ObjectMapper.Map<SubscribableEdition>(input.Edition);
            if (!edition.ECCPEditionsTypeId.HasValue)
            {
                edition.ECCPEditionsTypeId = input.Edition.ECCPEditionsTypeId;
            }

            if (edition.ExpiringEditionId.HasValue)
            {
                var expiringEdition = (SubscribableEdition)await _editionManager.GetByIdAsync(edition.ExpiringEditionId.Value);
                if (!expiringEdition.IsFree)
                {
                    throw new UserFriendlyException(L("ExpiringEditionMustBeAFreeEdition"));
                }
            }

            await _editionManager.CreateAsync(edition);
            await CurrentUnitOfWork.SaveChangesAsync(); //It's done to get Id of the edition.

            await SetFeatureValues(edition, input.FeatureValues);
            await this.UpdateGrantedPermissionsAsync(edition, input.GrantedPermissionNames);
        }

        [AbpAuthorize(AppPermissions.Pages_Editions_Edit)]
        protected virtual async Task UpdateEditionAsync(CreateOrUpdateEditionDto input)
        {
            if (input.Edition.Id != null)
            {
                var edition = (SubscribableEdition)await _editionManager.GetByIdAsync(input.Edition.Id.Value);

                var existingSubscribableEdition = (SubscribableEdition)edition;
                var updatingSubscribableEdition = ObjectMapper.Map<SubscribableEdition>(input.Edition);
                if (existingSubscribableEdition.IsFree &&
                    !updatingSubscribableEdition.IsFree &&
                    await _subscribableEditionRepository.CountAsync(e => e.ExpiringEditionId == existingSubscribableEdition.Id) > 0)
                {
                    throw new UserFriendlyException(L("ThisEditionIsUsedAsAnExpiringEdition"));
                }

                ObjectMapper.Map(input.Edition, edition);

                await SetFeatureValues(edition, input.FeatureValues);

                await this.UpdateGrantedPermissionsAsync(edition, input.GrantedPermissionNames);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Editions)]
        public async Task<PagedResultDto<ECCPEditionsTypeLookupTableDto>> GetAllECCPEditionsTypeForLookupTable(GetAllForLookupTableInput input)
        {
            var query = this._eCCPEditionsTypeRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.ToString().Contains(input.Filter)
            );

            var totalCount = await query.CountAsync();

            var eccpEditionsTypeList = await query
                                         .PageBy(input)
                                         .ToListAsync();

            var lookupTableDtoList = new List<ECCPEditionsTypeLookupTableDto>();
            foreach (var eccpEditionsType in eccpEditionsTypeList)
            {
                lookupTableDtoList.Add(new ECCPEditionsTypeLookupTableDto
                                           {
                                               Id = eccpEditionsType.Id,
                                               DisplayName = eccpEditionsType.Name.ToString()
                                           });
            }

            return new PagedResultDto<ECCPEditionsTypeLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        private Task SetFeatureValues(Edition edition, List<NameValueDto> featureValues)
        {
            return _editionManager.SetFeatureValuesAsync(edition.Id,
                featureValues.Select(fv => new NameValue(fv.Name, fv.Value)).ToArray());
        }

        private async Task UpdateGrantedPermissionsAsync(Edition edition, List<string> grantedPermissionNames)
        {
            var grantedPermissions = PermissionManager.GetPermissionsFromNamesByValidating(grantedPermissionNames);
            await SetGrantedPermissionsAsync(edition, grantedPermissions);
        }

        private async Task SetGrantedPermissionsAsync(Edition edition, IEnumerable<Permission> permissions)
        {
            var oldPermissions = await this.GetGrantedPermissionsAsync(edition);
            var newPermissions = permissions.ToArray();

            foreach (var permission in oldPermissions.Where(p => !newPermissions.Contains(p)))
            {
                await ProhibitEditionPermissionAsync(edition, permission);
            }

            foreach (var permission in newPermissions.Where(p => !oldPermissions.Contains(p)))
            {
                await GrantEditionPermissionAsync(edition, permission);
            }
        }

        private async Task<IReadOnlyList<Permission>> GetGrantedPermissionsAsync(Edition edition)
        {
            var permissionList = new List<Permission>();

            List<ECCPEditionPermission> permissions = await this._eccpEditionPermissionRepository.GetAllListAsync(e => e.EditionId == edition.Id);

            foreach (var permission in PermissionManager.GetAllPermissions(false))
            {
                if (this.IsGrantedAsync(permission, permissions))
                {
                    permissionList.Add(permission);
                }
            }

            return permissionList;
        }

        private bool IsGrantedAsync(Permission permission, List<ECCPEditionPermission> permissions)
        {
            return permissions.Any(e => e.Name == permission.Name && e.IsGranted);
        }

        private async Task<bool> IsGrantedAsync(int editionId, Permission permission)
        {
            return await this._eccpEditionPermissionRepository.CountAsync(e => e.IsGranted && e.EditionId == editionId && e.Name == permission.Name) > 0;
        }

        /// <summary>
        /// Grants a permission for a edition.
        /// </summary>
        /// <param name="edition">Edition</param>
        /// <param name="permission">Permission</param>
        private async Task GrantEditionPermissionAsync(Edition edition, Permission permission)
        {
            if (await this.IsGrantedAsync(edition.Id, permission))
            {
                return;
            }

            var entity = this._eccpEditionPermissionRepository.GetAll().FirstOrDefault(e => e.EditionId == edition.Id && e.Name == permission.Name);
            if (entity == null)
            {
                await this._eccpEditionPermissionRepository.InsertAsync(
                    new ECCPEditionPermission { EditionId = edition.Id, IsGranted = true, Name = permission.Name });
            }
            else if (!entity.IsGranted)
            {
                entity.IsGranted = true;
                await this._eccpEditionPermissionRepository.UpdateAsync(entity);
            }
        }

        /// <summary>
        /// Prohibits a permission for a edition.
        /// </summary>
        /// <param name="edition">Edition</param>
        /// <param name="permission">Permission</param>
        private async Task ProhibitEditionPermissionAsync(Edition edition, Permission permission)
        {
            if (!await this.IsGrantedAsync(edition.Id, permission))
            {
                return;
            }

            var entity = this._eccpEditionPermissionRepository.GetAll().FirstOrDefault(e => e.IsGranted && e.EditionId == edition.Id && e.Name == permission.Name);
            if (entity != null)
            {
                entity.IsGranted = false;
                await this._eccpEditionPermissionRepository.UpdateAsync(entity);
            }
        }
    }
}
