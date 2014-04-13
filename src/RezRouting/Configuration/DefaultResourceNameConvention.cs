using System;
using System.Collections.Generic;
using System.Linq;
using RezRouting.Routing;
using RezRouting.Utility;

namespace RezRouting.Configuration
{
    public class DefaultResourceNameConvention : IResourceNameConvention
    {
        public virtual string GetResourceName(IEnumerable<Type> controllerTypes, ResourceType resourceType)
        {
            var names = controllerTypes.Select(ControllerNameFormatter.TrimControllerFromTypeName).ToArray();
            
            if (names.Length == 0)
                throw new ArgumentException("At least one type is expected", "controllerTypes");

            var name = GetCommonStart(names);
            if (name != "")
            {
                name = resourceType == ResourceType.Collection
                    ? name.Pluralize(Plurality.CouldBeEither)
                    : name.Singularize(Plurality.CouldBeEither);
            }
            return name;
        }

        private static string GetCommonStart(string[] names)
        {
            var normalized = names.Select(x => x.ToLowerInvariant()).ToArray();
            var first = normalized.First();
            var others = normalized.Skip(1).ToArray();
            int maxIndex = normalized.Select(n => n.Length).Min();
            int index = 0;
            while (index < maxIndex && others.All(other => first[index] == other[index]))
            {
                index++;
            }

            string name = names.First().Substring(0, index);
            return name;
        }
    }
}