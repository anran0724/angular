// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpMaintenancePlansExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenancePlans.Exporting
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpMaintenancePlans.Dtos;

    /// <summary>
    /// The EccpMaintenancePlansExcelExporter interface.
    /// </summary>
    public interface IEccpMaintenancePlansExcelExporter
    {
        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpMaintenancePlans">
        /// The eccp maintenance plans.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        FileDto ExportToFile(List<GetEccpMaintenancePlanForView> eccpMaintenancePlans);
    }
}