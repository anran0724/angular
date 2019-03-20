// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpMaintenanceWorksAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorks
{
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;

    using Sinodom.ElevatorCloud.EccpMaintenanceWorks.Dtos;

    /// <summary>
    /// The EccpMaintenanceWorksAppService interface.
    /// </summary>
    public interface IEccpMaintenanceWorksAppService : IApplicationService
    {
        /// <summary>
        /// The app create.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task AppCreate(CreateOrEditAppEccpMaintenanceWorkDto input);

        /// <summary>
        /// The create or edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task CreateOrEdit(CreateOrEditEccpMaintenanceWorkDto input);

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
        Task<PagedResultDto<GetEccpMaintenanceWorkForView>> GetAll(GetAllEccpMaintenanceWorksInput input);

        /// <summary>
        /// The get all eccp maintenance template node for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<EccpMaintenanceTemplateNodeLookupTableDto>> GetAllEccpMaintenanceTemplateNodeForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get all eccp maintenance work order for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<EccpMaintenanceWorkOrderLookupTableDto>> GetAllEccpMaintenanceWorkOrderForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get app all.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<GetEccpMaintenanceWorkForView>> GetAppAll(GetAllEccpMaintenanceWorksInput input);

        /// <summary>
        /// The get eccp maintenance work for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<GetEccpMaintenanceWorkForEditOutput> GetEccpMaintenanceWorkForEdit(EntityDto input);

        /// <summary>
        /// 工作记录表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<GetWorkForRecordOutput>> GetWorkRecords(GetAllEccpMaintenanceWorksInput input);

        /// <summary>
        /// 工作记录详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<GetEccpMaintenanceWorkFlowDetails>> GetEccpMaintenanceWorkFlowDetails(int id);


        string GetWorkQrCode(string key, int userId, int workId, string longitude, string latitude);
    }
}