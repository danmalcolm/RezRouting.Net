using System.Collections.Generic;

namespace RezRouting.AspNetMvc.RouteTypes.Tasks
{
    public class TaskRouteScheme
    {
        private readonly IRouteType displayCollection;
        private readonly IRouteType editCollectionTask;
        private readonly IRouteType handleCollectionTask;

        private readonly IRouteType displayCollectionItem;
        private readonly IRouteType editCollectionItemTask;
        private readonly IRouteType handleCollectionItemTask;

        private readonly IRouteType displaySingular;
        private readonly IRouteType editSingularTask;
        private readonly IRouteType handleSingularTask;

        public TaskRouteScheme()
        {
            displayCollection = new ActionRouteType("Index", ResourceLevel.Collection, "Index", "GET", "");
            editCollectionTask = new TaskRouteType("EditCollectionTask", ResourceLevel.Collection, "Edit", "GET");
            handleCollectionTask = new TaskRouteType("HandleCollectionTask", ResourceLevel.Collection, "Handle", "POST");

            displayCollectionItem = new ActionRouteType("Show", ResourceLevel.CollectionItem, "Show", "GET", "");
            editCollectionItemTask = new TaskRouteType("EditCollectionItemTask", ResourceLevel.CollectionItem, "Edit", "GET");
            handleCollectionItemTask = new TaskRouteType("HandleCollectionItemTask", ResourceLevel.CollectionItem, "Handle", "POST");

            displaySingular = new ActionRouteType("Show", ResourceLevel.Singular, "Show", "GET", "");
            editSingularTask = new TaskRouteType("EditSingularTask", ResourceLevel.Singular, "Edit", "GET");
            handleSingularTask = new TaskRouteType("HandleCollectionTask", ResourceLevel.Singular, "Handle", "POST");
        }

        public IEnumerable<IRouteType> RouteTypes
        {
            get
            {
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
}