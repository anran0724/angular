// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpElevatorQrCodesAppService.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpElevatorQrCodes
{
    using System;
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpElevatorQrCodes.Dtos;

    /// <summary>
    /// The EccpElevatorQrCodesAppService interface.
    /// </summary>
    public interface IEccpElevatorQrCodesAppService : IApplicationService
    {
        /// <summary>
        /// The binding.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task Binding(CreateOrEditEccpElevatorQrCodeDto input);

        /// <summary>
        /// The create or edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task CreateOrEdit(CreateOrEditEccpElevatorQrCodeDto input);

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task Delete(EntityDto<Guid> input);

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<GetEccpElevatorQrCodeForView>> GetAll(GetAllEccpElevatorQrCodesInput input);

        /// <summary>
        /// The get allbind.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<PagedResultDto<GetEccpElevatorQrCodeBindLogView>> GetAllbind(GetEccpElevatorQrCodeBindLogInput input);

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
        /// The get eccp elevator qr code for edit.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<GetEccpElevatorQrCodeForEditOutput> GetEccpElevatorQrCodeForEdit(EntityDto<Guid> input);

        /// <summary>
        /// The get eccp elevator qr codes to excel.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<FileDto> GetEccpElevatorQrCodesToExcel(GetAllEccpElevatorQrCodesForExcelInput input);

        /// <summary>
        /// The modify.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task Modify(CreateOrEditEccpElevatorQrCodeDto input);

        /// <summary>
        /// The modify eccp.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task ModifyEccp(CreateOrEditEccpElevatorQrCodeDto input);

        /// <summary>
        /// The modify qr code.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task ModifyQRCode(CreateOrEditEccpElevatorQrCodeDto input);
    }
}