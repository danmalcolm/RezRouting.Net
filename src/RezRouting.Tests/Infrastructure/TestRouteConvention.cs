using System;
using System.Collections.Generic;
using System.Linq;
using RezRouting.AspNetMvc;
using RezRouting.Configuration;
using RezRouting.Configuration.Conventions;
using RezRouting.Configuration.Options;
using RezRouting.Resources;

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
        private readonly Func<Resource, IResourceHandler, bool> filter;

        public TestRouteConvention(string name, string action, string httpMethod, string path, Func<Resource, IResourceHandler, bool> filter = null)
        {
            this.name = name;
            this.action = action;
            this.httpMethod = httpMethod;
            this.path = path;
            this.filter = filter ?? delegate { return true; };
        }

        public virtual IEnumerable<Route> Create(Resource resource, IEnumerable<IResourceHandler> handlers, UrlPathSettings urlPathSettings)
        {
            return handlers
                .OfType<MvcController>()
                .Where(x => filter(resource, x))
                .Select(x => new Route(name, new MvcAction(x.ControllerType, action), httpMethod, path, null));
        }
    }
}