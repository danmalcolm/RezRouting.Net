using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RezRouting2.Utility;

namespace RezRouting2.AspNetMvc.RouteTypes.Standard
{
    public static class CrudRouteTypes
    {
        static CrudRouteTypes()
        {
            CollectionIndex = MvcRouteTypeHelper.ActionRouteType("Index", ResourceLevel.Collection, "Index", "GET", "");
            CollectionNew = MvcRouteTypeHelper.ActionRouteType("New", ResourceLevel.Collection, "New", "GET", "new");
            CollectionCreate = MvcRouteTypeHelper.ActionRouteType("Create", ResourceLevel.Collection, "Create", "POST", "");
            CollectionItemShow = MvcRouteTypeHelper.ActionRouteType("Show", ResourceLevel.CollectionItem, "Show", "GET", "");
            CollectionItemEdit = MvcRouteTypeHelper.ActionRouteType("Edit", ResourceLevel.CollectionItem, "Edit", "GET", "edit");
            CollectionItemUpdate = MvcRouteTypeHelper.ActionRouteType("Update", ResourceLevel.CollectionItem, "Update", "PUT", "");
            CollectionItemDelete = MvcRouteTypeHelper.ActionRouteType("Delete", ResourceLevel.CollectionItem, "Delete", "DELETE", "");
            SingularNew = MvcRouteTypeHelper.ActionRouteType("New", ResourceLevel.Singular, "New", "GET", "new");
            SingularCreate = MvcRouteTypeHelper.ActionRouteType("Create", ResourceLevel.Singular, "Create", "POST", "");
            SingularShow = MvcRouteTypeHelper.ActionRouteType("Show", ResourceLevel.Singular, "Show", "GET", "");
            SingularEdit = MvcRouteTypeHelper.ActionRouteType("Edit", ResourceLevel.Singular, "Edit", "GET", "edit");
            SingularUpdate = MvcRouteTypeHelper.ActionRouteType("Update", ResourceLevel.Singular, "Update", "PUT", "");
            SingularDelete = MvcRouteTypeHelper.ActionRouteType("Delete", ResourceLevel.Singular, "Delete", "DELETE", "");
        }

        public static IEnumerable<RouteType> All
        {
            get
            {
                yield return CollectionIndex;
                yield return CollectionNew;
                yield return CollectionCreate;
                yield return CollectionItemShow;
                yield return CollectionItemEdit;
                yield return CollectionItemUpdate;
                yield return CollectionItemDelete;
                yield return SingularNew;
                yield return SingularCreate;
                yield return SingularShow;
                yield return SingularEdit;
                yield return SingularUpdate;
                yield return SingularDelete;
            }
        }

        public static RouteType CollectionIndex { get; private set; }

        public static RouteType CollectionNew { get; private set; }

        public static RouteType CollectionCreate { get; private set; }

        public static RouteType CollectionItemShow { get; set; }

        public static RouteType CollectionItemEdit { get; set; }

        public static RouteType CollectionItemUpdate { get; set; }

        public static RouteType CollectionItemDelete { get; set; }

        public static RouteType SingularNew { get; private set; }

        public static RouteType SingularCreate { get; private set; }

        public static RouteType SingularShow { get; set; }

        public static RouteType SingularEdit { get; set; }

        public static RouteType SingularUpdate { get; set; }

        public static RouteType SingularDelete { get; set; }

    }
}