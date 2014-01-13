
using System;

using IISExpressController.Configuration;
using IISExpressController.Services;

using Moq;

using NUnit.Framework;

namespace IISExpressControllerTests.UnitTests
{
    using IISExpressController.CustomExceptions;

    /// <summary>
    /// Class to encapsulate IISConfigurationService specific unit tests
    /// </summary>
    [TestFixture]
    public class IISConfigurationServiceUnitTests
    {
        /// <summary>
        /// private field to hold a mocked IISConfigurationManager (where T = IISConfig) instance created in the SetUp/Init method
        /// </summary>
        private Mock<IConfigurationManager<IISConfig>> mockConfigManager;

        /// <summary>
        /// private field to hold a mocked IISConfigValidationService instance created in the SetUp/Init method
        /// </summary>
        private Mock<IConfigValidationService> mockValidation;

        /// <summary>
        /// private field to hold  a reference to the IISConfigurationService instance set in the SetUp/Init method
        /// </summary>
        private IISConfigurationService configService;

        /// <summary>
        /// Method to create the IISConfigurationService instance used by this test class
        /// </summary>
        [SetUp]
        public void Init()
        {
            this.mockConfigManager = new Mock<IConfigurationManager<IISConfig>>();
            this.mockConfigManager.Setup(c => c.GetSection(It.IsAny<string>())).Returns(TestData.TestIISConfig.Instance);

            this.mockValidation = new Mock<IConfigValidationService>();
            this.mockValidation.Setup(v => v.IsSatisfiedBy(TestData.TestIISConfig.Instance)).Returns(true);

            this.configService = GivenANewIISConfigurationServiceInstance(this.mockConfigManager, this.mockValidation);
        }

        /// <summary>
        /// Method to test that the ExecutablePath property returns the correct value
        /// </summary>
        [Test]
        public void WhenTheExecutablePathPropertyIsCalledTheExpectedValueIsReturned()
        {
            Assert.That(this.configService.ExecutablePath, Is.EqualTo(TestData.TestIISConfig.Instance.IIsExecutablePath));
        }

        /// <summary>
        /// Method to test that the ConfigurationPath property returns the correct value
        /// </summary>
        [Test]
        public void WhenTheConfigurationPathPropertyIsCalledTheExpectedValueIsReturned()
        {
            Assert.That(this.configService.ConfigurationPath, Is.EqualTo(TestData.TestIISConfig.Instance.IISConfigFilePath));
        }

        /// <summary>
        /// Method to test that the WebSite property returns the correct value
        /// </summary>
        [Test]
        public void WhenTheWebSitePropertyIsCalledTheExpectedValueIsReturned()
        {
            Assert.That(this.configService.WebSite, Is.EqualTo(TestData.TestIISConfig.Instance.WebSiteName));
        }

        /// <summary>
        /// Method to test that the AppPool property returns the correct value
        /// </summary>
        [Test]
        public void WhenTheAppPoolPropertyIsCalledTheExpectedValueIsReturned()
        {
            Assert.That(this.configService.AppPool, Is.EqualTo(TestData.TestIISConfig.Instance.AppPoolName));
        }

        /// <summary>
        /// Method to test that an exception is thrown when the IISConfig contains an invalid IIsExecutablePath
        /// </summary>
        [Test]
        public void WhenTheIISConfigHasAnInvalidIIsExecutablePathPropertyAnExceptionIsThrown()
        {
            var localConfig = new IISConfig
                { IIsExecutablePath = "zz:/RubishPath", IISConfigFilePath = "yy:/MoreRubbish" };
            this.mockConfigManager.Setup(c => c.GetSection(It.IsAny<string>())).Returns(localConfig);

            Assert.Throws(
                Is.TypeOf<Exception<IISConfigExceptionArgs>>().And.Message.Contains("zz:/RubishPath"),
                () =>
                    {
                        this.configService = new IISConfigurationService(
                            this.mockConfigManager.Object, this.mockValidation.Object);
                    });
        }

        /// <summary>
        /// Method to test that an exception is thrown when the IISConfig contains an invalid IISConfigFile path
        /// </summary>
        [Test]
        public void WhenTheIISConfigHasAnInvalidIISConfigFilePathPropertyAnExceptionIsThrown()
        {
            var localConfig = new IISConfig { IIsExecutablePath = "zz:/RubishPath", IISConfigFilePath = "yy:/MoreRubbish" };
            this.mockConfigManager.Setup(c => c.GetSection(It.IsAny<string>())).Returns(localConfig);

            Assert.Throws(
                Is.TypeOf<Exception<IISConfigExceptionArgs>>().And.Message.Contains("yy:/MoreRubbish"),
                () =>
                {
                    this.configService = new IISConfigurationService(
                        this.mockConfigManager.Object, this.mockValidation.Object);
                });
        }

        /// <summary>
        /// Method to test that an exception is thrown when the IsSatisfiedBy method of IConfigValidationService returns false whatever the reason
        /// </summary>
        [Test]
        public void WhenTheIConfigValidationServiceIsSatisfiedByMethodReturnsFalseAnExceptionIsThrown()
        {
            this.mockValidation.Setup(v => v.IsSatisfiedBy(It.IsAny<IISConfig>())).Returns(false);
            
            Assert.Throws(
                Is.TypeOf<Exception<IISConfigExceptionArgs>>()
                    .And.Message.Contains(TestData.TestIISConfig.Instance.IIsExecutablePath)
                    .And.Message.Contains(TestData.TestIISConfig.Instance.IISConfigFilePath),
                () =>
                {
                    this.configService = new IISConfigurationService(
                        this.mockConfigManager.Object, this.mockValidation.Object);
                });
        }

        /// <summary>
        /// Private helper method to create a new IISConfigurationService instance
        /// </summary>
        /// <param name="mockManager"> Mock IISConfigurationManager </param>
        /// <param name="mockValidator"> Mock IISConfigValidationService </param>
        /// <returns> New instance of IISConfigurationService  </returns>
        private static IISConfigurationService GivenANewIISConfigurationServiceInstance(
            IMock<IConfigurationManager<IISConfig>> mockManager, IMock<IConfigValidationService> mockValidator)
        {
            if (mockManager == null)
            {
                throw new ArgumentNullException("mockManager");
            }
            if (mockValidator == null)
            {
                throw new ArgumentNullException("mockValidator");
            }

            return new IISConfigurationService(mockManager.Object, mockValidator.Object);
        }
    }
}
