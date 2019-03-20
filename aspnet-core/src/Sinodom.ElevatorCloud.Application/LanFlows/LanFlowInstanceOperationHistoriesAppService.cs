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
	[AbpAuthorize(AppPermissions.Pages_LanFlowInstanceOperationHistories)]
    public class LanFlowInstanceOperationHistoriesAppService : ElevatorCloudAppServiceBase, ILanFlowInstanceOperationHistoriesAppService
    {
		 private readonly IRepository<LanFlowInstanceOperationHistory> _lanFlowInstanceOperationHistoryRepository;

		  public LanFlowInstanceOperationHistoriesAppService(IRepository<LanFlowInstanceOperationHistory> lanFlowInstanceOperationHistoryRepository) 
		  {
			_lanFlowInstanceOperationHistoryRepository = lanFlowInstanceOperationHistoryRepository;
		
		  }

		 public async Task<PagedResultDto<GetLanFlowInstanceOperationHistoryForView>> GetAll(GetAllLanFlowInstanceOperationHistoriesInput input)
         {

			var filteredLanFlowInstanceOperationHistories = _lanFlowInstanceOperationHistoryRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.StatusName.Contains(input.Filter) || e.ActionDesc.Contains(input.Filter) || e.ActionValue.Contains(input.Filter) || e.ActionCode.Contains(input.Filter))
                        .WhereIf(input.MinStatusValueFilter != null, e => e.StatusValue >= input.MinStatusValueFilter)
                        .WhereIf(input.MaxStatusValueFilter != null, e => e.StatusValue <= input.MaxStatusValueFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StatusNameFilter),  e => e.StatusName.ToLower() == input.StatusNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ActionCodeFilter),  e => e.ActionCode.ToLower() == input.ActionCodeFilter.ToLower().Trim());


            var query = (from o in filteredLanFlowInstanceOperationHistories

                         select new GetLanFlowInstanceOperationHistoryForView()
                         {
                             LanFlowInstanceOperationHistory = ObjectMapper.Map<LanFlowInstanceOperationHistoryDto>(o)

                         });

            var totalCount = await query.CountAsync();

            var lanFlowInstanceOperationHistories = await query
                .OrderBy(input.Sorting ?? "lanFlowInstanceOperationHistory.id asc")
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<GetLanFlowInstanceOperationHistoryForView>(
                totalCount,
                lanFlowInstanceOperationHistories
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_LanFlowInstanceOperationHistories_Edit)]
		 public async Task<GetLanFlowInstanceOperationHistoryForEditOutput> GetLanFlowInstanceOperationHistoryForEdit(EntityDto input)
         {
            var lanFlowInstanceOperationHistory = await _lanFlowInstanceOperationHistoryRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetLanFlowInstanceOperationHistoryForEditOutput {LanFlowInstanceOperationHistory = ObjectMapper.Map<CreateOrEditLanFlowInstanceOperationHistoryDto>(lanFlowInstanceOperationHistory)};
            			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditLanFlowInstanceOperationHistoryDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_LanFlowInstanceOperationHistories_Create)]
		 private async Task Create(CreateOrEditLanFlowInstanceOperationHistoryDto input)
         {
            var lanFlowInstanceOperationHistory = ObjectMapper.Map<LanFlowInstanceOperationHistory>(input);

			

            await _lanFlowInstanceOperationHistoryRepository.InsertAsync(lanFlowInstanceOperationHistory);
         }

		 [AbpAuthorize(AppPermissions.Pages_LanFlowInstanceOperationHistories_Edit)]
		 private async Task Update(CreateOrEditLanFlowInstanceOperationHistoryDto input)
         {
            var lanFlowInstanceOperationHistory = await _lanFlowInstanceOperationHistoryRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, lanFlowInstanceOperationHistory);
         }

		 [AbpAuthorize(AppPermissions.Pages_LanFlowInstanceOperationHistories_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _lanFlowInstanceOperationHistoryRepository.DeleteAsync(input.Id);
         }

    }
}