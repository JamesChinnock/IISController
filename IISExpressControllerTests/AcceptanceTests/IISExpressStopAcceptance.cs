
using IISExpressController;
using IISExpressController.Services;

using Machine.Specifications;

using Moq;

using It = Machine.Specifications.It;

namespace IISExpressControllerTests.AcceptanceTests
{
    /// <summary>
    /// Initializes a new instance of the <see cref="When_stop_is_invoked_on_an_IISExpress_instance"/> class.
    /// </summary>
    [Subject(typeof(IISExpress))]
    public class When_stop_is_invoked_on_an_IISExpress_instance
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
        /// Invoke the Stop method on the IISExpress 
        /// </summary>
        private Because of = () =>
            {
                _sut.Start();
                _sut.Stop();
            };

        /// <summary>
        /// Assertion that Stop is invoked on the IProcessManagementService
        /// </summary>
        private It the_Stop_method_is_also_invoked_on_the_IProcessManagementService =
            () => _mockProcessManagementService.Verify(v => v.Stop(), Times.AtLeastOnce);
    }
}
