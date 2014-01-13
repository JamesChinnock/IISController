namespace IISExpressController.Configuration
{
    using System.Configuration;

    public class IISConfigurationManager<T> : IConfigurationManager<T> where T : class 
    {
        public T GetSection(string identifier)
        {
            return (T)ConfigurationManager.GetSection(identifier);
        }
    }
}