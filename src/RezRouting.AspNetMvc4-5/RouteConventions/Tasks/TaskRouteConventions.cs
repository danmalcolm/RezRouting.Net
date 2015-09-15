using System.Collections.Generic;
using RezRouting.Configuration.Extensions;
using RezRouting.Resources;

namespace RezRouting.AspNetMvc.RouteConventions.Tasks
{
    /// <summary>
    /// Creates IRouteConventions used to map resource display and task routes
    /// </summary>
    public class TaskRouteConventions : ExtensionScheme
    {
        protected override IEnumerable<IExtension> GetExtensions()
        {
            var editCollectionTask = new TaskRouteConvention("EditCollectionTask", ResourceType.Collection, "Edit", "GET");
            var handleCollectionTask = new TaskRouteConvention("HandleCollectionTask", ResourceType.Collection, "Handle", "POST");

            var editCollectionItemTask = new TaskRouteConvention("EditCollectionItemTask", ResourceType.CollectionItem, "Edit", "GET");
            var handleCollectionItemTask = new TaskRouteConvention("HandleCollectionItemTask", ResourceType.CollectionItem, "Handle", "POST");

            var editSingularTask = new TaskRouteConvention("EditSingularTask", ResourceType.Singular, "Edit", "GET");
            var handleSingularTask = new TaskRouteConvention("HandleCollectionTask", ResourceType.Singular, "Handle", "POST");

            yield return editCollectionTask;
            yield return handleCollectionTask;

            yield return editCollectionItemTask;
            yield return handleCollectionItemTask;

            yield return editSingularTask;
            yield return handleSingularTask;
        }
    }
}