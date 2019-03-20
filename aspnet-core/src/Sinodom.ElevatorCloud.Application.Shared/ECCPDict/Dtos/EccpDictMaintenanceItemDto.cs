// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictMaintenanceItemDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpDict.Dtos
{
    using Abp.Application.Services.Dto;

    /// <summary>
    /// The eccp dict maintenance item dto.
    /// </summary>
    public class EccpDictMaintenanceItemDto : EntityDto
    {
        /// <summary>
        /// Gets or sets the dis order.
        /// </summary>
        public int DisOrder { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the term code.
        /// </summary>
        public string TermCode { get; set; }

        /// <summary>
        /// Gets or sets the term desc.
        /// </summary>
        public string TermDesc { get; set; }
    }
}