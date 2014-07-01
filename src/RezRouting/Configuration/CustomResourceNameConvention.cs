using System;
using System.Collections.Generic;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Implementation to enable customization of resource name via a function
    /// </summary>
    internal class CustomResourceNameConvention : IResourceNameConvention
    {
        private readonly Func<IEnumerable<Type>, ResourceType, ResourceName> create;

        public CustomResourceNameConvention(Func<IEnumerable<Type>,ResourceType,ResourceName> create)
        {
            if (create == null) throw new ArgumentNullException("create");
            this.create = create;
        }

        public ResourceName GetResourceName(IEnumerable<Type> controllerTypes, ResourceType resourceType)
        {
            return create(controllerTypes, resourceType);
        }
    }
}