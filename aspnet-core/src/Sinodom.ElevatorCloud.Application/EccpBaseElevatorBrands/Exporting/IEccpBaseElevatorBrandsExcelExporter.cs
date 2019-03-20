// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEccpBaseElevatorBrandsExcelExporter.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorBrands.Exporting
{
    using System.Collections.Generic;

    using Sinodom.ElevatorCloud.Dto;
    using Sinodom.ElevatorCloud.EccpBaseElevatorBrands.Dtos;

    /// <summary>
    ///     The EccpBaseElevatorBrandsExcelExporter interface.
    /// </summary>
    public interface IEccpBaseElevatorBrandsExcelExporter
    {
        /// <summary>
        /// The export to file.
        /// </summary>
        /// <param name="eccpBaseElevatorBrands">
        /// The eccp base elevator brands.
        /// </param>
        /// <returns>
        /// The <see cref="FileDto"/>.
        /// </returns>
        FileDto ExportToFile(List<GetEccpBaseElevatorBrandForView> eccpBaseElevatorBrands);
    }
}