using System;
using RezRouting.Options;

namespace RezRouting.Tests.Infrastructure
{
    /// <summary>
    /// Simple route type implementation that sets up via a custom function
    /// </summary>
    public class TestRouteType : IRouteType
    {
        private readonly Action<Resource, Type, IConfigureRoute> configure;

        public TestRouteType(string name, Action<Resource,Type,IConfigureRoute> configure)
        {
            this.configure = configure;
            Name = name;
        }

        /// <summary>
        /// Name of RouteType, design for reference or use within application code
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Creates a Route model based on the resource and handler type, if they support
        /// the route specified by this RouteType
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="handlerType"></param>
        /// <param name="pathFormatter"></param>
        /// <returns></returns>
        public virtual Route BuildRoute(Resource resource, Type handlerType, UrlPathFormatter pathFormatter)
        {
            var builder = new RouteBuilder(handlerType);
            configure(resource, handlerType, builder);
            return builder.Build();
        } 
    }
}