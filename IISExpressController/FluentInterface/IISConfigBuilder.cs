
using System.Text;

namespace IISExpressController.Fluent
{
    public sealed class IISConfigBuilder : IISConfigBuilderBase
    {
        public IISConfigBuilder UsingConfigFile(string path)
        {
            if(!string.IsNullOrEmpty(path))
            {
                this.ConfigPath = path;
            }

            return this;
        }

        public IISConfigBuilder WithWebSite(string webSite)
        {
            if (!string.IsNullOrEmpty(webSite))
            {
                this.IisWebSite = webSite;
            }

            return this;
        }

        public IISConfigBuilder AndAppPool(string appPool)
        {
            if (!string.IsNullOrEmpty(appPool))
            {
                this.IisAppPool = appPool;
            }

            return this;
        }

        public static IISConfigBuilder Create()
        {
            var config = new IISConfigBuilder();
            return config;
        }

        public string Build()
        {
            var configArguments = new StringBuilder();
            return configArguments
                .AppendFormat(STRING_FORMAT, CONFIG, this.ConfigPath)
                .AppendFormat(STRING_FORMAT, WEBSITE, this.IisWebSite)
                .AppendFormat(STRING_FORMAT, APP_POOL, this.IisAppPool)
                .ToString();
        }
    }
}