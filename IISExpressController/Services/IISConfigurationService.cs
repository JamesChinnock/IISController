
using System;

using IISExpressController.Configuration;

namespace IISExpressController.Services
{
    using IISExpressController.CustomExceptions;

    /// <summary>
    /// Represents the complete basic configuration required to start an instance of IIS or IISExpress 
    /// </summary>
    public class IISConfigurationService : IInternetConfigurationService
    {
        /// <summary>
        /// Private readonly field in which to store the configManager ctor parameter 
        /// </summary>
        private readonly IConfigurationManager<IISConfig> _configManager;

        /// <summary>
        /// Private type to hold the instance of the IISConfig returned by the GetSection method
        /// of the IConfigurationManager passed as the ctor parameter
        /// </summary>
        private readonly IISConfig _iisConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="IISConfigurationService"/> class
        /// </summary>
        /// <param name="configManager"> Details of the IIS Executable and config </param>
        /// <param name="configValidationService"> An IISExpressConfigValidationService implementation to validate the config values </param>
        public IISConfigurationService(IConfigurationManager<IISConfig> configManager, IConfigValidationService configValidationService)
        {
            if (configManager == null)
            {
                throw new ArgumentNullException("configManager");
            }

            if (configValidationService == null)
            {
                throw new ArgumentNullException("configValidationService");
            }

            this._configManager = configManager;
            this._iisConfig = this._configManager.GetSection("IISExpress");

            if (!IsConfigurationValid(this._iisConfig, configValidationService))
            {
                throw new Exception<IISConfigExceptionArgs>(new IISConfigExceptionArgs(this._iisConfig.IIsExecutablePath, this._iisConfig.IISConfigFilePath));
            }
        }

        /// <summary>
        /// Gets the path to the IISExpress executable
        /// </summary>
        public string ExecutablePath
        {
            get
            {
                return this._iisConfig.IIsExecutablePath;
            }
        }

        /// <summary>
        /// Gets the path to the IISExpress config file
        /// </summary>
        public string ConfigurationPath
        {
            get
            {
                return this._iisConfig.IISConfigFilePath;
            }
        }

        /// <summary>
        /// Gets the name of the website
        /// </summary>
        public string WebSite
        {
            get
            {
                return this._iisConfig.WebSiteName;
            }
        }

        /// <summary>
        /// Gets the name of the AppPool
        /// </summary>
        public string AppPool
        {
            get
            {
                return this._iisConfig.AppPoolName;
            }
        }

        /// <summary>
        /// Private method to determin whether the IISConfig values are valid paths
        /// </summary>
        /// <param name="config"> IISConfig </param>
        /// <param name="configValidationService"> IConfigValidationService </param>
        /// <returns> True if IISConfig is valid, else false</returns>
        private static bool IsConfigurationValid(IISConfig config, IConfigValidationService configValidationService)
        {
            return configValidationService.IsSatisfiedBy(config);
        }
    }
}
