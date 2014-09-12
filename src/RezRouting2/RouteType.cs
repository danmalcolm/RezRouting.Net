using System;

namespace RezRouting2
{
    /// <summary>
    /// Simple route type implementation that sets up via a custom function
    /// </summary>
    public class RouteType : IRouteType
    {
        private readonly Action<Resource, Type, IConfigureRoute> configure;

        public RouteType(string name, Action<Resource,Type,IConfigureRoute> configure)
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
        /// <returns></returns>
        public virtual Route BuildRoute(Resource resource, Type handlerType)
        {
            var builder = new RouteBuilder(handlerType);
            configure(resource, handlerType, builder);
            return builder.Build();
        } 
    }
}