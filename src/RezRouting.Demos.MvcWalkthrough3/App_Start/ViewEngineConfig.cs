using System.Web.Mvc;
using RezRouting.Demos.MvcWalkthrough3.ViewEngines;

namespace RezRouting.Demos.MvcWalkthrough3
{
    public static class ViewEnginesConfig
    {
        public static void Configure(ViewEngineCollection viewEngines)
        {
            viewEngines.Clear();
            var settings = new ControllerPathSettings(true);
            var viewEngine = new ControllerPathRazorViewEngine(settings);
            viewEngines.Add(viewEngine);
        }
    }
}