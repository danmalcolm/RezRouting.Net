using System;
using System.Collections.Generic;
using System.Linq;
using RezRouting.AspNetMvc.Utility;
using RezRouting.Utility;

namespace RezRouting.AspNetMvc.ControllerDiscovery
{
    /// <summary>
    /// Contains a collection of controller types discovered within one or more 
    /// assemblies / root namespaces. Each item has a key, based on the subnamespace 
    /// below the root controller namespace, e.g. MyApp.Controllers.Products
    /// would have the key "Products".
    /// </summary>
    public class ControllerIndex
    {
        public static ControllerIndex Create(IEnumerable<ControllerRoot> roots)
        {
            var items = from root in roots
                from controllerType in root.Assembly.GetExportedTypes()
                where MvcControllerHelper.IsController(controllerType)
                where root.Includes(controllerType)
                let controllerNs = controllerType.Namespace
                let keyStartIndex = Math.Min(controllerNs.Length, root.Namespace.Length + 1)
                let key = controllerNs.Substring(keyStartIndex)
                orderby key
                select new ControllerIndexItem(key, controllerType);
            return new ControllerIndex(items);
        }

        private ControllerIndex(IEnumerable<ControllerIndexItem> items)
        {
            this.Items = items.ToReadOnlyList();
        }

        /// <summary>
        /// Gets the collection of items
        /// </summary>
        public IList<ControllerIndexItem> Items { get; private set; }
    }
}