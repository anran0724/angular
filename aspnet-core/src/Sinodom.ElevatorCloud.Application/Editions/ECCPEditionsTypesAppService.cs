// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ECCPEditionsTypesAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Editions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    using Abp.Application.Services.Dto;
    using Abp.Authorization;
    using Abp.AutoMapper;
    using Abp.Domain.Repositories;
    using Abp.Linq.Extensions;

    using Microsoft.EntityFrameworkCore;

    using Sinodom.ElevatorCloud.Authorization;
    using Sinodom.ElevatorCloud.Editions.Dtos;

    /// <summary>
    /// The eccp editions types app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpEditionsTypes)]
    public class ECCPEditionsTypesAppService : ElevatorCloudAppServiceBase, IECCPEditionsTypesAppService
    {
        /// <summary>
        /// The _e ccp editions type repository.
        /// </summary>
        private readonly IRepository<ECCPEditionsType> _eccpEditionsTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECCPEditionsTypesAppService"/> class.
        /// </summary>
        /// <param name="eccpEditionsTypeRepository">
        /// The e ccp editions type repository.
        /// </param>
        public ECCPEditionsTypesAppService(IRepository<ECCPEditionsType> eccpEditionsTypeRepository)
        {
            this._eccpEditionsTypeRepository = eccpEditionsTypeRepository;
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
        public async Task CreateOrEdit(CreateOrEditECCPEditionsTypeDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpEditionsTypes_Delete)]
        public async Task Delete(EntityDto input)
        {
            await this._eccpEditionsTypeRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetECCPEditionsTypeForView>> GetAll(GetAllECCPEditionsTypesInput input)
        {
            var filteredEccpEditionsTypes = this._eccpEditionsTypeRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.Contains(input.Filter));

            var query = from o in filteredEccpEditionsTypes
                        select new 
                                   {
                                       ECCPEditionsType = o
                                   };

            var totalCount = await query.CountAsync();

            var eccpEditionsTypes = new List<GetECCPEditionsTypeForView>();

            query.OrderBy(input.Sorting ?? "eCCPEditionsType.id asc").PageBy(input).MapTo(eccpEditionsTypes);

            return new PagedResultDto<GetECCPEditionsTypeForView>(totalCount, eccpEditionsTypes);
        }

        /// <summary>
        /// The get eccp editions type for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpEditionsTypes_Edit)]
        public async Task<GetECCPEditionsTypeForEditOutput> GetECCPEditionsTypeForEdit(EntityDto input)
        {
            var eccpEditionsType = await this._eccpEditionsTypeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetECCPEditionsTypeForEditOutput
                             {
                                 ECCPEditionsType =
                                     this.ObjectMapper.Map<CreateOrEditECCPEditionsTypeDto>(eccpEditionsType)
                             };

            return output;
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpEditionsTypes_Create)]
        private async Task Create(CreateOrEditECCPEditionsTypeDto input)
        {
            var eccpEditionsType = this.ObjectMapper.Map<ECCPEditionsType>(input);

            await this._eccpEditionsTypeRepository.InsertAsync(eccpEditionsType);
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpEditionsTypes_Edit)]
        private async Task Update(CreateOrEditECCPEditionsTypeDto input)
        {
            var eccpEditionsType = await this._eccpEditionsTypeRepository.FirstOrDefaultAsync(input.Id.GetValueOrDefault(0));
            this.ObjectMapper.Map(input, eccpEditionsType);
        }
    }
}