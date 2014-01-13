
using System;

namespace IISExpressController.Services
{
    using System.Diagnostics;

    /// <summary>
    /// Represents the ability to Create and Terminate a Windows Process
    /// </summary>
    public interface IProcessManagementService
    {
        /// <summary>
        /// Gets the Windows Process Id in which our process is running
        /// </summary>
        int ProcessId { get; }

        /// <summary>
        /// Gets the Windows Process Name in which our process is running
        /// </summary>
        string ProcessName { get; }

        /// <summary>
        /// Method to start the IISExpress instance
        /// </summary>
        /// <param name="processStartInfo"> ProcessStartInfo containing settings required to start the process  </param>
        void Start(ProcessStartInfo processStartInfo);

        /// <summary>
        /// Method to start the IISExpress instance
        /// </summary>
        void Stop();
    }
}