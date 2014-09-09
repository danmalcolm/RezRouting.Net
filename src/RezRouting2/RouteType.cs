using System;

namespace RezRouting2
{
    public class RouteType
    {
        private readonly Action<Resource, Type, IConfigureRoute> configure;

        public RouteType(string name, Action<Resource,Type,IConfigureRoute> configure)
        {
            this.configure = configure;
            Name = name;
        }

        public string Name { get; private set; }

        public Route BuildRoute(Resource resource, Type controllerType)
        {
            var builder = new RouteBuilder(controllerType);
            configure(resource, controllerType, builder);
            return builder.Build();
        } 
    }
}