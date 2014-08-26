using System;

namespace RezRouting2
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

        public string Name { set; get; }

        public Type ControllerType { get; private set; }
        
        public string Action { get; private set; }
        
        public string HttpMethod { get; private set; }
        
        public string Path { get; private set; }
    }
}