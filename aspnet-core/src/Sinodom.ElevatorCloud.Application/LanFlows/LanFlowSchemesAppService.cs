
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
	[AbpAuthorize(AppPermissions.Pages_Administration_LanFlowSchemes)]
    public class LanFlowSchemesAppService : ElevatorCloudAppServiceBase, ILanFlowSchemesAppService
    {
		 private readonly IRepository<LanFlowScheme> _lanFlowSchemeRepository;
		 

		  public LanFlowSchemesAppService(IRepository<LanFlowScheme> lanFlowSchemeRepository ) 
		  {
			_lanFlowSchemeRepository = lanFlowSchemeRepository;
			
		  }

		 public async Task<PagedResultDto<GetLanFlowSchemeForView>> GetAll(GetAllLanFlowSchemesInput input)
         {

			var filteredLanFlowSchemes = _lanFlowSchemeRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.SchemeName.Contains(input.Filter) || e.SchemeType.Contains(input.Filter) || e.SchemeContent.Contains(input.Filter) || e.TableName.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.SchemeTypeFilter),  e => e.SchemeType.ToLower() == input.SchemeTypeFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.TableNameFilter),  e => e.TableName.ToLower() == input.TableNameFilter.ToLower().Trim())
						.WhereIf(input.MinAuthorizeTypeFilter != null, e => e.AuthorizeType >= input.MinAuthorizeTypeFilter)
						.WhereIf(input.MaxAuthorizeTypeFilter != null, e => e.AuthorizeType <= input.MaxAuthorizeTypeFilter);


			var query = (from o in filteredLanFlowSchemes
                         
                         select new GetLanFlowSchemeForView() { LanFlowScheme = ObjectMapper.Map<LanFlowSchemeDto>(o)
						 
						 })
						 ;

            var totalCount = await query.CountAsync();

            var lanFlowSchemes = await query
                .OrderBy(input.Sorting ?? "lanFlowScheme.id asc")
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<GetLanFlowSchemeForView>(
                totalCount,
                lanFlowSchemes
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Administration_LanFlowSchemes_Edit)]
		 public async Task<GetLanFlowSchemeForEditOutput> GetLanFlowSchemeForEdit(EntityDto input)
         {
            var lanFlowScheme = await _lanFlowSchemeRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetLanFlowSchemeForEditOutput {LanFlowScheme = ObjectMapper.Map<CreateOrEditLanFlowSchemeDto>(lanFlowScheme)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditLanFlowSchemeDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_LanFlowSchemes_Create)]
		 private async Task Create(CreateOrEditLanFlowSchemeDto input)
         {
            var lanFlowScheme = ObjectMapper.Map<LanFlowScheme>(input);

			

            await _lanFlowSchemeRepository.InsertAsync(lanFlowScheme);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_LanFlowSchemes_Edit)]
		 private async Task Update(CreateOrEditLanFlowSchemeDto input)
         {
            var lanFlowScheme = await _lanFlowSchemeRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, lanFlowScheme);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_LanFlowSchemes_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _lanFlowSchemeRepository.DeleteAsync(input.Id);
         }

		 
    }
}