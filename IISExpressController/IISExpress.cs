
using System;
using System.Diagnostics;

using IISExpressController.Fluent;
using IISExpressController.Services;

namespace IISExpressController
{
    /// <summary>
    /// Sealed class that represents control over an IISExpress instance
    /// </summary>
    public sealed class IISExpress
    {
        /// <summary>
        /// Private field to hold the configService ctor parameter
        /// </summary>
        private readonly IInternetConfigurationService _configService;

        /// <summary>
        /// Private field to hold the processManagementService ctor parameter
        /// </summary>
        private readonly IProcessManagementService _processManagementService;
       
        /// <summary>
        /// Initializes a new instance of the <see cref="IISExpress"/> class
        /// </summary>
        /// <param name="configService"> An IInternetConfigurationService to manage the configuration values </param>
        /// <param name="processManagementService"> An IISExpressConfigValidationService implementation to validate the config values </param>
        public IISExpress(IInternetConfigurationService configService, IProcessManagementService processManagementService)
        {
            if (configService == null)
            {
                throw new ArgumentNullException("configService");
            }

            if (processManagementService == null)
            {
                throw new ArgumentNullException("processManagementService");
            }

            this._configService = configService;
            this._processManagementService = processManagementService;
        }

        /// <summary>
        /// Gets the  windows Process Id that IIS is currently running in
        /// </summary>
        public int IISProcessId
        {
            get
            {
                return this._processManagementService.ProcessId;
            }
        }

        /// <summary>
        /// Method to start the IISExpress instance
        /// </summary>
        public void Start()
        {
            this._processManagementService.Start(
                new ProcessStartInfo
                    {
                        FileName = this._configService.ExecutablePath,
                        Arguments = this.BuildProcessArguments(),
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    });
        }

        /// <summary>
        /// Method to stop the IISExpress instance
        /// </summary>
        public void Stop()
        {
            this._processManagementService.Stop();
        }

        /// <summary>
        /// Private heloper method to set the arguments that will be passed to the IISExpress process
        /// when it is created by the Start method
        /// </summary>
        /// <returns> String built up of IISExpress config values </returns>
        private string BuildProcessArguments()
        {
            return IISConfigBuilder.Create()
                                    .UsingConfigFile(this._configService.ConfigurationPath)
                                    .WithWebSite(this._configService.WebSite)
                                    .AndAppPool(this._configService.AppPool)
                                    .Build();
        }
    }
}
