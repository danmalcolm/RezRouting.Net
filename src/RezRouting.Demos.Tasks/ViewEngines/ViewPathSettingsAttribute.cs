using System;

namespace RezRouting.Demos.Tasks.ViewEngines
{
    /// <summary>
    /// Settings that apply to the logic used to formulate the view location based on the controller path.
    /// Used by ControllerPathViewEngine.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ViewPathSettingsAttribute : Attribute
    {
        /// <summary>
        /// Specifies that the name of the controller should not be included in the path to the view.
        /// Suitable when there are several controllers in the same namespace that render different views
        /// and you don't want a subfolder for each controller.
        /// </summary>
        public bool OmitControllerName { get; set; }
    }
}