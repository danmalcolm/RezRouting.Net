﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RezRouting.Configuration;
using RezRouting.Model;
using RezRouting.Utility;

namespace RezRouting
{
    internal interface IResourceBuilder
    {
    }

    /// <summary>
    /// Base class for "builder" classes used at configuration time to specify the routes
    /// for a singular or collection resource
    /// </summary>
    public abstract class ResourceBuilder : IResourceBuilder
    {
        private readonly RouteConfigurationBuilder configurationBuilder = new RouteConfigurationBuilder();
        protected readonly List<ResourceBuilder> children = new List<ResourceBuilder>();
        private readonly List<Type> controllerTypes = new List<Type>();
        private readonly List<string> includedRouteNames = new List<string>();
        private readonly List<string> excludedRouteNames = new List<string>();
        private ResourceName customName;
        private string customPath;
        private string customIdName;
        private string customIdNameAsAncestor;

        /// <summary>
        /// Sets a specific name for the resource that will be used in route names and URLs, overriding
        /// the default resource name logic.
        /// </summary>
        public void CustomName(string singular = null, string plural = null)
        {
            customName = new ResourceName(singular, plural);
        }

        /// <summary>
        /// Sets a specific value for the resource's path segment in route URLs
        /// </summary>
        /// <param name="path"></param>
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
        /// Customises all routes by mapped for the current resource based on the
        /// configuration options specified - applies only to the current resource, not to any nested
        /// resources</summary>
        /// <param name="configure"></param>
        public void Configure(Action<RouteConfigurationBuilder> configure)
        {
            configure(configurationBuilder);
        }

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
        /// Sets controllers used to handle this resource's action by 
        /// finding all controllers within same namespace and assembly
        /// of a controller type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void HandledByAllInScopeOf<T>()
        {
            var type = typeof (T);
            var assembly = type.Assembly;
            var types = assembly.GetExportedTypes()
                .Where(x => x.IsSubclassOf(typeof (Controller)) && x.Namespace == type.Namespace);
            types.Each(HandledBy);
        }

        /// <summary>
        /// Sets a controller used to handle actions
        /// </summary>
        /// <param name="controllerType"></param>
        // ReSharper disable once MemberCanBePrivate.Global
        public void HandledBy(Type controllerType)
        {
            if (controllerTypes.Contains(controllerType))
            {
                throw new ArgumentException(string.Format("The controller type {0} has already been added for this resource", controllerType.Name));
            }
            controllerTypes.Add(controllerType);
        }

        /// <summary>
        /// Sets up routes for a child collection resource nested within the current resource
        /// </summary>
        /// <param name="configure"></param>
        public void Collection(Action<CollectionBuilder> configure)
        {
            var builder = new CollectionBuilder();
            configure(builder);
            children.Add(builder);
        }

        /// <summary>
        /// Creates routes for a child singular resource nested within the current resource
        /// </summary>
        /// <param name="configure"></param>
        public void Singular(Action<SingularBuilder> configure)
        {
            var builder = new SingularBuilder();
            configure(builder);
            children.Add(builder);
        }

        #endregion
        
        internal Resource Build(RouteConfiguration sharedConfiguration, ResourceBuildContext context)
        {
            var configuration = configurationBuilder.Extend(sharedConfiguration);

            return BuildResource(configuration, context);
        }

        protected abstract Resource BuildResource(RouteConfiguration configuration, ResourceBuildContext context);

        protected ResourceName GetResourceName(RouteConfiguration configuration)
        {
            return customName ?? configuration.ResourceNameConvention.GetResourceName(controllerTypes, ResourceType);
        }

        protected RouteUrlProperties GetRouteUrlProperties(RouteConfiguration configuration, ResourceName name)
        {
            string resourcePath = customPath ?? FormatResourcePath(name, configuration);

            string idName = customIdName 
                ?? configuration.IdNameConvention.GetIdName(name);
            string idNameAsAncestor = customIdNameAsAncestor 
                ?? configuration.IdNameConvention.GetIdNameAsAncestor(name);

            var routeProperties = new RouteUrlProperties(resourcePath, idName, idNameAsAncestor);
            return routeProperties;
        }

        private string FormatResourcePath(ResourceName resourceName, RouteConfiguration configuration)
        {
            string name = ResourceType == ResourceType.Collection ? resourceName.Plural : resourceName.Singular;
            return configuration.ResourcePathFormatter.GetResourcePath(name);
        }

        protected IEnumerable<ResourceRoute> GetRoutes(ResourceName resourceName, string[] resourceNames, RouteConfiguration configuration)
        {
            var routeTypes = GetApplicableRouteTypes(configuration);

            // Get list of available actions on each controller
            var controllers = (from controllerType in controllerTypes
                               let controllerDescriptor = new ReflectedControllerDescriptor(controllerType)
                               from actionDescriptor in controllerDescriptor.GetCanonicalActions()
                               let actionName = actionDescriptor.GetActionNameOverride() ?? actionDescriptor.ActionName
                               group actionName by controllerType into @group
                               select new
                               {
                                   Type = @group.Key,
                                   ActionNames = @group.Distinct(StringComparer.InvariantCultureIgnoreCase).ToArray()
                               }).ToList();

            // Create route(s) for each RouteType based on matching controller(s)
            var routes = from routeType in routeTypes
                         orderby routeType.MappingOrder
                         let routeControllers = (from c in controllers
                                                 where c.ActionNames.ContainsIgnoreCase(routeType.ActionName)
                                                 let settings = routeType.GetCustomSettings(ResourceType, resourceName, c.Type)
                                                 where settings.Include
                                                 select new {ControllerType = c.Type, Settings = settings }).ToList()
                         let multipleControllers = routeControllers.Count > 1
                         from routeController in routeControllers
                         let routeName = GetRouteName(configuration, resourceNames, routeType, routeController.ControllerType, routeController.Settings, multipleControllers)
                         select new ResourceRoute(routeName, routeType, ResourceType, routeController.ControllerType, routeController.Settings);

            return routes;
        }

        private RouteType[] GetApplicableRouteTypes(RouteConfiguration configuration)
        {
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
            return routeTypes.ToArray();
        }

        private string GetRouteName(RouteConfiguration configuration, string[] resourceNames, RouteType routeType, Type controllerType, CustomRouteSettings settings, bool multipleControllers)
        {
            string prefix = configuration.RouteNamePrefix;
            if (prefix != "")
                prefix += ".";
            // If multiple controllers, we have to include controllerName to prevent Route name clashes
            bool includeControllerName = settings.IncludeControllerInRouteName || multipleControllers;
            return prefix + configuration.RouteNameConvention.GetRouteName(resourceNames, routeType.Name, controllerType, includeControllerName);
        }
    }
}