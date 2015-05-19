using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace RezRouting.Demos.MvcWalkthrough2.ViewEngines
{
    // Adapted from https://github.com/danmalcolm/ControllerPathViewEngine
    public class ControllerPathResolver
    {
        private readonly ConcurrentDictionary<Type,string> paths = new ConcurrentDictionary<Type, string>(); 
        private readonly ControllerPathSettings settings;

        public ControllerPathResolver(ControllerPathSettings settings)
        {
            this.settings = settings;
        }

        public string GetPath(Type controllerType)
        {
            return paths.GetOrAdd(controllerType, GetControllerPath);
        }

        private string GetControllerPath(Type controllerType)
        {
            string controllerName = GetControllerName(controllerType);
            var directories = GetDirectoriesBaseOnNamespace(controllerType).ToList();

            bool excludeControllerName = settings.MergeNameIntoNamespace
                && controllerName.Equals(directories.LastOrDefault(), StringComparison.OrdinalIgnoreCase);
            if (!excludeControllerName)
            {
                directories.Add(controllerName);
            }

            return string.Join("/", directories);
        }

        private IEnumerable<string> GetDirectoriesBaseOnNamespace(Type controllerType)
        {
            var directories = controllerType.Namespace != null
                ? controllerType.Namespace.Split('.')
                : null;

            if (directories == null || directories.Length == 0)
                return Enumerable.Empty<string>();
           
            // Get a directory for each level within namespace below last occurence of 
            // a "Controllers" element.
            return directories
                .Reverse()
                .TakeWhile(x => x != "Controllers")
                .Reverse();
        }

        private string GetControllerName(Type controllerType)
        {
            string name = controllerType.Name;
            if (name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
            {
                return name.Substring(0, name.Length - "Controller".Length);
            }
            return name;
        }
    }
}