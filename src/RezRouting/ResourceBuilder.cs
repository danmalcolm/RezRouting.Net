using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RezRouting.Configuration;
using RezRouting.Model;
using RezRouting.Utility;

namespace RezRouting
{
    /// <summary>
    /// Base class for "builder" classes used at configuration time to specify the routes
    /// for a singular or collection resource
    /// </summary>
    public abstract class ResourceBuilder
    {
        private const string DefaultIdName = "id";
        private readonly List<ResourceBuilder> children = new List<ResourceBuilder>();
        private readonly HashSet<Type> controllerTypes = new HashSet<Type>();
        private readonly List<string> includedRouteNames = new List<string>();
        private readonly List<string> excludedRouteNames = new List<string>();
        private string customName;
        private string customPath;
        private string customIdName;
        private string customIdNameAsAncestor;

        public void CustomName(string name)
        {
            customName = name;
        }

        public void CustomUrlPath(string path)
        {
            customPath = path;
        }

        protected void CustomIdName(string name)
        {
            customIdName = name;
        }

        protected void CustomIdNameAsAncestor(string name)
        {
            customIdNameAsAncestor = name;
        }

        protected abstract ResourceType ResourceType { get; }

        #region Configuration options

        /// <summary>
        /// Specifies the routes to include when mapping routes for this resource.
        /// Only the routes with the specified names will be mapped.
        /// </summary>
        /// <param name="names">The names of the RouteTypes to include</param>
        public void Include(params string[] names)
        {
            if (excludedRouteNames.Any())
                throw new InvalidOperationException("Cannot combine include and exclude");
            includedRouteNames.AddRange(names);
        }

        /// <summary>
        /// Specifies the routes to exclude when mapping routes for this resource.
        /// The routes with the specified names will not be mapped.
        /// </summary>
        /// <param name="names">The names of the RouteTypes to exclude</param>
        public void Exclude(params string[] names)
        {
            if (includedRouteNames.Any())
                throw new InvalidOperationException("Cannot combine include and exclude");
            excludedRouteNames.AddRange(names);
        }

        /// <summary>
        /// Sets a controller used to handle this resource's actions
        /// </summary>
        /// <typeparam name="TController"></typeparam>
        public void HandledBy<TController>()
            where TController : Controller
        {
            HandledBy(typeof(TController));
        }

        /// <summary>
        /// Sets controllers used to handle this resource's actions
        /// </summary>
        /// <typeparam name="TController1"></typeparam>
        /// <typeparam name="TController2"></typeparam>
        public void HandledBy<TController1, TController2>()
            where TController1 : Controller
            where TController2 : Controller
        {
            HandledBy(typeof(TController1));
            HandledBy(typeof(TController2));
        }

        /// <summary>
        /// Sets controllers used to handle this resource's actions
        /// </summary>
        /// <typeparam name="TController1"></typeparam>
        /// <typeparam name="TController2"></typeparam>
        /// <typeparam name="TController3"></typeparam>
        public void HandledBy<TController1, TController2, TController3>()
            where TController1 : Controller
            where TController2 : Controller
            where TController3 : Controller
        {
            HandledBy(typeof(TController1));
            HandledBy(typeof(TController2));
            HandledBy(typeof(TController3));
        }

        /// <summary>
        /// Sets controllers used to handle this resource's actions
        /// </summary>
        /// <typeparam name="TController1"></typeparam>
        /// <typeparam name="TController2"></typeparam>
        /// <typeparam name="TController3"></typeparam>
        /// <typeparam name="TController4"></typeparam>
        public void HandledBy<TController1, TController2, TController3, TController4>()
            where TController1 : Controller
            where TController2 : Controller
            where TController3 : Controller
            where TController4 : Controller
        {
            HandledBy(typeof(TController1));
            HandledBy(typeof(TController2));
            HandledBy(typeof(TController3));
            HandledBy(typeof(TController4));
        }

        /// <summary>
        /// Sets a controller used to handle actions
        /// </summary>
        /// <param name="controllerType"></param>
        // ReSharper disable once MemberCanBePrivate.Global
        public void HandledBy(Type controllerType)
        {
            controllerTypes.Add(controllerType);
        }

