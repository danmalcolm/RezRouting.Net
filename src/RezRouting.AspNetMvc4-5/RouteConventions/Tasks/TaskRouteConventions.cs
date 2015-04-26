using System.Collections.Generic;
using RezRouting.Configuration.Conventions;
using RezRouting.Resources;

namespace RezRouting.AspNetMvc.RouteConventions.Tasks
{
    /// <summary>
    /// Creates IRouteConventions used to map resource display and task routes
    /// </summary>
    public class TaskRouteConventions : IRouteConventionScheme
    {
        public IEnumerable<IRouteConvention> GetConventions()
        {
            var displayCollection = new ActionRouteConvention("Index", ResourceType.Collection, "Index", "GET", "");
            var editCollectionTask = new TaskRouteConvention("EditCollectionTask", ResourceType.Collection, "Edit", "GET");
            var handleCollectionTask = new TaskRouteConvention("HandleCollectionTask", ResourceType.Collection, "Handle", "POST");

            var displayCollectionItem = new ActionRouteConvention("Show", ResourceType.CollectionItem, "Show", "GET", "");
            var editCollectionItemTask = new TaskRouteConvention("EditCollectionItemTask", ResourceType.CollectionItem, "Edit", "GET");
            var handleCollectionItemTask = new TaskRouteConvention("HandleCollectionItemTask", ResourceType.CollectionItem, "Handle", "POST");

            var displaySingular = new ActionRouteConvention("Show", ResourceType.Singular, "Show", "GET", "");
            var editSingularTask = new TaskRouteConvention("EditSingularTask", ResourceType.Singular, "Edit", "GET");
            var handleSingularTask = new TaskRouteConvention("HandleCollectionTask", ResourceType.Singular, "Handle", "POST");

            yield return displayCollection;
            yield return editCollectionTask;
            yield return handleCollectionTask;

            yield return displayCollectionItem;
            yield return editCollectionItemTask;
            yield return handleCollectionItemTask;

            yield return displaySingular;
            yield return editSingularTask;
            yield return handleSingularTask;
        }
    }
}