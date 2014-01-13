
using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace IISExpressController.CustomExceptions
{
    /// <summary>
    /// Represents the ability to create generic serializable exception
    /// </summary>
    /// <typeparam name="TExceptionArgs"> The type of the exception </typeparam>
    [Serializable]
    public class Exception<TExceptionArgs> : Exception where TExceptionArgs : ExceptionArgs
    {
        /// <summary>
        /// For (de)serialization
        /// </summary>
        private const string Args = "Args";

        /// <summary>
        /// Private field to hold the TExceptionArgs generic type parameter
        /// </summary>
        private readonly TExceptionArgs _args;
 
        /// <summary>
        /// Initializes a new instance of the <see cref="TExceptionArgs"/> class
        /// </summary>
        /// <param name="message"> Exception message </param>
        /// <param name="innerException"> Any InnerException that may exist </param>
        public Exception(string message = null, Exception innerException = null) : this(null, message, innerException)
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="TExceptionArgs"/> class
        /// </summary>
        /// <param name="args">   </param>
        /// <param name="message"> Exception message </param>
        /// <param name="innerException"> Any InnerException that may exist </param>
        public Exception(TExceptionArgs args, string message = null, Exception innerException = null) : base(message, innerException)
        {
            this._args = args;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TExceptionArgs"/> class
        /// This constructor is for deserialization; since the class is sealed, the constructor is private
        /// </summary>
        /// <param name="info"> SerializationInfo </param>
        /// <param name="context"> StreamingContext </param>
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        private Exception(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this._args = (TExceptionArgs)info.GetValue(Args, typeof(TExceptionArgs));
        }

        /// <summary>
        /// Gets a message that describes the current exception
        /// </summary>
        public override string Message
        {
            get
            {
                var baseMsg = base.Message;
                return (this._args == null) ? baseMsg : baseMsg + " (" + this._args.Message + ")";
            }
        }

        /// <summary>
        /// Overriden Exception method that sets the SerializationInfo with information about the exception.
        /// This method is for serialization; Public because of the ISerializable interface
        /// </summary>
        /// <param name="info"> SerializationInfo </param>
        /// <param name="context"> StreamingContext </param>
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(Args, this._args);

            base.GetObjectData(info, context);
        }
        
        /// <summary>
        /// Overridden Equals method
        /// </summary>
        /// <param name="obj"> Object to which equality should be determined </param>
        /// <returns> True if equal, else false </returns>
        public override bool Equals(object obj)
        {
            var other = obj as Exception<TExceptionArgs>;
            if (other == null)
            {
                return false;
            }

            return object.Equals(this._args, other._args) && base.Equals(obj);
        }

        /// <summary>
        /// Overridden GetHashCode method to due to overridden Equals method
        /// </summary>
        /// <returns> Integer hashcode </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public abstract class ExceptionArgs
    {
        public virtual string Message
        {
            get
            {
                return string.Empty;
            }
        }
    }
}
