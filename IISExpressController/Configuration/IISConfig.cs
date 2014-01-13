
using System.Configuration;

namespace IISExpressController.Configuration
{
    /// <summary>
    /// Type representing the path details for the IISExpress executable and config
    /// </summary>
    public class IISConfig : ConfigurationSection
    {
        /// <summary>
        /// Gets or sets the path to the IISExpress executable to be controlled
        /// </summary>
        [ConfigurationProperty("IIsExecutablePath", IsRequired = true)]
        public string IIsExecutablePath
        {
            get
            {
                return (string)this["IIsExecutablePath"];
            } 

            set
            {
                this["IIsExecutablePath"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the path to the IISExpress executable's config file
        /// </summary>
        [ConfigurationProperty("IISConfigFilePath", IsRequired = true)]
        public string IISConfigFilePath
        {
            get
            {
                return (string)this["IISConfigFilePath"];
            }

            set
            {
                this["IISConfigFilePath"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the IIS Website name thats to be hosted by IISExpress
        /// </summary>
        [ConfigurationProperty("WebSiteName", IsRequired = true)]
        public string WebSiteName
        {
            get
            {
                return (string)this["WebSiteName"];
            }

            set
            {
                this["WebSiteName"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the AppPool to be used by IIS
        /// </summary>
        [ConfigurationProperty("AppPoolName", IsRequired = true)]
        public string AppPoolName
        {
            get
            {
                return (string)this["AppPoolName"];
            }

            set
            {
                this["AppPoolName"] = value;
            }
        }
    }


}