using System.Collections.Generic;

namespace RezRouting2.AspNetMvc.RouteTypes.Crud
{
    public static class CrudRouteTypes
    {
        static CrudRouteTypes()
        {
            CollectionIndex = new MvcActionRouteType("Index", ResourceLevel.Collection, "Index", "GET", "");
            CollectionNew = new MvcActionRouteType("New", ResourceLevel.Collection, "New", "GET", "new");
            CollectionCreate = new MvcActionRouteType("Create", ResourceLevel.Collection, "Create", "POST", "");
            CollectionItemShow = new MvcActionRouteType("Show", ResourceLevel.CollectionItem, "Show", "GET", "");
            CollectionItemEdit = new MvcActionRouteType("Edit", ResourceLevel.CollectionItem, "Edit", "GET", "edit");
            CollectionItemUpdate = new MvcActionRouteType("Update", ResourceLevel.CollectionItem, "Update", "PUT", "");
            CollectionItemDelete = new MvcActionRouteType("Delete", ResourceLevel.CollectionItem, "Delete", "DELETE", "");
            SingularNew = new MvcActionRouteType("New", ResourceLevel.Singular, "New", "GET", "new");
            SingularCreate = new MvcActionRouteType("Create", ResourceLevel.Singular, "Create", "POST", "");
            SingularShow = new MvcActionRouteType("Show", ResourceLevel.Singular, "Show", "GET", "");
            SingularEdit = new MvcActionRouteType("Edit", ResourceLevel.Singular, "Edit", "GET", "edit");
            SingularUpdate = new MvcActionRouteType("Update", ResourceLevel.Singular, "Update", "PUT", "");
            SingularDelete = new MvcActionRouteType("Delete", ResourceLevel.Singular, "Delete", "DELETE", "");
        }

        public static IEnumerable<IRouteType> All
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

        public static IRouteType CollectionIndex { get; private set; }

        public static IRouteType CollectionNew { get; private set; }

        public static IRouteType CollectionCreate { get; private set; }

        public static IRouteType CollectionItemShow { get; set; }

        public static IRouteType CollectionItemEdit { get; set; }

        public static IRouteType CollectionItemUpdate { get; set; }

        public static IRouteType CollectionItemDelete { get; set; }

        public static IRouteType SingularNew { get; private set; }

        public static IRouteType SingularCreate { get; private set; }

        public static IRouteType SingularShow { get; set; }

        public static IRouteType SingularEdit { get; set; }

        public static IRouteType SingularUpdate { get; set; }

        public static IRouteType SingularDelete { get; set; }
    }
}