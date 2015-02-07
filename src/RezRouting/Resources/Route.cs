using System;
using System.Collections.Generic;
using RezRouting.Utility;

namespace RezRouting.Resources
{
    /// <summary>
    /// Represents a route belonging to a Resource. A route maps a URL path and other
    /// properties of an HTTP request (such as the HTTP method) to the handler 
    /// responsible for executing the request.
    /// </summary>
    public class Route
    {
        /// <summary>
        /// Creates a Route
        /// </summary>
        /// <param name="name"></param>
        /// <param name="httpMethod"></param>
        /// <param name="path"></param>
        /// <param name="customProperties"></param>
        public Route(string name, IResourceRouteHandler handler, string httpMethod, string path, CustomValueCollection customProperties = null, CustomValueCollection additionalRouteValues = null)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (handler == null) throw new ArgumentNullException("handler");
            if (httpMethod == null) throw new ArgumentNullException("httpMethod");
            if (path == null) throw new ArgumentNullException("path");
            
            Name = name;
            Handler = handler;
            HttpMethod = httpMethod;
            Path = path;
            CustomProperties = customProperties != null 
                ? new CustomValueCollection(customProperties) 
                : new CustomValueCollection();
            AdditionalRouteValues = additionalRouteValues != null
                ? new CustomValueCollection(additionalRouteValues)
                : new CustomValueCollection();
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
        /// The handler that handles this Route
        /// </summary>
        public IResourceRouteHandler Handler { get; private set; }
        
        /// <summary>
        /// The HTTP method for requests that will be handled by this Route
        /// </summary>
        public string HttpMethod { get; private set; }
        
        /// <summary>
        /// The path within the URL for requests that will be handled by this Route
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Custom data assigned to this Route - designed for use by application-specific
        /// extensions to core routing functionality.
        /// </summary>
        public CustomValueCollection CustomProperties { get; private set; }

        /// <summary>
        /// Additional values used to build the route URL and added to route when
        /// configured
        /// </summary>
        public CustomValueCollection AdditionalRouteValues { get; private set; }

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