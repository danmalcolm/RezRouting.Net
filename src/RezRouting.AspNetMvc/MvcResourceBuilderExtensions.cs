using System;
using System.Web.Routing;
using RezRouting.Configuration;
using RezRouting.Resources;

namespace RezRouting.AspNetMvc
{
    /// <summary>
    /// Extension methods for mapping MVC routes based on resources and routes
    /// configured by a ResourceGraphBuilder
    /// </summary>
    public static class MvcResourceBuilderExtensions
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
        /// <param name="routes"></param>
        /// <param name="area">The name of the area within which the routes should be created</param>
        /// <param name="modelAction">Specifies an action to be executed with the ResourceGraphModel
        /// instance that is built by the ResourceGraphBuilder. The action is executed after the MVC
        /// routes have been created. For use by application-specific functionality that 
        /// makes use of the ResourceGraphModel</param>
        public static void MapMvcRoutes(this ResourceGraphBuilder builder, RouteCollection routes, string area = null, Action<ResourceGraphModel> modelAction = null)
        {
            if (builder == null) throw new ArgumentNullException("builder");
            if (routes == null) throw new ArgumentNullException("routes");

            var model = builder.Build();
            new MvcRouteCreator().CreateRoutes(model, routes, area);
            if (modelAction != null)
                modelAction(model);
        } 
    }
}