
namespace IISExpressController.Services
{
    using IISExpressController.Configuration;

    /// <summary>
    /// Represents the ability to ensure that an IExecutableDetails derived object meets the sepcification
    /// </summary>
    public interface IConfigValidationService
    {
        /// <summary>
        /// Method to ensure the IExecutableDetails objects properties hold valid paths
        /// </summary>
        /// <param name="details"> the IISConfig containing the config settings </param>
        /// <returns> True if the exe and config paths are valid </returns>
        bool IsSatisfiedBy(IISConfig details);
    }
}