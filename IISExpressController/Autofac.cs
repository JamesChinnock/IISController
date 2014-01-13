
using Autofac;

namespace IISExpressController
{
    using IISExpressController.Services;

    /// <summary>
    /// Represents the ability to create, build, register and setup Autofac components
    /// </summary>
    public class AutofacCreator
    {
        private ContainerBuilder builder;

        public AutofacCreator()
        {
            this.builder = new ContainerBuilder();

            this.builder.RegisterType<IISConfigurationService>().As<IInternetConfigurationService>();
            this.builder.RegisterType<IISConfigValidationService>().As<IConfigValidationService>();



        }
    }
}
