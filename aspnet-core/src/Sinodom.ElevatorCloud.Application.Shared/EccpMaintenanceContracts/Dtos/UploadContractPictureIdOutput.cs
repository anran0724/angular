// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UploadContractPictureIdOutput.cs" company="Sinodom">
//   ElevatorTeam@2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sinodom.ElevatorCloud.EccpMaintenanceContracts.Dtos
{
    using Abp.Web.Models;

    /// <summary>
    /// The upload contract picture id output.
    /// </summary>
    public class UploadContractPictureIdOutput : ErrorInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadContractPictureIdOutput"/> class.
        /// </summary>
        public UploadContractPictureIdOutput()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadContractPictureIdOutput"/> class.
        /// </summary>
        /// <param name="error">
        /// The error.
        /// </param>
        public UploadContractPictureIdOutput(ErrorInfo error)
        {
            this.Code = error.Code;
            this.Details = error.Details;
            this.Message = error.Message;
            this.ValidationErrors = error.ValidationErrors;
        }

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the file token.
        /// </summary>
        public string FileToken { get; set; }

        /// <summary>
        /// Gets or sets the file type.
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public int Width { get; set; }
    }
}