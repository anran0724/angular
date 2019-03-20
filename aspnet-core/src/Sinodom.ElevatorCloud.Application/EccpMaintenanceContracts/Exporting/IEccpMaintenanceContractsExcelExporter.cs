// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpMaintenanceContractsExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceContracts.Exporting
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpMaintenanceContracts.Dtos;

    /// <summary>
    /// The EccpMaintenanceContractsExcelExporter interface.
    /// </summary>
    public interface IEccpMaintenanceContractsExcelExporter
    {
        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpMaintenanceContracts">
        /// The eccp maintenance contracts.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        FileDto ExportToFile(List<GetEccpMaintenanceContractForView> eccpMaintenanceContracts);
    }
}