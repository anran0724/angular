// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpMaintenanceWorkOrdersExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Exporting
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpDict.Dtos;
    using Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos;

    /// <summary>
    /// The EccpMaintenanceWorkOrdersExcelExporter interface.
    /// </summary>
    public interface IEccpMaintenanceWorkOrdersWordExporter
    {
        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpDictMaintenanceItems">
        /// The eccp dict maintenance items.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        FileDto ExportToFile(byte[] bytes);
    }
}