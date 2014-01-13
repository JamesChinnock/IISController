
using IISExpressController.Configuration;

namespace IISExpressControllerTests.TestData
{
    /// <summary>
    /// Static class simply to return an instance of IISConfig set with some test paths
    /// </summary>
    public static class TestIISConfig 
    {
        /// <summary>
        /// Gets an IISConfig instance set up with test data values
        /// </summary>
        public static IISConfig Instance
        {
            get
            {
                return new IISConfig
                    {
                        IIsExecutablePath = @"C:\Program Files (x86)\IIS Express\iisexpress.exe",
                        IISConfigFilePath = @"C:\Program Files (x86)\IIS Express\config\administration.config",
                        WebSiteName = @"TestWebSiteBeingHosted",
                        AppPoolName = @"TestAppPoolName"
                    };
            }
        }

    }
}
