// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictWorkOrderTypesAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpDict
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
    using Sinodom.ElevatorCloud.EccpDict.Dtos;

    /// <summary>
    ///     The eccp dict work order types app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictWorkOrderTypes)]
    public class EccpDictWorkOrderTypesAppService : ElevatorCloudAppServiceBase, IEccpDictWorkOrderTypesAppService
    {
        /// <summary>
        ///     The _eccp dict work order type repository.
        /// </summary>
        private readonly IRepository<EccpDictWorkOrderType> _eccpDictWorkOrderTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpDictWorkOrderTypesAppService"/> class.
        /// </summary>
        /// <param name="eccpDictWorkOrderTypeRepository">
        /// The eccp dict work order type repository.
        /// </param>
        public EccpDictWorkOrderTypesAppService(IRepository<EccpDictWorkOrderType> eccpDictWorkOrderTypeRepository)
        {
            this._eccpDictWorkOrderTypeRepository = eccpDictWorkOrderTypeRepository;
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
        public async Task CreateOrEdit(CreateOrEditEccpDictWorkOrderTypeDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictWorkOrderTypes_Delete)]
        public async Task Delete(EntityDto input)
        {
            await this._eccpDictWorkOrderTypeRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetEccpDictWorkOrderTypeForView>> GetAll(
            GetAllEccpDictWorkOrderTypesInput input)
        {
            var filteredEccpDictWorkOrderTypes = this._eccpDictWorkOrderTypeRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.Contains(input.Filter));

            var query = from o in filteredEccpDictWorkOrderTypes
                        select new 
                                   {
                                       EccpDictWorkOrderType = o
                                   };

            var totalCount = await query.CountAsync();

            var eccpDictWorkOrderTypes = new List<GetEccpDictWorkOrderTypeForView>();
                
            query.OrderBy(input.Sorting ?? "eccpDictWorkOrderType.id asc").PageBy(input).MapTo(eccpDictWorkOrderTypes);

            return new PagedResultDto<GetEccpDictWorkOrderTypeForView>(totalCount, eccpDictWorkOrderTypes);
        }

        /// <summary>
        /// The get eccp dict work order type for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictWorkOrderTypes_Edit)]
        public async Task<GetEccpDictWorkOrderTypeForEditOutput> GetEccpDictWorkOrderTypeForEdit(EntityDto input)
        {
            var eccpDictWorkOrderType = await this._eccpDictWorkOrderTypeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpDictWorkOrderTypeForEditOutput
                             {
                                 EccpDictWorkOrderType =
                                     this.ObjectMapper.Map<CreateOrEditEccpDictWorkOrderTypeDto>(eccpDictWorkOrderType)
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictWorkOrderTypes_Create)]
        private async Task Create(CreateOrEditEccpDictWorkOrderTypeDto input)
        {
            var eccpDictWorkOrderType = this.ObjectMapper.Map<EccpDictWorkOrderType>(input);

            await this._eccpDictWorkOrderTypeRepository.InsertAsync(eccpDictWorkOrderType);
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictWorkOrderTypes_Edit)]
        private async Task Update(CreateOrEditEccpDictWorkOrderTypeDto input)
        {
            if (input.Id != null)
            {
                var eccpDictWorkOrderType =
                    await this._eccpDictWorkOrderTypeRepository.FirstOrDefaultAsync((int)input.Id);
                this.ObjectMapper.Map(input, eccpDictWorkOrderType);
            }
        }
    }
}