
namespace IISExpressController.CustomExceptions
{
    /// <summary>
    /// Class that represents an exception thrown when an IISConfig is invalid . 
    /// </summary>
    public class IISConfigExceptionArgs : ExceptionArgs
    {
        /// <summary>
        /// Private field to hold the readonly executablePath ctor parameter
        /// </summary>
        private readonly string _executablePath;

        /// <summary>
        /// Private field to hold the readonly configPath ctor parameter
        /// </summary>
        private readonly string _configPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="IISConfigExceptionArgs"/> class
        /// </summary>
        /// <param name="executablePath"> Name of the Process </param>
        /// <param name="configPath"> Id of the Process </param>
        public IISConfigExceptionArgs(string executablePath, string configPath)
        {
            this._executablePath = executablePath;
            this._configPath = configPath;
        }

        /// <summary>
        /// Gets the executable path from the config settings
        /// </summary>
        public string ExecutablePath 
        { 
            get
            {
                return this._executablePath;
            }
        }

        /// <summary>
        /// Gets the config file path from the config settings
        /// </summary>
        public string ConfigPath
        {
            get
            {
                return this._configPath;
            }
        }

        /// <summary>
        /// Overridden Message property
        /// </summary>
        public override string Message
        {
            get
            {
                return string.Format(@"One or both of the paths: {0}, or {1} are invalid", this.ExecutablePath, this.ConfigPath);
            }
        }
    }
}
