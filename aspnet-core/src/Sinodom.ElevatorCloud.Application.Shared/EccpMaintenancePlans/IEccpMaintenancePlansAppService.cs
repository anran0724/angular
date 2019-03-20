// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpMaintenancePlansAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenancePlans
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpMaintenancePlans.Dtos;

    /// <summary>
    /// The EccpMaintenancePlansAppService interface.
    /// </summary>
    public interface IEccpMaintenancePlansAppService : IApplicationService
    {
        /// <summary>
        /// The close plan.
        /// </summary>
        /// <param name="planGroupGuid">
        /// The plan group guid.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task ClosePlan(Guid? planGroupGuid);

        /// <summary>
        /// The create or edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task CreateOrEdit(CreateOrEditEccpMaintenancePlanInfoDto input);

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task Delete(EntityDto input);

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<GetEccpMaintenancePlanForView>> GetAll(GetAllEccpMaintenancePlansInput input);

        /// <summary>
        /// The get all eccp base elevator for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<EccpBaseElevatorLookupTableDto>> GetAllEccpBaseElevatorForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get all maintenance templates for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<MaintenanceTemplatesLookupTableDto>> GetAllMaintenanceTemplatesForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get all maintenance type.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<GetEccpMaintenancePlanForEditOutput> GetEccpMaintenancePlanForCreate();

        /// <summary>
        /// The get all maintenance user for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<MaintenanceUserLookupTableDto>> GetAllMaintenanceUserForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get all property user for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<PropertyUserLookupTableDto>> GetAllPropertyUserForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get eccp maintenance plan for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<GetEccpMaintenancePlanForEditOutput> GetEccpMaintenancePlanForEdit(EntityDto input);

        /// <summary>
        /// The get eccp maintenance plans to excel.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<FileDto> GetEccpMaintenancePlansToExcel(GetAllEccpMaintenancePlansForExcelInput input);

        Task<PagedResultDto<GetEccpMaintenanceWorkOrderPlanForView>> GetAllWorkOrdersByPlanId(
            GetAllEccpMaintenanceWorkOrderPlansInput input);

        Task<GetEccpMaintenancePlanForEditOutput> GetEccpMaintenancePlanForView(EntityDto input);
    }
}