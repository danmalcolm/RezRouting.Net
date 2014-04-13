using System;
using System.Collections.Specialized;

namespace RezRouting.Tests.Shared
{
    /// <summary>
    /// Properties of a request used to test route mapping
    /// </summary>
    public class RouteTestingRequest
    {
        /// <summary>
        /// Creates instance using shorthand "[method] [path]"
        /// </summary>
        public static RouteTestingRequest Create(string request, NameValueCollection headers = null, NameValueCollection form = null)
        {
            var requestParts = request.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if(requestParts.Length != 2)
                throw new ArgumentException("Request should be in the form [METHOD] [PATH], e.g. \"GET /users\"", request);
            string httpMethod = requestParts[0];
            string path = requestParts[1];
            return new RouteTestingRequest(httpMethod, path, headers ?? new NameValueCollection(), form ?? new NameValueCollection());
        }

        public RouteTestingRequest (string httpMethod, string path, 
            NameValueCollection headers, NameValueCollection form)
        {
            HttpMethod = httpMethod;
            Path = path;
            Headers = headers;
            Form = form;
        }

        public string HttpMethod { get; set; }

        public string Path { get; set; }

        public NameValueCollection Headers { get; set; }

        public NameValueCollection Form { get; set; }
    }
}