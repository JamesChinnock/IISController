
namespace IISExpressController.Services
{
    /// <summary>
    /// Represents the complete basic configuration required to start an instance of IIS or IISExpress 
    /// </summary>
    public interface IInternetConfigurationService
    {
        /// <summary>
        /// Gets the path to the IISExpress executable to be controlled
        /// </summary>
        string ExecutablePath { get; }

        /// <summary>
        /// Gets the path to the IISExpress executable's config file
        /// </summary>
        string ConfigurationPath { get; }

        /// <summary>
        /// Gets the IIS Website name thats to be hosted by IISExpress
        /// </summary>
        string WebSite { get; }

        /// <summary>
        /// Gets the AppPool to be used by IIS
        /// </summary>
        string AppPool { get; }
    }
}