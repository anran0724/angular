// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpPropertyCompanyChangeLogDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.ECCPBasePropertyCompanies.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp property company change log dto.
    /// </summary>
    public class EccpPropertyCompanyChangeLogDto : EntityDto
    {
        /// <summary>
        /// Gets or sets the field name.
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or sets the new value.
        /// </summary>
        public string NewValue { get; set; }

        /// <summary>
        /// Gets or sets the old value.
        /// </summary>
        public string OldValue { get; set; }

        /// <summary>
        /// Gets or sets the property company id.
        /// </summary>
        public int PropertyCompanyId { get; set; }
    }
}