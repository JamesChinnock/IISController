
using System.IO;

using IISExpressController.Configuration;

namespace IISExpressController.Services
{
    /// <summary>
    /// Represents the ability to ensure that an IISConfig instance is valid
    /// </summary>
    public class IISConfigValidationService : IConfigValidationService
    {
        /// <summary>
        /// Method to ensure the IExecutableDetails objects properties hold valid paths
        /// </summary>
        /// <param name="details"> the IExecutableDetails containing the path data </param>
        /// <returns> True if the exe and config paths are valid </returns>
        public bool IsSatisfiedBy(IISConfig details)
        {
            return File.Exists(details.IIsExecutablePath) && File.Exists(details.IISConfigFilePath);
        }
    }
}