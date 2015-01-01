using System.Collections.Generic;
using RezRouting.Configuration;
using RezRouting.Resources;

namespace RezRouting.AspNetMvc.RouteConventions.Crud
{
    /// <summary>
    /// Creates IRouteConventions used to map basic Create, Read, Update
    /// and Delete actions for all types of resource
    /// </summary>
    public class CrudRouteConventions : IRouteConventionScheme
    {
        /// <summary>
        /// Creates the individual route conventions
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IRouteConvention> GetConventions()
        {
            var collectionIndex = new ActionRouteConvention("Index", ResourceType.Collection, "Index", "GET", "");
            var collectionNew = new ActionRouteConvention("New", ResourceType.Collection, "New", "GET", "new");
            var collectionCreate = new ActionRouteConvention("Create", ResourceType.Collection, "Create", "POST", "");
            
            var collectionItemShow = new ActionRouteConvention("Show", ResourceType.CollectionItem, "Show", "GET", "");
            var collectionItemEdit = new ActionRouteConvention("Edit", ResourceType.CollectionItem, "Edit", "GET", "edit");
            var collectionItemUpdate = new ActionRouteConvention("Update", ResourceType.CollectionItem, "Update", "PUT", "");
            var collectionItemDelete = new ActionRouteConvention("Delete", ResourceType.CollectionItem, "Delete", "DELETE", "");
            
            var singularNew = new ActionRouteConvention("New", ResourceType.Singular, "New", "GET", "new");
            var singularCreate = new ActionRouteConvention("Create", ResourceType.Singular, "Create", "POST", "");
            var singularShow = new ActionRouteConvention("Show", ResourceType.Singular, "Show", "GET", "");
            var singularEdit = new ActionRouteConvention("Edit", ResourceType.Singular, "Edit", "GET", "edit");
            var singularUpdate = new ActionRouteConvention("Update", ResourceType.Singular, "Update", "PUT", "");
            var singularDelete = new ActionRouteConvention("Delete", ResourceType.Singular, "Delete", "DELETE", "");
            
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