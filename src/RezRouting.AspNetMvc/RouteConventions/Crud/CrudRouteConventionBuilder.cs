using System.Collections.Generic;
using RezRouting.Configuration;
using RezRouting.Resources;

namespace RezRouting.AspNetMvc.RouteConventions.Crud
{
    /// <summary>
    /// Creates IRouteConventions used to map basic Create, Read, Update
    /// and Delete actions for all types of resource
    /// </summary>
    public class CrudRouteConventionBuilder
    {
        /// <summary>
        /// Creates the individual route conventions
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IRouteConvention> Build()
        {
            var collectionIndex = new ActionRouteConvention("Index", ResourceLevel.Collection, "Index", "GET", "");
            var collectionNew = new ActionRouteConvention("New", ResourceLevel.Collection, "New", "GET", "new");
            var collectionCreate = new ActionRouteConvention("Create", ResourceLevel.Collection, "Create", "POST", "");
            
            var collectionItemShow = new ActionRouteConvention("Show", ResourceLevel.CollectionItem, "Show", "GET", "");
            var collectionItemEdit = new ActionRouteConvention("Edit", ResourceLevel.CollectionItem, "Edit", "GET", "edit");
            var collectionItemUpdate = new ActionRouteConvention("Update", ResourceLevel.CollectionItem, "Update", "PUT", "");
            var collectionItemDelete = new ActionRouteConvention("Delete", ResourceLevel.CollectionItem, "Delete", "DELETE", "");
            
            var singularNew = new ActionRouteConvention("New", ResourceLevel.Singular, "New", "GET", "new");
            var singularCreate = new ActionRouteConvention("Create", ResourceLevel.Singular, "Create", "POST", "");
            var singularShow = new ActionRouteConvention("Show", ResourceLevel.Singular, "Show", "GET", "");
            var singularEdit = new ActionRouteConvention("Edit", ResourceLevel.Singular, "Edit", "GET", "edit");
            var singularUpdate = new ActionRouteConvention("Update", ResourceLevel.Singular, "Update", "PUT", "");
            var singularDelete = new ActionRouteConvention("Delete", ResourceLevel.Singular, "Delete", "DELETE", "");
            
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