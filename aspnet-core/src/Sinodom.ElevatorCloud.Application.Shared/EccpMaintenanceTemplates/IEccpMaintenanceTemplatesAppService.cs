// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpMaintenanceTemplatesAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTemplates
{
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpMaintenanceTemplates.Dtos;

    /// <summary>
    /// The EccpMaintenanceTemplatesAppService interface.
    /// </summary>
    public interface IEccpMaintenanceTemplatesAppService : IApplicationService
    {
        /// <summary>
        /// The create or edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task CreateOrEdit(CreateOrEditEccpMaintenanceTemplateDto input);

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
        Task<PagedResultDto<GetEccpMaintenanceTemplateForView>> GetAll(GetAllEccpMaintenanceTemplatesInput input);

        /// <summary>
        /// The get all eccp dict elevator type for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<EccpDictElevatorTypeLookupTableDto>> GetAllEccpDictElevatorTypeForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get all eccp dict maintenance type for lookup table.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<EccpDictMaintenanceTypeLookupTableDto>> GetAllEccpDictMaintenanceTypeForLookupTable(
            GetAllForLookupTableInput input);

        /// <summary>
        /// The get eccp maintenance template for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<GetEccpMaintenanceTemplateForEditOutput> GetEccpMaintenanceTemplateForEdit(EntityDto input);

        /// <summary>
        /// The get eccp maintenance templates to excel.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<FileDto> GetEccpMaintenanceTemplatesToExcel(GetAllEccpMaintenanceTemplatesForExcelInput input);
    }
}