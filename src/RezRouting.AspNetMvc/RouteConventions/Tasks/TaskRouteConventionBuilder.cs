using System.Collections.Generic;

namespace RezRouting.AspNetMvc.RouteConventions.Tasks
{
    /// <summary>
    /// Creates IRouteConventions used to map resource display and task routes
    /// </summary>
    public class TaskRouteConventionBuilder
    {
        public IEnumerable<IRouteConvention> Build()
        {
            var displayCollection = new ActionRouteConvention("Index", ResourceLevel.Collection, "Index", "GET", "");
            var editCollectionTask = new TaskRouteConvention("EditCollectionTask", ResourceLevel.Collection, "Edit", "GET");
            var handleCollectionTask = new TaskRouteConvention("HandleCollectionTask", ResourceLevel.Collection, "Handle", "POST");

            var displayCollectionItem = new ActionRouteConvention("Show", ResourceLevel.CollectionItem, "Show", "GET", "");
            var editCollectionItemTask = new TaskRouteConvention("EditCollectionItemTask", ResourceLevel.CollectionItem, "Edit", "GET");
            var handleCollectionItemTask = new TaskRouteConvention("HandleCollectionItemTask", ResourceLevel.CollectionItem, "Handle", "POST");

            var displaySingular = new ActionRouteConvention("Show", ResourceLevel.Singular, "Show", "GET", "");
            var editSingularTask = new TaskRouteConvention("EditSingularTask", ResourceLevel.Singular, "Edit", "GET");
            var handleSingularTask = new TaskRouteConvention("HandleCollectionTask", ResourceLevel.Singular, "Handle", "POST");

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