// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpBaseElevatorBrandsForExcelInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorBrands.Dtos
{
    /// <summary>
    /// The get all eccp base elevator brands for excel input.
    /// </summary>
    public class GetAllEccpBaseElevatorBrandsForExcelInput
    {
        /// <summary>
        /// Gets or sets the eccp base production company name filter.
        /// </summary>
        public string ECCPBaseProductionCompanyNameFilter { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }
    }
}