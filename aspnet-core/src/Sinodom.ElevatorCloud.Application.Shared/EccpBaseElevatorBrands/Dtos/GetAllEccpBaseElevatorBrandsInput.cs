// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpBaseElevatorBrandsInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpBaseElevatorBrands.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp base elevator brands input.
    /// </summary>
    public class GetAllEccpBaseElevatorBrandsInput : PagedAndSortedResultRequestDto
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