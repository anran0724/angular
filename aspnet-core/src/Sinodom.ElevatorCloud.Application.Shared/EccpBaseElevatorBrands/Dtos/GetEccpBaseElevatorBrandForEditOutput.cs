// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpBaseElevatorBrandForEditOutput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorBrands.Dtos
{
    /// <summary>
    /// The get eccp base elevator brand for edit output.
    /// </summary>
    public class GetEccpBaseElevatorBrandForEditOutput
    {
        /// <summary>
        /// Gets or sets the eccp base elevator brand.
        /// </summary>
        public CreateOrEditEccpBaseElevatorBrandDto EccpBaseElevatorBrand { get; set; }

        /// <summary>
        /// Gets or sets the eccp base production company name.
        /// </summary>
        public string ECCPBaseProductionCompanyName { get; set; }
    }
}