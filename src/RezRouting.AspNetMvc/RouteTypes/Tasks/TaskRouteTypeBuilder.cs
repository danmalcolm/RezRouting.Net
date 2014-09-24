using System.Collections.Generic;

namespace RezRouting.AspNetMvc.RouteTypes.Tasks
{
    /// <summary>
    /// Creates RouteTypes used to map resources and task routes
    /// </summary>
    public class TaskRouteTypeBuilder
    {
        public IEnumerable<IRouteType> Build()
        {
            var displayCollection = new ActionRouteType("Index", ResourceLevel.Collection, "Index", "GET", "");
            var editCollectionTask = new TaskRouteType("EditCollectionTask", ResourceLevel.Collection, "Edit", "GET");
            var handleCollectionTask = new TaskRouteType("HandleCollectionTask", ResourceLevel.Collection, "Handle", "POST");

            var displayCollectionItem = new ActionRouteType("Show", ResourceLevel.CollectionItem, "Show", "GET", "");
            var editCollectionItemTask = new TaskRouteType("EditCollectionItemTask", ResourceLevel.CollectionItem, "Edit", "GET");
            var handleCollectionItemTask = new TaskRouteType("HandleCollectionItemTask", ResourceLevel.CollectionItem, "Handle", "POST");

            var displaySingular = new ActionRouteType("Show", ResourceLevel.Singular, "Show", "GET", "");
            var editSingularTask = new TaskRouteType("EditSingularTask", ResourceLevel.Singular, "Edit", "GET");
            var handleSingularTask = new TaskRouteType("HandleCollectionTask", ResourceLevel.Singular, "Handle", "POST");

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