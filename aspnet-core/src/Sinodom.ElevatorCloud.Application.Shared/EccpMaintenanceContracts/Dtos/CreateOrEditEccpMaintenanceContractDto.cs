// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpMaintenanceContractDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceContracts.Dtos
{
    using System;

    using Abp.Application.Services.Dto;

    /// <summary>
    /// The create or edit eccp maintenance contract dto.
    /// </summary>
    public class CreateOrEditEccpMaintenanceContractDto : FullAuditedEntityDto<long?>
    {
        /// <summary>
        /// Gets or sets the contract picture id.
        /// </summary>
        public Guid? ContractPictureId { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the maintenance company id.
        /// </summary>
        public int MaintenanceCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the property company id.
        /// </summary>
        public int PropertyCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public DateTime StartDate { get; set; }

        public string ContractPictureDesc { get; set; }
    }
}