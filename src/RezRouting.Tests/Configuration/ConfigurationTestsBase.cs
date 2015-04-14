using System;
using System.Collections.Generic;
using System.Linq;
using RezRouting.Configuration;
using RezRouting.Resources;
using RezRouting.Tests.Infrastructure;

namespace RezRouting.Tests.Configuration
{
    public abstract class ConfigurationTestsBase
    {
        /// <summary>
        /// Configures resources using specified action and returns a dictionary of the resource
        /// hierarchy, keyed by full resource name
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        protected Dictionary<string, Resource> BuildResources(Action<IRootResourceBuilder> configure)
        {
            var root = RootResourceBuilder.Create("");
            configure(root);
            var resource = root.Build();
            return resource.Expand().ToDictionary(x => x.FullName);
        }
    }
}