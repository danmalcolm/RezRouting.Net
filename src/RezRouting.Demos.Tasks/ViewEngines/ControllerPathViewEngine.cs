using System;
using System.Web.Mvc;

namespace RezRouting.Demos.Tasks.ViewEngines
{
    public class ControllerPathViewEngine : RazorViewEngine
    {
        private readonly ControllerPathResolver controllerPathResolver;

        public ControllerPathViewEngine(ControllerPathSettings settings)
            : this(null, settings)
        {
        }

        public ControllerPathViewEngine(IViewPageActivator viewPageActivator, ControllerPathSettings settings)
            : base(viewPageActivator)
        {
            controllerPathResolver = new ControllerPathResolver(settings);
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            return InvokeWithControllerPath(controllerContext, () => base.FindView(controllerContext, viewName, masterName, useCache));
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            return InvokeWithControllerPath(controllerContext, () => base.FindPartialView(controllerContext, partialViewName, useCache));
        }

        /// <summary>
        /// Switches value of the "controller" route value within the current ControllerContext to the controller path, invokes the 
        /// FindView/FindPartialView method and restores the "controller" value. 
        /// 
        /// This allows us to use the existing functionality to find views nested according to the controller namespace.
        /// 
        /// Internally, ViewEngines that inherit from VirtualPathProviderViewEngine cache the result of finding views. The cache 
        /// key is generated from the "controller" value. Using the full controller path allows separate views to be cached, 
        /// supporting situations where we have the same controller name in different namespaces.
        /// </summary>
        private T InvokeWithControllerPath<T>(ControllerContext controllerContext, Func<T> func)
        {
            string controllerName = controllerContext.RouteData.GetRequiredString("controller");
            string controllerPath = controllerPathResolver.GetPath(controllerContext.Controller.GetType());
            controllerContext.RouteData.Values["controller"] = controllerPath;
            var result = func();
            controllerContext.RouteData.Values["controller"] = controllerName;
            return result;
        }
    }
}