        /// <summary>
        /// Sets up routes for a child collection resource within the current resource
        /// </summary>
        /// <param name="configure"></param>
        public void Collection(Action<CollectionBuilder> configure)
        {
            var builder = new CollectionBuilder();
            configure(builder);
            children.Add(builder);
        }

        /// <summary>
        /// Creates routes for a child singular resource within the current resource
        /// </summary>
        /// <param name="configure"></param>
        public void Singular(Action<SingularBuilder> configure)
        {
            var builder = new SingularBuilder();
            configure(builder);
            children.Add(builder);
        }

        #endregion

        /// <summary>
        /// Builds a resource model based on settings configured
        /// </summary>
        /// <returns></returns>
        internal Resource Build(RouteConfiguration configuration, Resource parent, string fullNamePrefix)
        {
            string name = customName ?? GetNameBasedOnControllers(configuration);
            var routeProperties = GetRouteUrlProperties(configuration, name);
            string fullName = fullNamePrefix + name;

            var routes = GetRoutes(configuration);

            var resource = new Resource(fullName, parent, routeProperties, ResourceType, routes);
            var childResources = from child in children
                                 select child.Build(configuration, resource, fullName + ".");
            resource.SetChildren(childResources);
            return resource;
        }

        private RouteUrlProperties GetRouteUrlProperties(RouteConfiguration configuration, string name)
        {
            string resourcePath = customPath ?? FormatResourcePath(name, configuration);

            string idName = customIdName ?? DefaultIdName;
            string idNameAsAncestor = customIdNameAsAncestor 
                ?? GetDefaultIdNameAsAncestor(name);

            var routeProperties = new RouteUrlProperties(resourcePath, idName, idNameAsAncestor);
            return routeProperties;
        }

        private static string GetDefaultIdNameAsAncestor(string name)
        {
            return name.Singularize(Plurality.CouldBeEither).Camelize() + "Id";
        }

        private string GetNameBasedOnControllers(RouteConfiguration configuration)
        {
            string name = configuration.ResourceNameConvention.GetResourceName(controllerTypes, ResourceType);
            if (string.IsNullOrWhiteSpace(name))
            {
                string controllerTypeNames = string.Join(", ", controllerTypes.Select(x => x.Name));
                throw new RouteConfigurationException(
                    "Unable to infer resource name from controller types " + controllerTypeNames +
                    ". Consider setting the name explicitly via the ResourceName method.");
            }
            return name;
        }

        private IEnumerable<ResourceRoute> GetRoutes(RouteConfiguration configuration)
        {
            // Get applicable RouteTypes for current resource type (singular or collection)
            var routeTypes = configuration.RouteTypes
                .Where(rt => rt.ResourceTypes.Contains(ResourceType));
            if (includedRouteNames.Any())
            {
                routeTypes = routeTypes.Where(
                    rt => includedRouteNames.Contains(rt.Name, StringComparer.InvariantCultureIgnoreCase));
            }
            if (excludedRouteNames.Any())
            {
                routeTypes = routeTypes.Where(
                    rt => !excludedRouteNames.Contains(rt.Name, StringComparer.InvariantCultureIgnoreCase));
            }

            // Get available actions on controllers handling actions for our resource
            var actions = (from controllerType in controllerTypes
                let descriptor = new ReflectedControllerDescriptor(controllerType)
                from action in descriptor.GetCanonicalActions()
                let actionName = action.GetActionNameOverride() ?? action.ActionName
                select new {ControllerType = descriptor.ControllerType, ActionName = actionName}).ToList();

            // Get first available action for each route
            var routes = from routeType in routeTypes
                orderby routeType.MappingOrder
                let action = actions.FirstOrDefault
                    (a => a.ActionName.EqualsIgnoreCase(routeType.ControllerAction))
                where action != null
                select new ResourceRoute(routeType, action.ControllerType);
            return routes;
        }

        private string FormatResourcePath(string resourceName, RouteConfiguration configuration)
        {
            return configuration.ResourcePathFormatter.GetResourcePath(resourceName);
        }
    }
}