// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EccpDictMaintenanceItemTemplateNodeDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpDict.Dtos
{
    /// <summary>
    /// The eccp dict maintenance item template node dto.
    /// </summary>
    public class EccpDictMaintenanceItemTemplateNodeDto
    {
        /// <summary>
        /// Gets or sets the dict maintenance item id.
        /// </summary>
        public int DictMaintenanceItemID { get; set; }

        /// <summary>
        /// Gets or sets the dis order.
        /// </summary>
        public int DisOrder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is assigned.
        /// </summary>
        public bool IsAssigned { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}