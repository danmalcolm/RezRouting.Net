using System;
using System.Collections.Generic;

namespace RezRouting.Configuration
{
    public interface IResourceNameConvention
    {
        string GetResourceName(IEnumerable<Type> controllerTypes, ResourceType resourceType);
    }
}