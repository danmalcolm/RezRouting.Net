using System;
using System.Linq;

namespace RezRouting.Demos.Tasks.ViewEngines
{
    public class ControllerPathResolver
    {
        private readonly ControllerPathSettings settings;

        public ControllerPathResolver(ControllerPathSettings settings)
        {
            this.settings = settings;
        }

        public string GetPath(Type controllerType)
        {
            string directoryPath = GetDirectoryPath(controllerType);
            string controllerName = GetControllerName(controllerType);
            var excludeName = ShouldExcludeControllerName(controllerType, directoryPath, controllerName);

            if (excludeName)
            {
                return directoryPath;
            }
            else
            {
                return string.Format("{0}/{1}", directoryPath, controllerName);
            }
        }

        private bool ShouldExcludeControllerName(Type controllerType, string directoryPath, string controllerName)
        {
            // Check if explicitly set via attribute on controller
            bool omitControllerName = controllerType.GetCustomAttributes(typeof (ViewPathSettingsAttribute), true)
                                                    .Cast<ViewPathSettingsAttribute>()
                                                    .Select(x => x.OmitControllerName)
                                                    .FirstOrDefault();
            if (omitControllerName)
                return true;
            
            // Otherwise check if it can be merged with namespace
            return settings.MergeNameIntoNamespace && CanMerge(directoryPath, controllerName);
        }

        private static bool CanMerge(string directoryPath, string controllerName)
        {
            return (directoryPath == controllerName || directoryPath.EndsWith(string.Format("/{0}", controllerName)));
        }

        private string GetDirectoryPath(Type controllerType)
        {
            string subPath = controllerType.Namespace;
            if (subPath.StartsWith(settings.RootControllerNamespace))
            {
                subPath = subPath.Substring(settings.RootControllerNamespace.Length + 1);
            }
            else if (subPath.StartsWith(settings.RootNamespace))
            {
                subPath = subPath.Substring(settings.RootNamespace.Length + 1);
            }
            string directoryPath = subPath.Replace(".", "/");
            return directoryPath;
        }

        private string GetControllerName(Type controllerType)
        {
            string typeName = controllerType.Name;
            if (typeName.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
            {
                return typeName.Substring(0, typeName.Length - "Controller".Length);
            }
            return typeName;
        }
    }
}
