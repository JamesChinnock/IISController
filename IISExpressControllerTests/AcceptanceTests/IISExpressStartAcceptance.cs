
using System.Diagnostics;

using IISExpressController;
using IISExpressController.Services;

using Machine.Specifications;

using Moq;

using It = Machine.Specifications.It;

namespace IISExpressControllerTests.AcceptanceTests
{
    /// <summary>
    /// Initializes a new instance of the <see cref="When_start_is_invoked_on_an_IISExpress_instance"/> class.
    /// </summary>
    [Subject(typeof(IISExpress))]
    public class When_start_is_invoked_on_an_IISExpress_instance
    {
        /// <summary>
        /// Mock IInternetConfigurationService to be set up in the Establish delegate and passed as constructor parameter
        /// </summary>
        private static Mock<IInternetConfigurationService> _mockIIInternetConfigurationService;

        /// <summary>
        /// Mock IConfigValidationService to be set up in the Establish delegate and passed as constructor parameter
        /// </summary>
        private static Mock<IProcessManagementService> _mockProcessManagementService;

        /// <summary>
        /// The IISExpress instance to be tested
        /// </summary>
        private static IISExpress _sut;

        /// <summary>
        /// Setup the mocks and the SUT 
        /// </summary>
        private Establish that = () =>
        {
            _mockIIInternetConfigurationService = new Mock<IInternetConfigurationService>();
            _mockProcessManagementService = new Mock<IProcessManagementService>();

            _sut = new IISExpress(_mockIIInternetConfigurationService.Object, _mockProcessManagementService.Object);
        };

        /// <summary>
        /// Invoke the Start method on the IISExpress 
        /// </summary>
        private Because of = () => _sut.Start();

        /// <summary>
        /// Assertion that Start is invoked on the IProcessManagementService
        /// </summary>
        private It the_Start_method_is_also_invoked_on_the_IProcessManagementService =
            () => _mockProcessManagementService.Verify(v => v.Start(Moq.It.IsAny<ProcessStartInfo>()), Times.AtLeastOnce);

        /// <summary>
        /// Assertion that the ExecutablePath property is used on the IConfigValidationService
        /// </summary>
        private It the_Getter_is_invoked_on_the_ExecutablePath_property_of_the_IInternetConfigurationService =
            () => _mockIIInternetConfigurationService.VerifyGet(v => v.ExecutablePath);

        /// <summary>
        /// Assertion that the ConfigurationPath property is used on the IConfigValidationService
        /// </summary>
        private It the_Getter_is_invoked_on_the_ConfigurationPath_property_of_the_IInternetConfigurationService =
            () => _mockIIInternetConfigurationService.VerifyGet(v => v.ConfigurationPath);

        /// <summary>
        /// Assertion that the WebSite property is used on the IConfigValidationService
        /// </summary>
        private It the_Getter_is_invoked_on_the_WebSite_property_of_the_IInternetConfigurationService =
            () => _mockIIInternetConfigurationService.VerifyGet(v => v.WebSite);

        /// <summary>
        /// Assertion that the AppPool property is used on the IConfigValidationService
        /// </summary>
        private It the_Getter_is_invoked_on_the_AppPool_property_of_the_IInternetConfigurationService =
            () => _mockIIInternetConfigurationService.VerifyGet(v => v.AppPool);
    }
}
