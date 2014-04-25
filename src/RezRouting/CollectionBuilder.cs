using RezRouting.Configuration;

namespace RezRouting
{
    /// <summary>
    /// Configures routes for the actions available on a collection of resources
    /// </summary>
    public class CollectionBuilder : ResourceBuilder
    {
        protected override ResourceType ResourceType
        {
            get { return ResourceType.Collection;  }
        }

        /// <summary>
        /// Specifies the name of the id value parameter used to specify an item in this 
        /// collection within the route URL, e.g. orders/{id}. This defaults to "id", e.g. orders/{id} but
        /// can be overriden.
        /// </summary>
        /// <param name="name"></param>
        public void IdName(string name)
        {
            CustomIdName(name);
        }

        /// <summary>
        /// Specifies the name of the id value parameter used to specify an item in this 
        /// collection within the route URL of a child resource, e.g. product/{productId}/reviews/{id}.
        /// This controls the name of the route value made available to the controller action. This 
        /// defaults to the singular name of the resource + "Id" but can be overriden.
        /// </summary>
        /// <param name="name"></param>
        public void IdNameAsAncestor(string name)
        {
            CustomIdNameAsAncestor(name);
        }
    }
}