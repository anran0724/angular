// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictPlaceTypesAppService.cs" company="Sinodom">
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
    ///     The eccp dict place types app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictPlaceTypes)]
    public class EccpDictPlaceTypesAppService : ElevatorCloudAppServiceBase, IEccpDictPlaceTypesAppService
    {
        /// <summary>
        ///     The _eccp dict place type repository.
        /// </summary>
        private readonly IRepository<EccpDictPlaceType> _eccpDictPlaceTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpDictPlaceTypesAppService"/> class.
        /// </summary>
        /// <param name="eccpDictPlaceTypeRepository">
        /// The eccp dict place type repository.
        /// </param>
        public EccpDictPlaceTypesAppService(IRepository<EccpDictPlaceType> eccpDictPlaceTypeRepository)
        {
            this._eccpDictPlaceTypeRepository = eccpDictPlaceTypeRepository;
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
        public async Task CreateOrEdit(CreateOrEditEccpDictPlaceTypeDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictPlaceTypes_Delete)]
        public async Task Delete(EntityDto input)
        {
            await this._eccpDictPlaceTypeRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetEccpDictPlaceTypeForView>> GetAll(GetAllEccpDictPlaceTypesInput input)
        {
            var filteredEccpDictPlaceTypes = this._eccpDictPlaceTypeRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.Contains(input.Filter));

            var query = from o in filteredEccpDictPlaceTypes
                        select new 
                                   {
                                       EccpDictPlaceType = o
                                   };

            var totalCount = await query.CountAsync();

            var eccpDictPlaceTypes = new List<GetEccpDictPlaceTypeForView>();

            query.OrderBy(input.Sorting ?? "eccpDictPlaceType.id asc").PageBy(input).MapTo(eccpDictPlaceTypes);

            return new PagedResultDto<GetEccpDictPlaceTypeForView>(totalCount, eccpDictPlaceTypes);
        }

        /// <summary>
        /// The get eccp dict place type for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictPlaceTypes_Edit)]
        public async Task<GetEccpDictPlaceTypeForEditOutput> GetEccpDictPlaceTypeForEdit(EntityDto input)
        {
            var eccpDictPlaceType = await this._eccpDictPlaceTypeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpDictPlaceTypeForEditOutput
                             {
                                 EccpDictPlaceType =
                                     this.ObjectMapper.Map<CreateOrEditEccpDictPlaceTypeDto>(eccpDictPlaceType)
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictPlaceTypes_Create)]
        private async Task Create(CreateOrEditEccpDictPlaceTypeDto input)
        {
            var eccpDictPlaceType = this.ObjectMapper.Map<EccpDictPlaceType>(input);

            await this._eccpDictPlaceTypeRepository.InsertAsync(eccpDictPlaceType);
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictPlaceTypes_Edit)]
        private async Task Update(CreateOrEditEccpDictPlaceTypeDto input)
        {
            if (input.Id != null)
            {
                var eccpDictPlaceType = await this._eccpDictPlaceTypeRepository.FirstOrDefaultAsync((int)input.Id);
                this.ObjectMapper.Map(input, eccpDictPlaceType);
            }
        }
    }
}