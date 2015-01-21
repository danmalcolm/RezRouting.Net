using System;
using System.Web.Routing;
using RezRouting.Configuration.Builders;
using RezRouting.Configuration.Options;
using RezRouting.Resources;

namespace RezRouting.AspNetMvc
{
    /// <summary>
    /// Extension methods for mapping MVC routes based on resources and routes
    /// configured by a ResourceGraphBuilder
    /// </summary>
    public static class ResourceGraphBuilderExtensions
    {
        /// <summary>
        /// <para>
        /// Creates ASP.Net MVC routes within the specified RouteCollection based on the resources 
        /// and routes configured by this ResourceGraphBuilder. Optionally, a custom action can be specified,
        /// which provides access to the ResourceGraphModel created by the ResourceGraphBuilder.
        /// </para> 
        /// <para>
        /// This is a convenience method that is equivalent to the following: <c> new MvcRouteCreator().CreateRoutes(builder.Build(), routes, area)</c>.
        /// </para>
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options"></param>
        /// <param name="routes"></param>
        /// <param name="area">The name of the area within which the routes should be created</param>
        /// <param name="modelAction">Specifies an action to be executed with the ResourceGraphModel
        ///     instance that is built by the ResourceGraphBuilder. The action is executed after the MVC
        ///     routes have been created. For use by application-specific functionality that 
        ///     makes use of the ResourceGraphModel</param>
        public static void MapMvcRoutes(this IResourceBuilder builder, ResourceOptions options, RouteCollection routes, string area = null, Action<Resource> modelAction = null)
        {
            if (builder == null) throw new ArgumentNullException("builder");
            if (options == null) throw new ArgumentNullException("options");
            if (routes == null) throw new ArgumentNullException("routes");

            var rootResource = builder.Build(options);
            new MvcRouteCreator().CreateRoutes(rootResource, routes, area);
            if (modelAction != null)
                modelAction(rootResource);

            // Reset cache used during the mapping process
            ActionMappingHelper.ResetCache();
        } 
    }
}