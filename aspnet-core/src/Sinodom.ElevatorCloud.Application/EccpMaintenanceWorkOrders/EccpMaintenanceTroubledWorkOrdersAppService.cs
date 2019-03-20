// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpMaintenanceTroubledWorkOrdersAppService.cs" company="Sinodom">
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
    using Sinodom.ElevatorCloud.EccpDict;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos;

    /// <summary>
    /// The eccp maintenance troubled work orders app service.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders)]
    public class EccpMaintenanceTroubledWorkOrdersAppService : ElevatorCloudAppServiceBase,
                                                               IEccpMaintenanceTroubledWorkOrdersAppService
    {
        /// <summary>
        /// The _eccp dict maintenance status repository.
        /// </summary>
        private readonly IRepository<EccpDictMaintenanceStatus> _eccpDictMaintenanceStatusRepository;

        /// <summary>
        /// The _eccp maintenance troubled work order repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceTroubledWorkOrder> _eccpMaintenanceTroubledWorkOrderRepository;

        /// <summary>
        /// The _eccp maintenance work order repository.
        /// </summary>
        private readonly IRepository<EccpMaintenanceWorkOrder, int> _eccpMaintenanceWorkOrderRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EccpMaintenanceTroubledWorkOrdersAppService"/> class.
        /// </summary>
        /// <param name="eccpMaintenanceTroubledWorkOrderRepository">
        /// The eccp maintenance troubled work order repository.
        /// </param>
        /// <param name="eccpMaintenanceWorkOrderRepository">
        /// The eccp maintenance work order repository.
        /// </param>
        /// <param name="eccpDictMaintenanceStatusRepository">
        /// The eccp dict maintenance status repository.
        /// </param>
        public EccpMaintenanceTroubledWorkOrdersAppService(
            IRepository<EccpMaintenanceTroubledWorkOrder> eccpMaintenanceTroubledWorkOrderRepository,
            IRepository<EccpMaintenanceWorkOrder, int> eccpMaintenanceWorkOrderRepository,
            IRepository<EccpDictMaintenanceStatus> eccpDictMaintenanceStatusRepository)
        {
            this._eccpMaintenanceTroubledWorkOrderRepository = eccpMaintenanceTroubledWorkOrderRepository;
            this._eccpMaintenanceWorkOrderRepository = eccpMaintenanceWorkOrderRepository;
            this._eccpDictMaintenanceStatusRepository = eccpDictMaintenanceStatusRepository;
        }

        /// <summary>
        /// The apply troubled work order.
        /// 申请问题工单
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<int> ApplyTroubledWorkOrder(CreateOrEditEccpMaintenanceTroubledWorkOrderDto input)
        {
            int result;
            if (input.MaintenanceWorkOrderId > 0)
            {
                var eccpMaintenanceWork =
                    await this._eccpMaintenanceWorkOrderRepository.FirstOrDefaultAsync(input.MaintenanceWorkOrderId);
                if (eccpMaintenanceWork != null)
                {
                    var dictMaintenanceStatus =
                        await this._eccpDictMaintenanceStatusRepository.FirstOrDefaultAsync(
                            eccpMaintenanceWork.MaintenanceStatusId);
                    if (dictMaintenanceStatus.Name != "已完成" && dictMaintenanceStatus.Name != "已超期")
                    {
                        var eccpMaintenanceTroubledWorkOrder =
                            this.ObjectMapper.Map<EccpMaintenanceTroubledWorkOrder>(input);

                        eccpMaintenanceTroubledWorkOrder.WorkOrderStatusName = dictMaintenanceStatus.Name;
                        eccpMaintenanceTroubledWorkOrder.IsAudit = 0;
                        if (this.AbpSession.TenantId != null)
                        {
                            eccpMaintenanceTroubledWorkOrder.TenantId = (int)this.AbpSession.TenantId;
                        }

                        await this._eccpMaintenanceTroubledWorkOrderRepository.InsertAsync(
                            eccpMaintenanceTroubledWorkOrder);
                        result = 1; // 申请成功
                    }
                    else
                    {
                        result = -3; // 工单已完成或已超期
                    }
                }
                else
                {
                    result = -2; // 工单不存在
                }
            }
            else
            {
                result = -1; // 工单ID错误
            }

            return result;
        }

        /// <summary>
        /// The audit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Audit)]
        public async Task Audit(AuditEccpMaintenanceTroubledWorkOrderDto input)
        {
            if (input.Id != null)
            {
                var eccpMaintenanceTroubledWorkOrder =
                    await this._eccpMaintenanceTroubledWorkOrderRepository.FirstOrDefaultAsync((int)input.Id);
                var eccpMaintenanceWorkOrder =
                    await this._eccpMaintenanceWorkOrderRepository.FirstOrDefaultAsync(
                        eccpMaintenanceTroubledWorkOrder.MaintenanceWorkOrderId);
                if (eccpMaintenanceWorkOrder != null)
                {
                    var eccpDictMaintenanceStatus =
                        await this._eccpDictMaintenanceStatusRepository.FirstOrDefaultAsync(e => e.Name == "问题工单");
                    if (eccpDictMaintenanceStatus != null)
                    {
                        eccpMaintenanceWorkOrder.MaintenanceStatusId = eccpDictMaintenanceStatus.Id;
                        await this._eccpMaintenanceWorkOrderRepository.UpdateAsync(eccpMaintenanceWorkOrder);
                    }
                }

                this.ObjectMapper.Map(input, eccpMaintenanceTroubledWorkOrder);
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
        public async Task CreateOrEdit(CreateOrEditEccpMaintenanceTroubledWorkOrderDto input)
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
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Delete)]
        public async Task Delete(EntityDto input)
        {
            await this._eccpMaintenanceTroubledWorkOrderRepository.DeleteAsync(input.Id);
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
        public async Task<PagedResultDto<GetEccpMaintenanceTroubledWorkOrderForView>> GetAll(
            GetAllEccpMaintenanceTroubledWorkOrdersInput input)
        {
            var filteredEccpMaintenanceTroubledWorkOrders = this._eccpMaintenanceTroubledWorkOrderRepository.GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.WorkOrderStatusName.Contains(input.Filter) || e.TroubledDesc.Contains(input.Filter))
                .WhereIf(input.IsAuditFilter != -1, e => e.IsAudit == input.IsAuditFilter).WhereIf(
                    !string.IsNullOrWhiteSpace(input.EccpMaintenanceTroubledDescFilter),
                    e => e.TroubledDesc == input.EccpMaintenanceTroubledDescFilter.ToLower().Trim());

            var eccpMaintenanceWorkOrders = this._eccpMaintenanceWorkOrderRepository.GetAll()
                .Include(t => t.MaintenanceStatus).Where(t => t.MaintenanceStatus.Name != "已超期")
                .Where(t => t.MaintenanceStatus.Name != "已完成");

            var query = (from o in filteredEccpMaintenanceTroubledWorkOrders
                         join o1 in eccpMaintenanceWorkOrders on o.MaintenanceWorkOrderId equals o1.Id
                         select new 
                                    {
                                        EccpMaintenanceTroubledWorkOrder = o,
                                        EccpMaintenanceWorkOrderRemark = o1 == null ? string.Empty : o1.Remark
                                    }).WhereIf(
                !string.IsNullOrWhiteSpace(input.EccpMaintenanceWorkOrderRemarkFilter),
                e => e.EccpMaintenanceWorkOrderRemark.ToLower()
                     == input.EccpMaintenanceWorkOrderRemarkFilter.ToLower().Trim());

            var totalCount = await query.CountAsync();

            var eccpMaintenanceTroubledWorkOrders = new List<GetEccpMaintenanceTroubledWorkOrderForView>();

            query.OrderBy(input.Sorting ?? "eccpMaintenanceTroubledWorkOrder.id asc").PageBy(input).MapTo(eccpMaintenanceTroubledWorkOrders);

            return new PagedResultDto<GetEccpMaintenanceTroubledWorkOrderForView>(
                totalCount,
                eccpMaintenanceTroubledWorkOrders);
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
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders)]
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
        /// The get eccp maintenance troubled work order for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Edit)]
        public async Task<GetEccpMaintenanceTroubledWorkOrderForEditOutput> GetEccpMaintenanceTroubledWorkOrderForEdit(
            EntityDto input)
        {
            var eccpMaintenanceTroubledWorkOrder =
                await this._eccpMaintenanceTroubledWorkOrderRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEccpMaintenanceTroubledWorkOrderForEditOutput
                             {
                                 EccpMaintenanceTroubledWorkOrder =
                                     this.ObjectMapper.Map<CreateOrEditEccpMaintenanceTroubledWorkOrderDto>(
                                         eccpMaintenanceTroubledWorkOrder)
                             };
            var eccpMaintenanceWorkOrder =
                await this._eccpMaintenanceWorkOrderRepository.FirstOrDefaultAsync(
                    output.EccpMaintenanceTroubledWorkOrder.MaintenanceWorkOrderId);
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
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Create)]
        private async Task Create(CreateOrEditEccpMaintenanceTroubledWorkOrderDto input)
        {
            var eccpMaintenanceTroubledWorkOrder = this.ObjectMapper.Map<EccpMaintenanceTroubledWorkOrder>(input);

            if (this.AbpSession.TenantId != null)
            {
                eccpMaintenanceTroubledWorkOrder.TenantId = (int)this.AbpSession.TenantId;
            }

            await this._eccpMaintenanceTroubledWorkOrderRepository.InsertAsync(eccpMaintenanceTroubledWorkOrder);
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
        [AbpAuthorize(AppPermissions.Pages_EccpMaintenance_EccpMaintenanceTroubledWorkOrders_Edit)]
        private async Task Update(CreateOrEditEccpMaintenanceTroubledWorkOrderDto input)
        {
            if (input.Id != null)
            {
                var eccpMaintenanceTroubledWorkOrder =
                    await this._eccpMaintenanceTroubledWorkOrderRepository.FirstOrDefaultAsync((int)input.Id);
                this.ObjectMapper.Map(input, eccpMaintenanceTroubledWorkOrder);
            }
        }
    }
}