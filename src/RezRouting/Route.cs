using System;
using RezRouting.Utility;

namespace RezRouting
{
    public class Route
    {
        public Route(string name, Type controllerType, string action, string httpMethod, string path)
        {
            Name = name;
            ControllerType = controllerType;
            Action = action;
            HttpMethod = httpMethod;
            Path = path;
        }

        internal void InitResource(Resource resource)
        {
            Resource = resource;
        }

        public string Name { set; get; }

        public string FullName
        {
            get { return Resource.FullName + "." + Name; }
        }

        public Resource Resource { get; private set; }

        public Type ControllerType { get; private set; }
        
        public string Action { get; private set; }
        
        public string HttpMethod { get; private set; }
        
        public string Path { get; private set; }

        public string Url
        {
            get
            {
                return UrlPathHelper.JoinPaths(Resource.Url, Path);
            }
        }

    }
}