using Sinodom.ElevatorCloud.Authorization.Users;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions.Dtos;
using Sinodom.ElevatorCloud.Dto;
using Abp.Application.Services.Dto;
using Sinodom.ElevatorCloud.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Sinodom.ElevatorCloud.MultiTenancy.EccpUserExtensions
{
	[AbpAuthorize(AppPermissions.Pages_UserPathHistories)]
    public class UserPathHistoriesAppService : ElevatorCloudAppServiceBase, IUserPathHistoriesAppService
    {
		 private readonly IRepository<UserPathHistory, long> _userPathHistoryRepository;
		 private readonly IRepository<User,long> _userRepository;
		 

		  public UserPathHistoriesAppService(IRepository<UserPathHistory, long> userPathHistoryRepository , IRepository<User, long> userRepository) 
		  {
			_userPathHistoryRepository = userPathHistoryRepository;
			_userRepository = userRepository;
		
		  }

		 public async Task<PagedResultDto<GetUserPathHistoryForView>> GetAll(GetAllUserPathHistoriesInput input)
         {
			
			var filteredUserPathHistories = _userPathHistoryRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PhoneId.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.PhoneIdFilter),  e => e.PhoneId.ToLower() == input.PhoneIdFilter.ToLower().Trim())
						.WhereIf(input.MinLongitudeFilter != null, e => e.Longitude >= input.MinLongitudeFilter)
						.WhereIf(input.MaxLongitudeFilter != null, e => e.Longitude <= input.MaxLongitudeFilter)
						.WhereIf(input.MinLatitudeFilter != null, e => e.Latitude >= input.MinLatitudeFilter)
						.WhereIf(input.MaxLatitudeFilter != null, e => e.Latitude <= input.MaxLatitudeFilter);


			var query = (from o in filteredUserPathHistories
                         join o1 in _userRepository.GetAll() on o.UserId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetUserPathHistoryForView() {
							UserPathHistory = ObjectMapper.Map<UserPathHistoryDto>(o),
                         	UserName = s1 == null ? "" : s1.Name.ToString()
						})
						.WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.UserName.ToLower() == input.UserNameFilter.ToLower().Trim());

            var totalCount = await query.CountAsync();

            var userPathHistories = await query
                .OrderBy(input.Sorting ?? "userPathHistory.id asc")
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<GetUserPathHistoryForView>(
                totalCount,
                userPathHistories
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_UserPathHistories_Edit)]
		 public async Task<GetUserPathHistoryForEditOutput> GetUserPathHistoryForEdit(EntityDto<long> input)
         {
            var userPathHistory = await _userPathHistoryRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetUserPathHistoryForEditOutput {UserPathHistory = ObjectMapper.Map<CreateOrEditUserPathHistoryDto>(userPathHistory)};

		    if (output.UserPathHistory.UserId != null)
            {
                var user = await _userRepository.FirstOrDefaultAsync((long)output.UserPathHistory.UserId);
                output.UserName = user.Name.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditUserPathHistoryDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_UserPathHistories_Create)]
		 private async Task Create(CreateOrEditUserPathHistoryDto input)
         {
            var userPathHistory = ObjectMapper.Map<UserPathHistory>(input);

			
			if (AbpSession.TenantId != null)
			{
				userPathHistory.TenantId = (int) AbpSession.TenantId;
			}
		

            await _userPathHistoryRepository.InsertAsync(userPathHistory);
         }

		 [AbpAuthorize(AppPermissions.Pages_UserPathHistories_Edit)]
		 private async Task Update(CreateOrEditUserPathHistoryDto input)
         {
            var userPathHistory = await _userPathHistoryRepository.FirstOrDefaultAsync((long)input.Id);
             ObjectMapper.Map(input, userPathHistory);
         }

		 [AbpAuthorize(AppPermissions.Pages_UserPathHistories_Delete)]
         public async Task Delete(EntityDto<long> input)
         {
            await _userPathHistoryRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_UserPathHistories)]
         public async Task<PagedResultDto<UserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _userRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Name.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var userList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<UserLookupTableDto>();
			foreach(var user in userList){
				lookupTableDtoList.Add(new UserLookupTableDto
				{
					Id = user.Id,
					DisplayName = user.Name?.ToString()
				});
			}

            return new PagedResultDto<UserLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}