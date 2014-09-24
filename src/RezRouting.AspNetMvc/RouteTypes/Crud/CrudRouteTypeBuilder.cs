using System.Collections.Generic;

namespace RezRouting.AspNetMvc.RouteTypes.Crud
{
    /// <summary>
    /// Creates RouteTypes used to map resources and basic Create, Read, Update
    /// and Delete actions
    /// </summary>
    public class CrudRouteTypeBuilder
    {
        public IEnumerable<IRouteType> Build()
        {
            var collectionIndex = new ActionRouteType("Index", ResourceLevel.Collection, "Index", "GET", "");
            var collectionNew = new ActionRouteType("New", ResourceLevel.Collection, "New", "GET", "new");
            var collectionCreate = new ActionRouteType("Create", ResourceLevel.Collection, "Create", "POST", "");
            
            var collectionItemShow = new ActionRouteType("Show", ResourceLevel.CollectionItem, "Show", "GET", "");
            var collectionItemEdit = new ActionRouteType("Edit", ResourceLevel.CollectionItem, "Edit", "GET", "edit");
            var collectionItemUpdate = new ActionRouteType("Update", ResourceLevel.CollectionItem, "Update", "PUT", "");
            var collectionItemDelete = new ActionRouteType("Delete", ResourceLevel.CollectionItem, "Delete", "DELETE", "");
            
            var singularNew = new ActionRouteType("New", ResourceLevel.Singular, "New", "GET", "new");
            var singularCreate = new ActionRouteType("Create", ResourceLevel.Singular, "Create", "POST", "");
            var singularShow = new ActionRouteType("Show", ResourceLevel.Singular, "Show", "GET", "");
            var singularEdit = new ActionRouteType("Edit", ResourceLevel.Singular, "Edit", "GET", "edit");
            var singularUpdate = new ActionRouteType("Update", ResourceLevel.Singular, "Update", "PUT", "");
            var singularDelete = new ActionRouteType("Delete", ResourceLevel.Singular, "Delete", "DELETE", "");
            
            yield return collectionIndex;
            yield return collectionNew;
            yield return collectionCreate;
            yield return collectionItemShow;
            yield return collectionItemEdit;
            yield return collectionItemUpdate;
            yield return collectionItemDelete;
            yield return singularNew;
            yield return singularCreate;
            yield return singularShow;
            yield return singularEdit;
            yield return singularUpdate;
            yield return singularDelete;
        }
    }
}