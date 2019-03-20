// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllEccpDictElevatorTypesInput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpDict.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The get all eccp dict elevator types input.
    /// </summary>
    public class GetAllEccpDictElevatorTypesInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }
    }
}