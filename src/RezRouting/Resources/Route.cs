using System;
using System.Collections.Generic;
using RezRouting.Utility;

namespace RezRouting.Resources
{
    /// <summary>
    /// Represents a route belonging to a Resource
    /// </summary>
    public class Route
    {
        /// <summary>
        /// Creates a Route
        /// </summary>
        /// <param name="name"></param>
        /// <param name="controllerType"></param>
        /// <param name="action"></param>
        /// <param name="httpMethod"></param>
        /// <param name="path"></param>
        /// <param name="customProperties"></param>
        public Route(string name, Type controllerType, string action, string httpMethod, string path, IDictionary<string, object> customProperties = null)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (controllerType == null) throw new ArgumentNullException("controllerType");
            if (action == null) throw new ArgumentNullException("action");
            if (httpMethod == null) throw new ArgumentNullException("httpMethod");
            if (path == null) throw new ArgumentNullException("path");
            
            Name = name;
            ControllerType = controllerType;
            Action = action;
            HttpMethod = httpMethod;
            Path = path;
            CustomProperties = customProperties != null
                ? new Dictionary<string, object>(customProperties)
                : new Dictionary<string, object>(); ;
        }

        internal void InitResource(Resource resource)
        {
            Resource = resource;
        }

        /// <summary>
        /// The name of this Route
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// The full name of this Route (combined with the full name of the parent Resource)
        /// </summary>
        public string FullName
        {
            get { return Resource.FullName + "." + Name; }
        }

        /// <summary>
        /// The resource to which this Route belongs
        /// </summary>
        public Resource Resource { get; private set; }

        /// <summary>
        /// The controller that handles this Route
        /// </summary>
        public Type ControllerType { get; private set; }
        
        /// <summary>
        /// The name of the controller action that handles this Route
        /// </summary>
        public string Action { get; private set; }
        
        /// <summary>
        /// The HTTP method for requests that will be handled by this Route
        /// </summary>
        public string HttpMethod { get; private set; }
        
        /// <summary>
        /// The path within the URL for requests that will be handled by this Route
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Additional data assigned to this Route - designed for use by application-specific
        /// extensions to core routing functionality.
        /// </summary>
        public IDictionary<string, object> CustomProperties { get; private set; }

        /// <summary>
        /// The full URL for requests that will be handled by this Route
        /// </summary>
        public string Url
        {
            get
            {
                return UrlPathHelper.JoinPaths(Resource.Url, Path);
            }
        }
    }
}