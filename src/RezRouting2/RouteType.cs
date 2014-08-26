using System;

namespace RezRouting2
{
    public class RouteType
    {
        private readonly Action<Resource, Type, RouteBuilder> configure;

        public RouteType(string name, Action<Resource,Type,RouteBuilder> configure)
        {
            this.configure = configure;
            Name = name;
        }

        public string Name { get; private set; }

        public Route BuildRoute(Resource resource, Type controllerType)
        {
            var builder = new RouteBuilder();
            configure(resource, controllerType, builder);
            return builder.Build();
        } 
    }
}