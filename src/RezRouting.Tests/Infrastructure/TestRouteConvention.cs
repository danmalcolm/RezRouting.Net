using System;
using System.Collections.Generic;
using System.Linq;
using RezRouting.Options;

namespace RezRouting.Tests.Infrastructure
{
    /// <summary>
    /// Simple route type implementation for testing that sets up route for each 
    /// controller with the specified properties
    /// </summary>
    public class TestRouteConvention : IRouteConvention
    {
        private readonly string name;
        private readonly string action;
        private readonly string httpMethod;
        private readonly string path;
        private readonly Func<Resource, Type, bool> filter;

        public TestRouteConvention(string name, string action, string httpMethod, string path, Func<Resource, Type, bool> filter = null)
        {
            this.name = name;
            this.action = action;
            this.httpMethod = httpMethod;
            this.path = path;
            this.filter = filter ?? delegate { return true; };
        }

        public virtual IEnumerable<Route> Create(Resource resource, IEnumerable<Type> controllerTypes, UrlPathFormatter pathFormatter)
        {
            return controllerTypes
                .Where(type => filter(resource, type))
                .Select(x => new Route(name, x, action, httpMethod, path, null));
        }
    }
}