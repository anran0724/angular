// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpBaseElevatorBrandForView.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorBrands.Dtos
{
    /// <summary>
    /// The get eccp base elevator brand for view.
    /// </summary>
    public class GetEccpBaseElevatorBrandForView
    {
        /// <summary>
        /// Gets or sets the eccp base elevator brand.
        /// </summary>
        public EccpBaseElevatorBrandDto EccpBaseElevatorBrand { get; set; }

        /// <summary>
        /// Gets or sets the eccp base production company name.
        /// </summary>
        public string ECCPBaseProductionCompanyName { get; set; }
    }
}