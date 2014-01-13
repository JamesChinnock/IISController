
using System.Diagnostics;
using System.Linq;

using IISExpressController;
using IISExpressController.Configuration;
using IISExpressController.Services;

using NUnit.Framework;

namespace IISExpressControllerTests.UnitTests
{
    /// <summary>
    /// Class to encapsulate IISExpress specific unit tests
    /// </summary>
    [TestFixture]
    public class IISExpressUnitTests 
    {
        /// <summary>
        /// private field to contain a reference to the IISExpress instance created in the SetUp/Init method
        /// </summary>
        private IISExpress iisExpress;

        /// <summary>
        /// Method to create the IISExpress instance used by this test class
        /// </summary>
        [SetUp]
        public void Init()
        {
            this.iisExpress = GivenANewIISExpressInstance();
        }

        /// <summary>
        /// Method to clear up any processes and data created by the tests
        /// </summary>
        [TearDown]
        public void CleanUp()
        {
            if (Process.GetProcesses().Count(s => s.Id == this.iisExpress.IISProcessId) == 0)
            {
                return;
            }

            this.iisExpress.Stop();
            this.iisExpress = null;
        }

        /// <summary>
        /// Method to test the process creation following a call to the Start method
        /// </summary>
        [Test]
        public void WhenTheStartMethodIsInvokedTheProcessIdReturnedExistsInTheResourcesOfTheLocalComputer()
        {
            this.iisExpress.Start();
            Assert.That(Process.GetProcesses().Where(s => s.Id == this.iisExpress.IISProcessId), Is.Not.Null);
        }

        /// <summary>
        /// Method to test the process termination, the assertion follows the windows Exited event
        /// </summary>
        [Test]
        public void WhenTheStopMethodIsInvokedTheProcessIsKilledAndTheIdIsRemovedFromTheResourcesOfTheLocalComputer()
        {
            this.iisExpress.Start();
            Assert.That(Process.GetProcesses().Where(s => s.Id == this.iisExpress.IISProcessId), Is.Not.Null);

            this.iisExpress.Stop();

            var procCount = Process.GetProcesses().Count(p => p.Id == this.iisExpress.IISProcessId && p.ProcessName != "Idle");
            Assert.That(procCount, Is.EqualTo(0));
        }
        
        /// <summary>
        /// Method to check that when an IISEXpress is first created (without Start being invoked) then IISProcessId is zero
        /// </summary>
        [Test]
        public void WhenAnIIsExpressTypeIsFirstInstantiatedTheIISProcessIDIsZero()
        {
            Assert.That(this.iisExpress.IISProcessId, Is.EqualTo(0));
        }

        /// <summary>
        /// Method to check that when Start is invoked on an IISEXpress then IISProcessId is non zero
        /// </summary>
        [Test]
        public void WhenStartIsInvokedTheIISProcessIDIsNotZero()
        {
            this.iisExpress.Start();
            Assert.That(this.iisExpress.IISProcessId, Is.Not.EqualTo(0));
        }

        /// <summary>
        /// Method to check that when Start is invoked on an IISEXpress then IISProcessId is non zero
        /// </summary>
        [Test]
        public void WhenStopIsInvokedFollowingASuccesfullStartTheIISProcessIDIsSetToZeroAgain()
        {
            this.iisExpress.Start();
            this.iisExpress.Stop();
            Assert.That(this.iisExpress.IISProcessId, Is.EqualTo(0));
        }

        /// <summary>
        /// Private helper method to create a new IISExpress instance
        /// </summary>
        /// <returns> New instance of IISExpress </returns>
        private static IISExpress GivenANewIISExpressInstance()
        {
            var configManager = new IISConfigurationManager<IISConfig>();
            var validation = new IISConfigValidationService();
            var configuration = new IISConfigurationService(configManager, validation);

            var procManagerService = new ProcessManagementService();

            return new IISExpress(configuration, procManagerService);
        }
    }
}
