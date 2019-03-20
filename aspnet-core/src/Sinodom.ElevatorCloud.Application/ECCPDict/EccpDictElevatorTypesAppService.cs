// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictElevatorTypesAppService.cs" company="Sinodom">
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
    ///     The eccp dict elevator types app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictElevatorTypes)]
    public class EccpDictElevatorTypesAppService : ElevatorCloudAppServiceBase, IEccpDictElevatorTypesAppService
    {
        /// <summary>
        ///     The _eccp dict elevator type repository.
        /// </summary>
        private readonly IRepository<EccpDictElevatorType> _eccpDictElevatorTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpDictElevatorTypesAppService"/> class.
        /// </summary>
        /// <param name="eccpDictElevatorTypeRepository">
        /// The eccp dict elevator type repository.
        /// </param>
        public EccpDictElevatorTypesAppService(IRepository<EccpDictElevatorType> eccpDictElevatorTypeRepository)
        {
            this._eccpDictElevatorTypeRepository = eccpDictElevatorTypeRepository;
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
        public async Task CreateOrEdit(CreateOrEditEccpDictElevatorTypeDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictElevatorTypes_Delete)]
        public async Task Delete(EntityDto input)
        {
            await this._eccpDictElevatorTypeRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetEccpDictElevatorTypeForView>> GetAll(GetAllEccpDictElevatorTypesInput input)
        {
            var filteredEccpDictElevatorTypes = this._eccpDictElevatorTypeRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.Contains(input.Filter));

            var query = from o in filteredEccpDictElevatorTypes
                        select new 
                                   {
                                       EccpDictElevatorType = o
                                   };

            var totalCount = await query.CountAsync();

            var eccpDictElevatorTypes = new List<GetEccpDictElevatorTypeForView>();
                
            query.OrderBy(input.Sorting ?? "eccpDictElevatorType.id asc").PageBy(input).MapTo(eccpDictElevatorTypes);

            return new PagedResultDto<GetEccpDictElevatorTypeForView>(totalCount, eccpDictElevatorTypes);
        }

        /// <summary>
        /// The get eccp dict elevator type for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictElevatorTypes_Edit)]
        public async Task<GetEccpDictElevatorTypeForEditOutput> GetEccpDictElevatorTypeForEdit(EntityDto input)
        {
            var eccpDictElevatorType = await this._eccpDictElevatorTypeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpDictElevatorTypeForEditOutput
                             {
                                 EccpDictElevatorType =
                                     this.ObjectMapper.Map<CreateOrEditEccpDictElevatorTypeDto>(eccpDictElevatorType)
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictElevatorTypes_Create)]
        private async Task Create(CreateOrEditEccpDictElevatorTypeDto input)
        {
            var eccpDictElevatorType = this.ObjectMapper.Map<EccpDictElevatorType>(input);

            await this._eccpDictElevatorTypeRepository.InsertAsync(eccpDictElevatorType);
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictElevatorTypes_Edit)]
        private async Task Update(CreateOrEditEccpDictElevatorTypeDto input)
        {
            if (input.Id != null)
            {
                var eccpDictElevatorType =
                    await this._eccpDictElevatorTypeRepository.FirstOrDefaultAsync((int)input.Id);
                this.ObjectMapper.Map(input, eccpDictElevatorType);
            }
        }
    }
}