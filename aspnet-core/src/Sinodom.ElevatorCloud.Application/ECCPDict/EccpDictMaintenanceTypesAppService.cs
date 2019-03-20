// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictMaintenanceTypesAppService.cs" company="Sinodom">
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
    ///     The eccp dict maintenance types app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictMaintenanceTypes)]
    public class EccpDictMaintenanceTypesAppService : ElevatorCloudAppServiceBase, IEccpDictMaintenanceTypesAppService
    {
        /// <summary>
        ///     The _eccp dict maintenance type repository.
        /// </summary>
        private readonly IRepository<EccpDictMaintenanceType> _eccpDictMaintenanceTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpDictMaintenanceTypesAppService"/> class.
        /// </summary>
        /// <param name="eccpDictMaintenanceTypeRepository">
        /// The eccp dict maintenance type repository.
        /// </param>
        public EccpDictMaintenanceTypesAppService(
            IRepository<EccpDictMaintenanceType> eccpDictMaintenanceTypeRepository)
        {
            this._eccpDictMaintenanceTypeRepository = eccpDictMaintenanceTypeRepository;
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
        public async Task CreateOrEdit(CreateOrEditEccpDictMaintenanceTypeDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictMaintenanceTypes_Delete)]
        public async Task Delete(EntityDto input)
        {
            await this._eccpDictMaintenanceTypeRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetEccpDictMaintenanceTypeForView>> GetAll(
            GetAllEccpDictMaintenanceTypesInput input)
        {
            var filteredEccpDictMaintenanceTypes = this._eccpDictMaintenanceTypeRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Name.Contains(input.Filter));

            var query = from o in filteredEccpDictMaintenanceTypes select new { EccpDictMaintenanceType = o };

            var totalCount = await query.CountAsync();

            List<GetEccpDictMaintenanceTypeForView> eccpDictMaintenanceTypes = new List<GetEccpDictMaintenanceTypeForView>();

            query.OrderBy(input.Sorting ?? "eccpDictMaintenanceType.id asc").PageBy(input).MapTo(eccpDictMaintenanceTypes);

            return new PagedResultDto<GetEccpDictMaintenanceTypeForView>(totalCount, eccpDictMaintenanceTypes);
        }

        /// <summary>
        /// The get eccp dict maintenance type for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictMaintenanceTypes_Edit)]
        public async Task<GetEccpDictMaintenanceTypeForEditOutput> GetEccpDictMaintenanceTypeForEdit(EntityDto input)
        {
            var eccpDictMaintenanceType = await this._eccpDictMaintenanceTypeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpDictMaintenanceTypeForEditOutput
                             {
                                 EccpDictMaintenanceType =
                                     this.ObjectMapper.Map<CreateOrEditEccpDictMaintenanceTypeDto>(
                                         eccpDictMaintenanceType)
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictMaintenanceTypes_Create)]
        private async Task Create(CreateOrEditEccpDictMaintenanceTypeDto input)
        {
            var eccpDictMaintenanceType = this.ObjectMapper.Map<EccpDictMaintenanceType>(input);

            await this._eccpDictMaintenanceTypeRepository.InsertAsync(eccpDictMaintenanceType);
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictMaintenanceTypes_Edit)]
        private async Task Update(CreateOrEditEccpDictMaintenanceTypeDto input)
        {
            if (input.Id != null)
            {
                var eccpDictMaintenanceType =
                    await this._eccpDictMaintenanceTypeRepository.FirstOrDefaultAsync((int)input.Id);
                this.ObjectMapper.Map(input, eccpDictMaintenanceType);
            }
        }
    }
}