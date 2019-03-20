// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictMaintenanceWorkFlowStatusesAppService.cs" company="Sinodom">
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
    ///     The eccp dict maintenance work flow statuses app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictMaintenanceWorkFlowStatuses)]
    public class EccpDictMaintenanceWorkFlowStatusesAppService : ElevatorCloudAppServiceBase,
                                                                 IEccpDictMaintenanceWorkFlowStatusesAppService
    {
        /// <summary>
        ///     The _eccp dict maintenance work flow status repository.
        /// </summary>
        private readonly IRepository<EccpDictMaintenanceWorkFlowStatus> _eccpDictMaintenanceWorkFlowStatusRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpDictMaintenanceWorkFlowStatusesAppService"/> class.
        /// </summary>
        /// <param name="eccpDictMaintenanceWorkFlowStatusRepository">
        /// The eccp dict maintenance work flow status repository.
        /// </param>
        public EccpDictMaintenanceWorkFlowStatusesAppService(
            IRepository<EccpDictMaintenanceWorkFlowStatus> eccpDictMaintenanceWorkFlowStatusRepository)
        {
            this._eccpDictMaintenanceWorkFlowStatusRepository = eccpDictMaintenanceWorkFlowStatusRepository;
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
        public async Task CreateOrEdit(CreateOrEditEccpDictMaintenanceWorkFlowStatusDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictMaintenanceWorkFlowStatuses_Delete)]
        public async Task Delete(EntityDto input)
        {
            await this._eccpDictMaintenanceWorkFlowStatusRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetEccpDictMaintenanceWorkFlowStatusForView>> GetAll(
            GetAllEccpDictMaintenanceWorkFlowStatusesInput input)
        {
            var filteredEccpDictMaintenanceWorkFlowStatuses = this._eccpDictMaintenanceWorkFlowStatusRepository.GetAll()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.Name.Contains(input.Filter));

            var query = from o in filteredEccpDictMaintenanceWorkFlowStatuses
                        select new 
                                   {
                                       EccpDictMaintenanceWorkFlowStatus = o
                                   };

            var totalCount = await query.CountAsync();

            var eccpDictMaintenanceWorkFlowStatuses = new List<GetEccpDictMaintenanceWorkFlowStatusForView>();

            query.OrderBy(input.Sorting ?? "eccpDictMaintenanceWorkFlowStatus.id asc").PageBy(input).MapTo(eccpDictMaintenanceWorkFlowStatuses);

            return new PagedResultDto<GetEccpDictMaintenanceWorkFlowStatusForView>(
                totalCount,
                eccpDictMaintenanceWorkFlowStatuses);
        }

        /// <summary>
        /// The get eccp dict maintenance work flow status for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictMaintenanceWorkFlowStatuses_Edit)]
        public async Task<GetEccpDictMaintenanceWorkFlowStatusForEditOutput> GetEccpDictMaintenanceWorkFlowStatusForEdit(EntityDto input)
        {
            var eccpDictMaintenanceWorkFlowStatus =
                await this._eccpDictMaintenanceWorkFlowStatusRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpDictMaintenanceWorkFlowStatusForEditOutput
                             {
                                 EccpDictMaintenanceWorkFlowStatus =
                                     this.ObjectMapper.Map<CreateOrEditEccpDictMaintenanceWorkFlowStatusDto>(
                                         eccpDictMaintenanceWorkFlowStatus)
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictMaintenanceWorkFlowStatuses_Create)]
        private async Task Create(CreateOrEditEccpDictMaintenanceWorkFlowStatusDto input)
        {
            var eccpDictMaintenanceWorkFlowStatus = this.ObjectMapper.Map<EccpDictMaintenanceWorkFlowStatus>(input);

            await this._eccpDictMaintenanceWorkFlowStatusRepository.InsertAsync(eccpDictMaintenanceWorkFlowStatus);
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
        [AbpAuthorize(AppPermissions.Pages_EccpDict_EccpDictMaintenanceWorkFlowStatuses_Edit)]
        private async Task Update(CreateOrEditEccpDictMaintenanceWorkFlowStatusDto input)
        {
            if (input.Id != null)
            {
                var eccpDictMaintenanceWorkFlowStatus =
                    await this._eccpDictMaintenanceWorkFlowStatusRepository.FirstOrDefaultAsync((int)input.Id);
                this.ObjectMapper.Map(input, eccpDictMaintenanceWorkFlowStatus);
            }
        }
    }
}