using System;
using System.Web.Routing;

namespace RezRouting.AspNetMvc
{
    /// <summary>
    /// Extension methods for mapping MVC routes based on resources and routes
    /// configured by a RouteMapper
    /// </summary>
    public static class MvcRouteMapperExtensions
    {
        /// <summary>
        /// <para>
        /// Creates ASP.Net MVC routes within the specified RouteCollection based on the resources 
        /// and routes configured by this RouteMapper. Optionally, a custom action can be specified,
        /// which provides access to the ResourcesModel that is created by the RouteMapper.
        /// </para> 
        /// <para>
        /// This is a convenience method that is equivalent to the following: <c> new MvcRouteCreator().CreateRoutes(mapper.Build(), routes, area)</c>.
        /// </para>
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="routes"></param>
        /// <param name="area">The name of the area within which the routes should be created</param>
        /// <param name="modelAction">Specifies an action to be executed with the ResourcesModel
        /// instance that is built by the RouteMapper. The action is executed after the MVC
        /// routes have been created. For use by application-specific functionality that 
        /// makes use of the ResourcesModel</param>
        public static void MapMvcRoutes(this RouteMapper mapper, RouteCollection routes, string area = null, Action<ResourcesModel> modelAction = null)
        {
            if (mapper == null) throw new ArgumentNullException("mapper");
            if (routes == null) throw new ArgumentNullException("routes");

            var model = mapper.Build();
            new MvcRouteCreator().CreateRoutes(model, routes, area);
            if (modelAction != null)
                modelAction(model);
        } 
    }
}