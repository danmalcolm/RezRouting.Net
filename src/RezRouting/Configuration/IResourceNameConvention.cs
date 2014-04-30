using System;
using System.Collections.Generic;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Formats a resource's name
    /// </summary>
    public interface IResourceNameConvention
    {
        string GetResourceName(IEnumerable<Type> controllerTypes, ResourceType resourceType);
    }
}