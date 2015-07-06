using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using RezRouting.AspNetMvc.UrlGeneration;
using RezRouting.Demos.MvcWalkthrough3.Controllers.Common;

namespace RezRouting.Demos.MvcWalkthrough3.Utility
{
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Generates URL for action based on action expression
        /// </summary>
        /// <typeparam name="TController"></typeparam>
        /// <param name="urlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="additionalRouteValues"></param>
        /// <returns></returns>
        public static string ResourceUrl<TController>(this UrlHelper urlHelper, Expression<Action<TController>> expression, object additionalRouteValues = null)
             where TController : Controller
        {
            var routeValues = Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression(expression);
            var action = routeValues["action"].ToString();
            if (additionalRouteValues != null)
            {
                var additionalValueDict = new RouteValueDictionary(additionalRouteValues);
                foreach (var item in additionalValueDict)
                {
                    routeValues[item.Key] = item.Value;
                }
            }
            return urlHelper.ResourceUrl(typeof(TController), action, routeValues);
        }

        /// <summary>
        /// Generates URL to a task route, based on controller handling the task
        /// </summary>
        /// <typeparam name="TController"></typeparam>
        /// <param name="urlHelper"></param>
        /// <returns></returns>
        public static string TaskUrl<TController>(this UrlHelper urlHelper, object routeValues = null)
            where TController : Controller, ITaskController
        {
            return urlHelper.ResourceUrl<TController>("Edit", routeValues);
        }
    }
}