using Sinodom.ElevatorCloud.LanFlows;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Sinodom.ElevatorCloud.LanFlows.Dtos;
using Sinodom.ElevatorCloud.Dto;
using Abp.Application.Services.Dto;
using Sinodom.ElevatorCloud.Authorization;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Sinodom.ElevatorCloud.LanFlows
{
	[AbpAuthorize(AppPermissions.Pages_Administration_LanFlowStatusActions)]
    public class LanFlowStatusActionsAppService : ElevatorCloudAppServiceBase, ILanFlowStatusActionsAppService
    {
		 private readonly IRepository<LanFlowStatusAction> _lanFlowStatusActionRepository;
		 private readonly IRepository<LanFlowScheme,int> _lanFlowSchemeRepository;
		 

		  public LanFlowStatusActionsAppService(IRepository<LanFlowStatusAction> lanFlowStatusActionRepository , IRepository<LanFlowScheme, int> lanFlowSchemeRepository) 
		  {
			_lanFlowStatusActionRepository = lanFlowStatusActionRepository;
			_lanFlowSchemeRepository = lanFlowSchemeRepository;
		
		  }

		 public async Task<PagedResultDto<GetLanFlowStatusActionForView>> GetAll(GetAllLanFlowStatusActionsInput input)
         {

			var filteredLanFlowStatusActions = _lanFlowStatusActionRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.StatusName.Contains(input.Filter) || e.ActionName.Contains(input.Filter) || e.ActionDesc.Contains(input.Filter) || e.ActionCode.Contains(input.Filter) || e.UserRoleCode.Contains(input.Filter) || e.ApiAction.Contains(input.Filter))
                        .WhereIf(input.MinStatusValueFilter != null, e => e.StatusValue >= input.MinStatusValueFilter)
                        .WhereIf(input.MaxStatusValueFilter != null, e => e.StatusValue <= input.MaxStatusValueFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StatusNameFilter),  e => e.StatusName.ToLower() == input.StatusNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ActionNameFilter),  e => e.ActionName.ToLower() == input.ActionNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ActionCodeFilter),  e => e.ActionCode.ToLower() == input.ActionCodeFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.UserRoleCodeFilter),  e => e.UserRoleCode.ToLower() == input.UserRoleCodeFilter.ToLower().Trim())
                        .WhereIf(input.MinArgumentValueFilter != null, e => e.ArgumentValue >= input.MinArgumentValueFilter)
                        .WhereIf(input.MaxArgumentValueFilter != null, e => e.ArgumentValue <= input.MaxArgumentValueFilter)
                        .WhereIf(input.IsStartProcessFilter > -1,  e => Convert.ToInt32(e.IsStartProcess) == input.IsStartProcessFilter )
						.WhereIf(input.IsEndProcessFilter > -1,  e => Convert.ToInt32(e.IsEndProcess) == input.IsEndProcessFilter )
						.WhereIf(input.IsAdoptFilter > -1,  e => Convert.ToInt32(e.IsAdopt) == input.IsAdoptFilter );


			var query = (from o in filteredLanFlowStatusActions
                         join o1 in _lanFlowSchemeRepository.GetAll() on o.SchemeId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetLanFlowStatusActionForView() { LanFlowStatusAction = ObjectMapper.Map<LanFlowStatusActionDto>(o)
						 , LanFlowSchemeSchemeName = s1 == null ? "" : s1.SchemeName.ToString()
					
						 })
						 
						.WhereIf(!string.IsNullOrWhiteSpace(input.LanFlowSchemeSchemeNameFilter), e => e.LanFlowSchemeSchemeName.ToLower() == input.LanFlowSchemeSchemeNameFilter.ToLower().Trim());

            var totalCount = await query.CountAsync();

            var lanFlowStatusActions = await query
                .OrderBy(input.Sorting ?? "lanFlowStatusAction.id asc")
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<GetLanFlowStatusActionForView>(
                totalCount,
                lanFlowStatusActions
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Administration_LanFlowStatusActions_Edit)]
		 public async Task<GetLanFlowStatusActionForEditOutput> GetLanFlowStatusActionForEdit(EntityDto input)
         {
            var lanFlowStatusAction = await _lanFlowStatusActionRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetLanFlowStatusActionForEditOutput {LanFlowStatusAction = ObjectMapper.Map<CreateOrEditLanFlowStatusActionDto>(lanFlowStatusAction)};

		    if (output.LanFlowStatusAction.SchemeId != null)
            {
                var lanFlowScheme = await _lanFlowSchemeRepository.FirstOrDefaultAsync((int)output.LanFlowStatusAction.SchemeId);
                output.LanFlowSchemeSchemeName = lanFlowScheme.SchemeName.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditLanFlowStatusActionDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_LanFlowStatusActions_Create)]
		 private async Task Create(CreateOrEditLanFlowStatusActionDto input)
         {
            var lanFlowStatusAction = ObjectMapper.Map<LanFlowStatusAction>(input);

			

            await _lanFlowStatusActionRepository.InsertAsync(lanFlowStatusAction);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_LanFlowStatusActions_Edit)]
		 private async Task Update(CreateOrEditLanFlowStatusActionDto input)
         {
            var lanFlowStatusAction = await _lanFlowStatusActionRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, lanFlowStatusAction);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_LanFlowStatusActions_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _lanFlowStatusActionRepository.DeleteAsync(input.Id);
         }

		 		 [AbpAuthorize(AppPermissions.Pages_Administration_LanFlowStatusActions)]
         public async Task<PagedResultDto<LanFlowSchemeLookupTableDto>> GetAllLanFlowSchemeForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lanFlowSchemeRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.SchemeName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var lanFlowSchemeList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<LanFlowSchemeLookupTableDto>();
			foreach(var lanFlowScheme in lanFlowSchemeList){
				lookupTableDtoList.Add(new LanFlowSchemeLookupTableDto
				{
					Id = lanFlowScheme.Id,
					DisplayName = lanFlowScheme.SchemeName.ToString()
				});
			}

            return new PagedResultDto<LanFlowSchemeLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

         


    }
}