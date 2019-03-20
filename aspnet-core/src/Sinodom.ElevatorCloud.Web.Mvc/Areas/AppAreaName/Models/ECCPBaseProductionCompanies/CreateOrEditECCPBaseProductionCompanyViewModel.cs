// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateOrEditECCPBaseProductionCompanyViewModel.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.ECCPBaseProductionCompanies
{
    using Sinodom.ElevatorCloud.ECCPBaseProductionCompanies.Dtos;

    /// <summary>
    ///     The create or edit eccp base production company modal view model.
    /// </summary>
    public class CreateOrEditECCPBaseProductionCompanyViewModel
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
        ///     Gets or sets the eccp base production company.
        /// </summary>
        public CreateOrEditECCPBaseProductionCompanyDto EccpBaseProductionCompany { get; set; }

        /// <summary>
        ///     The is edit mode.
        /// </summary>
        public bool IsEditMode => this.EccpBaseProductionCompany.Id.HasValue;

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