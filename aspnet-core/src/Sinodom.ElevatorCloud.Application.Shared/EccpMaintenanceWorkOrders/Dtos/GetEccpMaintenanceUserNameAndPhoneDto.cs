// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetEccpMaintenanceUserNameAndPhoneDto.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceWorkOrders.Dtos
{
    /// <summary>
    /// The get eccp maintenance user name and phone dto.
    /// </summary>
    public class GetEccpMaintenanceUserNameAndPhoneDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        public long UserId { get; set; }
    }
}