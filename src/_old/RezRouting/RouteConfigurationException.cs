using System;
using System.Runtime.Serialization;

namespace RezRouting
{
    [Serializable]
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

        protected RouteConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}