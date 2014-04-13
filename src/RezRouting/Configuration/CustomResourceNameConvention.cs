using System;
using System.Collections.Generic;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Implementation to enable customization of resource name via a function
    /// </summary>
    public class CustomResourceNameConvention : IResourceNameConvention
    {
        private readonly Func<IEnumerable<Type>,ResourceType, string> create;

        public CustomResourceNameConvention(Func<IEnumerable<Type>,ResourceType,string> create)
        {
            if (create == null) throw new ArgumentNullException("create");
            this.create = create;
        }

        public string GetResourceName(IEnumerable<Type> controllerTypes, ResourceType resourceType)
        {
            return create(controllerTypes, resourceType);
        }
    }
}