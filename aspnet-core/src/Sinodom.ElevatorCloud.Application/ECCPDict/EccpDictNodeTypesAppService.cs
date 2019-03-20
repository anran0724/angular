// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictNodeTypesAppService.cs" company="Sinodom">
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
    ///     The eccp dict node types app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictNodeTypes)]
    public class EccpDictNodeTypesAppService : ElevatorCloudAppServiceBase, IEccpDictNodeTypesAppService
    {
        /// <summary>
        ///     The _eccp dict node type repository.
        /// </summary>
        private readonly IRepository<EccpDictNodeType> _eccpDictNodeTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpDictNodeTypesAppService"/> class.
        /// </summary>
        /// <param name="eccpDictNodeTypeRepository">
        /// The eccp dict node type repository.
        /// </param>
        public EccpDictNodeTypesAppService(IRepository<EccpDictNodeType> eccpDictNodeTypeRepository)
        {
            this._eccpDictNodeTypeRepository = eccpDictNodeTypeRepository;
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
        public async Task CreateOrEdit(CreateOrEditEccpDictNodeTypeDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictNodeTypes_Delete)]
        public async Task Delete(EntityDto input)
        {
            await this._eccpDictNodeTypeRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetEccpDictNodeTypeForView>> GetAll(GetAllEccpDictNodeTypesInput input)
        {
            var filteredEccpDictNodeTypes = this._eccpDictNodeTypeRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.Contains(input.Filter));

            var query = from o in filteredEccpDictNodeTypes
                        select new 
                                   {
                                       EccpDictNodeType = o
                                   };

            var totalCount = await query.CountAsync();

            var eccpDictNodeTypes = new List<GetEccpDictNodeTypeForView>();

            query.OrderBy(input.Sorting ?? "eccpDictNodeType.id asc").PageBy(input).MapTo(eccpDictNodeTypes);

            return new PagedResultDto<GetEccpDictNodeTypeForView>(totalCount, eccpDictNodeTypes);
        }

        /// <summary>
        /// The get eccp dict node type for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictNodeTypes_Edit)]
        public async Task<GetEccpDictNodeTypeForEditOutput> GetEccpDictNodeTypeForEdit(EntityDto input)
        {
            var eccpDictNodeType = await this._eccpDictNodeTypeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpDictNodeTypeForEditOutput
                             {
                                 EccpDictNodeType =
                                     this.ObjectMapper.Map<CreateOrEditEccpDictNodeTypeDto>(eccpDictNodeType)
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictNodeTypes_Create)]
        private async Task Create(CreateOrEditEccpDictNodeTypeDto input)
        {
            var eccpDictNodeType = this.ObjectMapper.Map<EccpDictNodeType>(input);

            await this._eccpDictNodeTypeRepository.InsertAsync(eccpDictNodeType);
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictNodeTypes_Edit)]
        private async Task Update(CreateOrEditEccpDictNodeTypeDto input)
        {
            if (input.Id != null)
            {
                var eccpDictNodeType = await this._eccpDictNodeTypeRepository.FirstOrDefaultAsync((int)input.Id);
                this.ObjectMapper.Map(input, eccpDictNodeType);
            }
        }
    }
}