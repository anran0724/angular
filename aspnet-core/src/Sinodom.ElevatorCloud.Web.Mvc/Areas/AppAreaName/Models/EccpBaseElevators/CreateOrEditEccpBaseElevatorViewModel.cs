// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditEccpBaseElevatorViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.EccpBaseElevators
{
    using Sinodom.ElevatorCloud.EccpBaseElevators.Dtos;

    /// <summary>
    ///     The create or edit eccp base elevator modal view model.
    /// </summary>
    public class CreateOrEditEccpBaseElevatorViewModel
    {
        /// <summary>
        ///     Gets or sets the city name.
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        ///     Gets or sets the district name.
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        ///     Gets or sets the eccp base annual inspection unit name.
        /// </summary>
        public string EccpBaseAnnualInspectionUnitName { get; set; }

        /// <summary>
        ///     Gets or sets the eccp base community name.
        /// </summary>
        public string EccpBaseCommunityName { get; set; }

        /// <summary>
        ///     Gets or sets the eccp base elevator.
        /// </summary>
        public CreateOrEditEccpBaseElevatorDto EccpBaseElevator { get; set; }

        /// <summary>
        ///     Gets or sets the eccp base elevator brand name.
        /// </summary>
        public string EccpBaseElevatorBrandName { get; set; }

        /// <summary>
        ///     Gets or sets the eccp base elevator model name.
        /// </summary>
        public string EccpBaseElevatorModelName { get; set; }

        /// <summary>
        ///     Gets or sets the eccp base elevator subsidiary info.
        /// </summary>
        public CreateOrEditEccpBaseElevatorSubsidiaryInfoDto EccpBaseElevatorSubsidiaryInfo { get; set; }

        /// <summary>
        ///     Gets or sets the eccp base maintenance company name.
        /// </summary>
        public string EccpBaseMaintenanceCompanyName { get; set; }

        /// <summary>
        ///     Gets or sets the eccp base production company name.
        /// </summary>
        public string EccpBaseProductionCompanyName { get; set; }

        /// <summary>
        ///     Gets or sets the eccp base property company name.
        /// </summary>
        public string EccpBasePropertyCompanyName { get; set; }

        /// <summary>
        ///     Gets or sets the eccp base register company name.
        /// </summary>
        public string EccpBaseRegisterCompanyName { get; set; }

        /// <summary>
        ///     Gets or sets the eccp dict elevator status name.
        /// </summary>
        public string EccpDictElevatorStatusName { get; set; }

        /// <summary>
        ///     Gets or sets the eccp dict elevator type name.
        /// </summary>
        public string EccpDictElevatorTypeName { get; set; }

        /// <summary>
        ///     Gets or sets the eccp dict place type name.
        /// </summary>
        public string EccpDictPlaceTypeName { get; set; }

        /// <summary>
        ///     The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpBaseElevator.Id.HasValue;

        /// <summary>
        ///     Gets or sets the province name.
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        ///     Gets or sets the street name.
        /// </summary>
        public string StreetName { get; set; }
    }
}