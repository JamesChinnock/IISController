
using System;

namespace IISExpressController.CustomExceptions
{
    /// <summary>
    /// Class that represents the Name and Id of Process object when an exception is thrown during Start or Kill. 
    /// </summary>
    [Serializable]
    public class ProcessExceptionArgs : ExceptionArgs
    {
        /// <summary>
        /// Private field to hold the readonly processName ctor parameter
        /// </summary>
        private readonly string _processName;

        /// <summary>
        /// Private field to hold the readonly arguments ctor parameter
        /// </summary>
        private readonly string _arguments;

        /// <summary>
        /// Private field to hold the readonly innerException ctor parameter
        /// </summary>
        private readonly Exception _innerException;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessExceptionArgs"/> class
        /// </summary>
        /// <param name="processName"> Name of the Process </param>
        /// <param name="arguments"> Id of the Process </param>
        /// <param name="innerException"> The inner exception </param>
        public ProcessExceptionArgs(string processName, string arguments, Exception innerException)
        {
            this._processName = processName;
            this._arguments = arguments;
            this._innerException = innerException;
        }

        /// <summary>
        /// Gets the name of the Process for which the exception has been thrown
        /// </summary>
        public string ProcessName
        {
            get
            {
                return this._processName;
            }
        }

        /// <summary>
        /// Gets the arguments for which the exception has been thrown
        /// </summary>
        public string Arguments 
        { 
            get
            {
                return this._arguments;
            }
        }

        /// <summary>
        /// Gets the inner exception
        /// </summary>
        public Exception InnerException
        {
            get
            {
                return this._innerException;
            }
        }

        /// <summary>
        /// Overridden Message property
        /// </summary>
        public override string Message
        {
            get
            {
                return string.Format("An exception was thrown by the Process: Name={0} Id={1}", this._processName, this._arguments);
            }
        }

    }
}
