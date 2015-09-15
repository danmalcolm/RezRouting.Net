using System.Collections.Generic;
using RezRouting.Configuration.Builders;
using RezRouting.Configuration.Options;

namespace RezRouting.Configuration.Extensions
{
    /// <summary>
    /// An extension that contains a collection of inner extensions, which are applied
    /// in turn to the resource hierarchy. A convenience base class designed to support
    /// configurable sets of related extensions.
    /// </summary>
    public abstract class ExtensionScheme : IExtension
    {
        public void Extend(ResourceData root, ConfigurationContext context, ConfigurationOptions options)
        {
            var extensions = GetExtensions();
            foreach (var extension in extensions)
            {
                extension.Extend(root, context, options);
            }
        }

        protected abstract IEnumerable<IExtension> GetExtensions();
    }
}