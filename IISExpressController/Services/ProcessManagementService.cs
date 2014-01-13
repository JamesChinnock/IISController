
using System;
using System.Diagnostics;

using IISExpressController.CustomExceptions;

namespace IISExpressController.Services
{
    /// <summary>
    /// Represents the ability to Create and Terminate a Windows Process
    /// </summary>
    public sealed class ProcessManagementService : IProcessManagementService
    {
        /// <summary>
        /// Private field to hold the processStartInfo ctor parameter
        /// </summary>
        private ProcessStartInfo _processStartInfo;

        /// <summary>
        /// Private field to hold the Process created by the Start() method
        /// </summary>
        private Process _process;

        /// <summary>
        /// Gets the Windows Process Id in which our process is running, the 0  value is chosen 
        /// to loosely emulate Windows which will set a Process's Id to 0 before the Process is removed
        /// by the scheduler.
        /// </summary>
        public int ProcessId
        {
            get
            {
                return this._process != null ? this._process.Id : 0;
            }
        }

        /// <summary>
        /// Gets the Windows Process Name in which our process is running
        /// </summary>
        public string ProcessName
        {
            get
            {
                return this._process != null ? this._process.ProcessName : string.Empty;
            }
        }

        /// <summary>
        /// Method to start the IISExpress instance
        /// </summary>
        /// <param name="processStartInfo"> ProcessStartInfo containing settings required to start the process  </param>
        public void Start(ProcessStartInfo processStartInfo)
        {
            if (processStartInfo == null)
            {
                throw new ArgumentNullException("processStartInfo");
            }

            this._processStartInfo = processStartInfo;

            try
            {
                this._process = Process.Start(this._processStartInfo);
                this._process.EnableRaisingEvents = true;
            }
            catch (Exception e)
            {
                throw new Exception<ProcessExceptionArgs>(
                    new ProcessExceptionArgs(this._processStartInfo.FileName, this._processStartInfo.Arguments, e));
            }            
        }
        
        /// <summary>
        /// Method to stop the IISExpress instance
        /// </summary>
        public void Stop()
        {
            if (this._process == null)
            {
                return;
            }

            this._process.Kill();
            this._process = null;
        }
    }
}
