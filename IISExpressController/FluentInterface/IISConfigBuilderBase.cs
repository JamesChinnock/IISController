
namespace IISExpressController.Fluent
{
    public abstract class IISConfigBuilderBase
    {
        protected IISConfigBuilderBase()
        {
            this.ConfigPath = string.Empty;
            this.IisWebSite = string.Empty;
            this.IisAppPool = string.Empty;
        }

        protected string ConfigPath { get; set; }

        protected string IisWebSite { get; set; }

        protected string IisAppPool { get; set; }

        protected const string STRING_FORMAT = @"/{0}:{1} ";

        protected const string CONFIG = "config";

        protected const string WEBSITE = "site";

        protected const string APP_POOL = "apppool";
    }
}
