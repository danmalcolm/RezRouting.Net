using System.Collections.Generic;
using RezRouting.Configuration.Conventions;

namespace RezRouting.Configuration.Options
{
    /// <summary>
    /// Options applied during resource and route configuration
    /// </summary>
    public class ConfigurationOptions
    {
        private readonly List<IRouteConvention> routeConventions = new List<IRouteConvention>();
        
        public ConfigurationOptions(UrlPathSettings urlPathSettings, IIdNameFormatter idNameFormatter)
        {
            UrlPathSettings = urlPathSettings;
            IdNameFormatter = idNameFormatter;
        }

        /// <summary>
        /// the settings that control path formatting in route URL template format
        /// </summary>
        public UrlPathSettings UrlPathSettings { get; private set; }

        /// <summary>
        /// Formats id values identifying collection item resources in 
        /// route URL templates. 
        /// </summary>
        public IIdNameFormatter IdNameFormatter { get; private set; }
    }
}