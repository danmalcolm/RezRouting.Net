using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Common
{
    public class RouteDataHelper
    {
        /// <summary>
        /// Gets entity id from route values. Note that RezRouting by default uses the
        /// key "id" to identify an item resource. If the route applies to a child resource, 
        /// then the key includes the name of the resource type, e.g. productId
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        public static int? GetEntityId(Type entityType, RouteValueDictionary routeValues)
        {
            var keys = GetPossibleIdKeys(entityType);
            object value = keys.Where(key => routeValues.ContainsKey(key))
                .Select(key => routeValues[key])
                .FirstOrDefault();
            int id;
            if (value != null && int.TryParse(value.ToString(), out id))
            {
                return id;
            }
            return null;
        }

        private static IEnumerable<string> GetPossibleIdKeys(Type entityType)
        {
            // Route values are case-insensitive
            yield return entityType.Name + "Id";
            yield return "Id";
        }
    }
}