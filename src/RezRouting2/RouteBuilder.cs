using System;
using System.Collections.Generic;
using System.Linq;

namespace RezRouting2
{
    public class RouteBuilder
    {
        private Type controllerType;

        private bool skip;

        private string name;
        private string httpMethod = "GET";
        private string path = "";
        private string action = "";

        public RouteBuilder(Type controllerType)
        {
            this.controllerType = controllerType;
        }

        public void Name(string name)
        {
            this.name = name;
        }

        public void Action(string action)
        {
            this.action = action;
        }

        public void HttpMethod(string method)
        {
            httpMethod = method;
        }

        public void Path(string path)
        {
            this.path = path;
        }

        public Route Build()
        {
            if (skip) return null;

            ThrowIfInvalid();

            return new Route(name, controllerType, action, httpMethod, path);
        }

        private void ThrowIfInvalid()
        {
            var missingPropertyNames = new List<string>();
            if(name == null) missingPropertyNames.Add("Name");
            if(action == null) missingPropertyNames.Add("Action");
            if(httpMethod == null) missingPropertyNames.Add("HttpMethod");
            if(path == null) missingPropertyNames.Add("Path");
            if(missingPropertyNames.Any())
            {
                string list = string.Join(", ", missingPropertyNames);
                throw new InvalidOperationException(string.Format("Cannot build route because the following required properties have not been configured: {0}", list));
            }
        }

        public void Skip()
        {
            this.skip = true;
        }
    }
}