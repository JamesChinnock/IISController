namespace IISExpressController.Configuration
{
    public interface IConfigurationManager<out T>  
    {
        T GetSection(string identifier);
    }
}