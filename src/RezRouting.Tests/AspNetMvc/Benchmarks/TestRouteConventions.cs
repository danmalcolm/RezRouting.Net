using System.Collections.Generic;
using RezRouting.Configuration;
using RezRouting.Configuration.Options;
using RezRouting.Resources;

namespace RezRouting.Tests.AspNetMvc.Benchmarks
{
    public class TestRouteConventions : IRouteConventionScheme
    {
        public IEnumerable<IRouteConvention> GetConventions()
        {
            throw new System.NotImplementedException();
        }
    }
}