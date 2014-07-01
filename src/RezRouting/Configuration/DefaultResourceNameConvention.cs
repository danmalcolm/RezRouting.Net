using System;
using System.Collections.Generic;
using System.Linq;
using RezRouting.Routing;
using RezRouting.Utility;

namespace RezRouting.Configuration
{
    public class DefaultResourceNameConvention : IResourceNameConvention
    {
        public virtual ResourceName GetResourceName(IEnumerable<Type> controllerTypes, ResourceType resourceType)
        {
            var names = controllerTypes.Select(RouteValueHelper.TrimControllerFromTypeName).ToArray();
            
            if (names.Length == 0)
                throw new ArgumentException("At least one type is expected", "controllerTypes");

            var name = GetCommonStartOrFirst(names);
            if (name != "")
            {
                string singular = null;
                string plural = null;
                if (resourceType == ResourceType.Collection)
                {
                    plural = name.Pluralize(Plurality.CouldBeEither);
                }
                else
                {
                    singular = name.Singularize(Plurality.CouldBeEither);
                }
                return new ResourceName(singular, plural);
            }
            return null;
        }

        private static string GetCommonStartOrFirst(string[] names)
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
            string name = (index > 0) ? names.First().Substring(0, index) : names.First();
            return name;
        }
    }
}