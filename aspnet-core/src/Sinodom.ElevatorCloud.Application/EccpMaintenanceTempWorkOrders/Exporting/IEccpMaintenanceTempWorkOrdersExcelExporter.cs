// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpMaintenanceTempWorkOrdersExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders.Exporting
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpMaintenanceTempWorkOrders.Dtos;

    /// <summary>
    /// The EccpMaintenanceTempWorkOrdersExcelExporter interface.
    /// </summary>
    public interface IEccpMaintenanceTempWorkOrdersExcelExporter
    {
        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpMaintenanceTempWorkOrders">
        /// The eccp maintenance temp work orders.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        FileDto ExportToFile(List<GetEccpMaintenanceTempWorkOrderForView> eccpMaintenanceTempWorkOrders);
    }
}