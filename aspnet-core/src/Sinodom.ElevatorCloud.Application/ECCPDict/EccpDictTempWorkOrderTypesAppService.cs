// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictTempWorkOrderTypesAppService.cs" company="Sinodom">
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
    ///     The eccp dict temp work order types app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictTempWorkOrderTypes)]
    public class EccpDictTempWorkOrderTypesAppService : ElevatorCloudAppServiceBase,
                                                        IEccpDictTempWorkOrderTypesAppService
    {
        /// <summary>
        ///     The _eccp dict temp work order type repository.
        /// </summary>
        private readonly IRepository<EccpDictTempWorkOrderType> _eccpDictTempWorkOrderTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpDictTempWorkOrderTypesAppService"/> class.
        /// </summary>
        /// <param name="eccpDictTempWorkOrderTypeRepository">
        /// The eccp dict temp work order type repository.
        /// </param>
        public EccpDictTempWorkOrderTypesAppService(
            IRepository<EccpDictTempWorkOrderType> eccpDictTempWorkOrderTypeRepository)
        {
            this._eccpDictTempWorkOrderTypeRepository = eccpDictTempWorkOrderTypeRepository;
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
        public async Task CreateOrEdit(CreateOrEditEccpDictTempWorkOrderTypeDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictTempWorkOrderTypes_Delete)]
        public async Task Delete(EntityDto input)
        {
            await this._eccpDictTempWorkOrderTypeRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetEccpDictTempWorkOrderTypeForView>> GetAll(
            GetAllEccpDictTempWorkOrderTypesInput input)
        {
            var filteredEccpDictTempWorkOrderTypes = this._eccpDictTempWorkOrderTypeRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.Contains(input.Filter));

            var query = from o in filteredEccpDictTempWorkOrderTypes
                        select new 
                                   {
                                       EccpDictTempWorkOrderType = o
                                   };

            var totalCount = await query.CountAsync();

            var eccpDictTempWorkOrderTypes = new List<GetEccpDictTempWorkOrderTypeForView>();

            query.OrderBy(input.Sorting ?? "eccpDictTempWorkOrderType.id asc").PageBy(input).MapTo(eccpDictTempWorkOrderTypes);

            return new PagedResultDto<GetEccpDictTempWorkOrderTypeForView>(totalCount, eccpDictTempWorkOrderTypes);
        }

        /// <summary>
        /// The get eccp dict temp work order type for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictTempWorkOrderTypes_Edit)]
        public async Task<GetEccpDictTempWorkOrderTypeForEditOutput> GetEccpDictTempWorkOrderTypeForEdit(
            EntityDto input)
        {
            var eccpDictTempWorkOrderType =
                await this._eccpDictTempWorkOrderTypeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpDictTempWorkOrderTypeForEditOutput
                             {
                                 EccpDictTempWorkOrderType =
                                     this.ObjectMapper.Map<CreateOrEditEccpDictTempWorkOrderTypeDto>(
                                         eccpDictTempWorkOrderType)
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictTempWorkOrderTypes_Create)]
        private async Task Create(CreateOrEditEccpDictTempWorkOrderTypeDto input)
        {
            var eccpDictTempWorkOrderType = this.ObjectMapper.Map<EccpDictTempWorkOrderType>(input);

            await this._eccpDictTempWorkOrderTypeRepository.InsertAsync(eccpDictTempWorkOrderType);
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictTempWorkOrderTypes_Edit)]
        private async Task Update(CreateOrEditEccpDictTempWorkOrderTypeDto input)
        {
            if (input.Id != null)
            {
                var eccpDictTempWorkOrderType =
                    await this._eccpDictTempWorkOrderTypeRepository.FirstOrDefaultAsync((int)input.Id);
                this.ObjectMapper.Map(input, eccpDictTempWorkOrderType);
            }
        }
    }
}