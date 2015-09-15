using System.Collections.Generic;
using System.Linq;
using RezRouting.Configuration.Extensions;

namespace RezRouting.Tests.Infrastructure
{
    public class TestRouteConventionScheme : ExtensionScheme
    {
        private readonly IExtension[] extensions;

        public TestRouteConventionScheme(IEnumerable<IExtension> extensions)
        {
            this.extensions = extensions.ToArray();
        }

        protected override IEnumerable<IExtension> GetExtensions()
        {
            return extensions;
        }
    }
}