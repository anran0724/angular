// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceWorkOrderEvaluationsAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders
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
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos;

    /// <summary>
    /// The eccp maintenance work order evaluations app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrderEvaluations)]
    public class EccpMaintenanceWorkOrderEvaluationsAppService : ElevatorCloudAppServiceBase,
                                                                 IEccpMaintenanceWorkOrderEvaluationsAppService
    {
        /// <summary>
        /// The _eccp maintenance work order evaluation repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkOrderEvaluation> _eccpMaintenanceWorkOrderEvaluationRepository;

        /// <summary>
        /// The _eccp maintenance work order repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkOrder, int> _eccpMaintenanceWorkOrderRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceWorkOrderEvaluationsAppService"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceWorkOrderEvaluationRepository">
        /// The eccp maintenance work order evaluation repository.
        /// </param>
        /// <param name="eccpMaintenanceWorkOrderRepository">
        /// The eccp maintenance work order repository.
        /// </param>
        public EccpMaintenanceWorkOrderEvaluationsAppService(
            IRepository<EccpMaintenanceWorkOrderEvaluation> eccpMaintenanceWorkOrderEvaluationRepository,
            IRepository<EccpMaintenanceWorkOrder, int> eccpMaintenanceWorkOrderRepository)
        {
            this._eccpMaintenanceWorkOrderEvaluationRepository = eccpMaintenanceWorkOrderEvaluationRepository;
            this._eccpMaintenanceWorkOrderRepository = eccpMaintenanceWorkOrderRepository;
        }

        /// <summary>
        /// The commit.
        /// 微信公众号/小程序用的维保评价接口
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The<see cref="Task"/>.
        /// </returns>
        [AbpAllowAnonymous]
        public async Task Commit(CreateOrEditEccpMaintenanceWorkOrderEvaluationDto input)
        {
            if (input.Id == null)
            {
                var eccpMaintenanceWorkOrderEvaluation =
                    this.ObjectMapper.Map<EccpMaintenanceWorkOrderEvaluation>(input);
                await this._eccpMaintenanceWorkOrderEvaluationRepository.InsertAsync(
                    eccpMaintenanceWorkOrderEvaluation);
            }
            else
            {
                var eccpMaintenanceWorkOrderEvaluation =
                    await this._eccpMaintenanceWorkOrderEvaluationRepository.FirstOrDefaultAsync((int)input.Id);
                this.ObjectMapper.Map(input, eccpMaintenanceWorkOrderEvaluation);
            }
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
        public async Task CreateOrEdit(CreateOrEditEccpMaintenanceWorkOrderEvaluationDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrderEvaluations_Delete)]
        public async Task Delete(EntityDto input)
        {
            await this._eccpMaintenanceWorkOrderEvaluationRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetEccpMaintenanceWorkOrderEvaluationForView>> GetAll(
            GetAllEccpMaintenanceWorkOrderEvaluationsInput input)
        {
            var filteredEccpMaintenanceWorkOrderEvaluations = this._eccpMaintenanceWorkOrderEvaluationRepository
                .GetAll()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.Remarks.Contains(input.Filter))
                .WhereIf(input.MinRankFilter != null, e => e.Rank >= input.MinRankFilter).WhereIf(
                    input.MaxRankFilter != null,
                    e => e.Rank <= input.MaxRankFilter);

            var query = (from o in filteredEccpMaintenanceWorkOrderEvaluations
                         join o1 in this._eccpMaintenanceWorkOrderRepository.GetAll() on o.WorkOrderId equals o1.Id into
                             j1
                         from s1 in j1.DefaultIfEmpty()
                         select new 
                                    {
                                        EccpMaintenanceWorkOrderEvaluation = o,
                                        EccpMaintenanceWorkOrderRemark = s1 == null ? string.Empty : s1.Remark
                                    }).WhereIf(
                !string.IsNullOrWhiteSpace(input.EccpMaintenanceWorkOrderRemarkFilter),
                e => e.EccpMaintenanceWorkOrderRemark.ToLower()
                     == input.EccpMaintenanceWorkOrderRemarkFilter.ToLower().Trim());

            var totalCount = await query.CountAsync();

            var eccpMaintenanceWorkOrderEvaluations = new List<GetEccpMaintenanceWorkOrderEvaluationForView>();

            query.OrderBy(input.Sorting ?? "eccpMaintenanceWorkOrderEvaluation.id asc").PageBy(input).MapTo(eccpMaintenanceWorkOrderEvaluations);

            return new PagedResultDto<GetEccpMaintenanceWorkOrderEvaluationForView>(
                totalCount,
                eccpMaintenanceWorkOrderEvaluations);
        }

        /// <summary>
        /// The get all eccp maintenance work order for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrderEvaluations)]
        public async Task<PagedResultDto<EccpMaintenanceWorkOrderLookupTableDto>> GetAllEccpMaintenanceWorkOrderForLookupTable(GetAllForLookupTableInput input)
        {
            var query = this._eccpMaintenanceWorkOrderRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Remark.ToString().Contains(input.Filter));

            var totalCount = await query.CountAsync();

            var eccpMaintenanceWorkOrderList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<EccpMaintenanceWorkOrderLookupTableDto>();
            foreach (var eccpMaintenanceWorkOrder in eccpMaintenanceWorkOrderList)
            {
                lookupTableDtoList.Add(
                    new EccpMaintenanceWorkOrderLookupTableDto
                        {
                            Id = eccpMaintenanceWorkOrder.Id, DisplayName = eccpMaintenanceWorkOrder.Remark
                        });
            }

            return new PagedResultDto<EccpMaintenanceWorkOrderLookupTableDto>(totalCount, lookupTableDtoList);
        }

        /// <summary>
        /// The get eccp maintenance work order evaluation for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrderEvaluations_Edit)]
        public async Task<GetEccpMaintenanceWorkOrderEvaluationForEditOutput> GetEccpMaintenanceWorkOrderEvaluationForEdit(EntityDto input)
        {
            var eccpMaintenanceWorkOrderEvaluation =
                await this._eccpMaintenanceWorkOrderEvaluationRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpMaintenanceWorkOrderEvaluationForEditOutput
                             {
                                 EccpMaintenanceWorkOrderEvaluation =
                                     this.ObjectMapper.Map<CreateOrEditEccpMaintenanceWorkOrderEvaluationDto>(
                                         eccpMaintenanceWorkOrderEvaluation)
                             };
            var eccpMaintenanceWorkOrder =
                await this._eccpMaintenanceWorkOrderRepository.FirstOrDefaultAsync(
                    output.EccpMaintenanceWorkOrderEvaluation.WorkOrderId);
            output.EccpMaintenanceWorkOrderRemark = eccpMaintenanceWorkOrder.Remark;

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
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrderEvaluations_Create)]
        private async Task Create(CreateOrEditEccpMaintenanceWorkOrderEvaluationDto input)
        {
            var eccpMaintenanceWorkOrderEvaluation = this.ObjectMapper.Map<EccpMaintenanceWorkOrderEvaluation>(input);

            await this._eccpMaintenanceWorkOrderEvaluationRepository.InsertAsync(eccpMaintenanceWorkOrderEvaluation);
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
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceWorkOrderEvaluations_Edit)]
        private async Task Update(CreateOrEditEccpMaintenanceWorkOrderEvaluationDto input)
        {
            if (input.Id != null)
            {
                var eccpMaintenanceWorkOrderEvaluation =
                    await this._eccpMaintenanceWorkOrderEvaluationRepository.FirstOrDefaultAsync((int)input.Id);
                this.ObjectMapper.Map(input, eccpMaintenanceWorkOrderEvaluation);
            }
        }
    }
}