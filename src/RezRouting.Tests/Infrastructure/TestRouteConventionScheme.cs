using System.Collections.Generic;
using RezRouting.Configuration.Conventions;

namespace RezRouting.Tests.Infrastructure
{
    public class TestRouteConventionScheme : IRouteConventionScheme
    {
        private readonly IRouteConvention[] conventions;

        public TestRouteConventionScheme(params IRouteConvention[] conventions)
        {
            this.conventions = conventions;
        }

        public IEnumerable<IRouteConvention> GetConventions()
        {
            return conventions;
        }
    }
}