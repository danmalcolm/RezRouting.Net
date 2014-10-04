using System;
using System.Web.Mvc;
using RezRouting.Demos.Tasks.ViewEngines;

namespace RezRouting.Demos.Tasks.Controllers.Shared
{
    [ViewPathSettings(OmitControllerName = true)]
    public class TaskController : Controller
    {
        private static string TrimControllerFromTypeName(Type controllerType)
        {
            string value = controllerType.Name;
            int index = value.LastIndexOf("Controller", StringComparison.InvariantCultureIgnoreCase);
            if (index != -1)
            {
                value = value.Substring(0, index);
            }
            return value;
        }

        protected ViewResult DisplayTaskView(object model)
        {
            string viewName = TrimControllerFromTypeName(GetType());
            return View(viewName, model);
        }
    }
}