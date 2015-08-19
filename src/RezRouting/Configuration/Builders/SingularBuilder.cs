using System;
using RezRouting.Configuration.Options;
using RezRouting.Utility;

namespace RezRouting.Configuration.Builders
{
    /// <summary>
    /// Configures and creates a singular Resource
    /// </summary>
    public class SingularBuilder : ResourceBuilderBase<SingularData>, ISingularConfigurator
    {
        /// <summary>
        /// Creates a new SingularBuilder
        /// </summary>
        /// <param name="name"></param>
        public SingularBuilder(ResourceData parentData, string name) : base(parentData, name)
        {
            
        }

        /// <inheritdoc />
        public void UrlPath(string path)
        {
            Data.UrlPath = path;
        }
    }
}