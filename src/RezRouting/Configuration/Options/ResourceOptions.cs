using System;
using System.Collections.Generic;
using RezRouting.Configuration.Conventions;

namespace RezRouting.Configuration.Options
{
    /// <summary>
    /// Options applied during resource and route configuration
    /// </summary>
    public class ResourceOptions
    {
        private readonly List<IRouteConvention> routeConventions = new List<IRouteConvention>();
        private IIdNameConvention idNameConvention = new DefaultIdNameConvention();
        private UrlPathSettings urlPathSettings = new UrlPathSettings();

        /// <summary>
        /// The UrlPathSettings to specify the URL path format used for route URLs
        /// </summary>
        public UrlPathSettings UrlPathSettings
        {
            get
            {
                return urlPathSettings;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                urlPathSettings = value;
            }
        }

        /// <summary>
        /// The convention used to format id names when configuring routes
        /// </summary>
        public IIdNameConvention IdNameConvention
        {
            get
            {
                return idNameConvention;
            }
            set
            {
                if(value == null) throw new ArgumentNullException("value");
                idNameConvention = value;
            }
        }

        /// <summary>
        /// The conventions used to generated routes for the resources being configured
        /// </summary>
        public IEnumerable<IRouteConvention> RouteConventions
        {
            get { return this.routeConventions.AsReadOnly(); }
        }

        /// <summary>
        /// Specifies a convention scheme containing conventions used to generate routes 
        /// for the resources configured by this ResourceGraphBuilder
        /// </summary>
        /// <param name="scheme"></param>
        public void AddRouteConventions(IRouteConventionScheme scheme)
        {
            var conventions = scheme.GetConventions();
            this.routeConventions.AddRange(conventions);
        }
    }
}