// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpBaseElevatorForView.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Sinodom.ElevatorCloud.EccpBaseElevators.Dtos
{
    /// <summary>
    /// The get eccp base elevator for view.
    /// </summary>
    public class GetEccpBaseElevatorForView
    {
        /// <summary>
        /// Gets or sets the city name.
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Gets or sets the district name.
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base annual inspection unit name.
        /// </summary>
        public string ECCPBaseAnnualInspectionUnitName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base community name.
        /// </summary>
        public string ECCPBaseCommunityName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base elevator.
        /// </summary>
        public EccpBaseElevatorDto EccpBaseElevator { get; set; }

        /// <summary>
        /// Gets or sets the eccp base elevator brand name.
        /// </summary>
        public string EccpBaseElevatorBrandName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base elevator model name.
        /// </summary>
        public string EccpBaseElevatorModelName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base maintenance company name.
        /// </summary>
        public string ECCPBaseMaintenanceCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base production company name.
        /// </summary>
        public string ECCPBaseProductionCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base property company name.
        /// </summary>
        public string ECCPBasePropertyCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the eccp base register company name.
        /// </summary>
        public string ECCPBaseRegisterCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict elevator status name.
        /// </summary>
        public string ECCPDictElevatorStatusName { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict elevator type name.
        /// </summary>
        public string EccpDictElevatorTypeName { get; set; }

        /// <summary>
        /// Gets or sets the eccp dict place type name.
        /// </summary>
        public string EccpDictPlaceTypeName { get; set; }

        /// <summary>
        /// Gets or sets the province name.
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// Gets or sets the street name.
        /// </summary>
        public string StreetName { get; set; }
    }
}