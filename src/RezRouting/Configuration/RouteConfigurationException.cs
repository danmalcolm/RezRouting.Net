using System;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Exception thrown if route configuration is invalid
    /// </summary>
    public class RouteConfigurationException : Exception
    {
        /// <summary>
        /// Creates a RouteConfigurationException
        /// </summary>
        public RouteConfigurationException()
        {
        }

        /// <summary>
        /// Creates a RouteConfigurationException
        /// </summary>
        /// <param name="message"></param>
        public RouteConfigurationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a RouteConfigurationException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public RouteConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}