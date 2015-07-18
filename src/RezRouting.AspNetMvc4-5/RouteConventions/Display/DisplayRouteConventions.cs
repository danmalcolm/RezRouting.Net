using System.Collections.Generic;
using RezRouting.Configuration.Conventions;
using RezRouting.Resources;

namespace RezRouting.AspNetMvc.RouteConventions.Display
{
    /// <summary>
    /// Creates IRouteConvention collection to map resource display routes
    /// </summary>
    public class DisplayRouteConventions : IRouteConventionScheme
    {
        public IEnumerable<IRouteConvention> GetConventions()
        {
            var displayCollection = new ActionRouteConvention("Index", ResourceType.Collection, "Index", "GET", "");
            
            var displayCollectionItem = new ActionRouteConvention("Show", ResourceType.CollectionItem, "Show", "GET", "");
            
            var displaySingular = new ActionRouteConvention("Show", ResourceType.Singular, "Show", "GET", "");
            
            yield return displayCollection;
            
            yield return displayCollectionItem;
            
            yield return displaySingular;
        }
    }
}