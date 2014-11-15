using System;

namespace RezRouting
{
    /// <summary>
    /// Exception thrown if route configuration is invalid
    /// </summary>
    public class RouteConfigurationException : Exception
    {
        public RouteConfigurationException()
        {
        }

        public RouteConfigurationException(string message) : base(message)
        {
        }

        public RouteConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}