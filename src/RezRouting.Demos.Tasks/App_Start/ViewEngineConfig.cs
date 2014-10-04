using System.Web.Mvc;
using RezRouting.Demos.Tasks.ViewEngines;

namespace RezRouting.Demos.Tasks
{
    public static class ViewEngineConfig
    {
        public static void Init(ViewEngineCollection viewEngines)
        {
            viewEngines.Clear();
            var settings = new ControllerPathSettings(typeof (MvcApplication).Namespace, mergeNameIntoNamespace: true);
            viewEngines.Add(new ControllerPathViewEngine(settings));
        }
    }
}
