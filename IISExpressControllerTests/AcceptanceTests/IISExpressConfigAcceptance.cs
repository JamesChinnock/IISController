
using IISExpressController.Services;

using Machine.Specifications;

namespace IISExpressControllerTests.AcceptanceTests
{
    using IISExpressController.Configuration;

    using Moq;

    using It = Machine.Specifications.It;

    /// <summary>
    ///  Initializes a new instance of the <see cref="When_a_new_IISConfigurationService_is_instantiated"/> class.
    /// </summary>
    [Subject(typeof(IISConfigurationService))]
    public class When_a_new_IISConfigurationService_is_instantiated
    {
        /// <summary>
        /// Mock IConfigurationManager of IISConfig to be set up in the Establish delegate and passed as constructor parameter
        /// </summary>
        private static Mock<IConfigurationManager<IISConfig>> _mockIISConfigurationService;

        /// <summary>
        /// Mock IConfigValidationService to be set up in the Establish delegate and passed as constructor parameter
        /// </summary>
        private static Mock<IConfigValidationService> _mockConfigValidationService;

        /// <summary>
        /// The IISConfigurationService instance to be tested
        /// </summary>
        private static IISConfigurationService _sut;

        /// <summary>
        /// Setup the mocks and the SUT 
        /// </summary>
        private Establish that = () =>
        {
            _mockIISConfigurationService = new Mock<IConfigurationManager<IISConfig>>();
            _mockConfigValidationService = new Mock<IConfigValidationService>();

            _mockConfigValidationService.Setup(v => v.IsSatisfiedBy(Moq.It.IsAny<IISConfig>())).Returns(true);

            _sut = new IISConfigurationService(_mockIISConfigurationService.Object, _mockConfigValidationService.Object);
        };

        /// <summary>
        /// Assertion that GetSection is invoked on the IConfigurationManager instance
        /// </summary>
        private It the_GetSection_method_is_invoked_on_the_IISConfigurationService =
            () => _mockIISConfigurationService.Verify(s => s.GetSection(Moq.It.IsAny<string>()), Times.AtLeastOnce);

        /// <summary>
        /// Assertion that IsSatisfiedBy is invoked on the IConfigValidationService
        /// </summary>
        private It the_IsSatisfiedBy_method_is_invoked_on_the_IISConfigurationService =
            () => _mockConfigValidationService.Verify(s => s.IsSatisfiedBy(Moq.It.IsAny<IISConfig>()), Times.AtLeastOnce);
    }
}
