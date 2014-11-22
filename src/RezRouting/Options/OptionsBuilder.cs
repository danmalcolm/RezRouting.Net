using System;

namespace RezRouting.Options
{
    /// <summary>
    /// Configures settings applied to a resource's routes when they are mapped
    /// </summary>
    public class OptionsBuilder : IConfigureOptions, IOptionsBuilder
    {
        private UrlPathFormatter resourcePathFormatter = new UrlPathFormatter();
        private IIdNameConvention idNameConvention = new DefaultIdNameConvention();

        /// <summary>
        /// Use the supplied settings to format the resource path.
        /// </summary>
        /// <param name="settings"></param>
        public void FormatUrlPaths(UrlPathSettings settings)
        {
            if (settings == null) throw new ArgumentNullException("settings");
            resourcePathFormatter = new UrlPathFormatter(settings);
        }

        /// <summary>
        /// Sets the strategy used to format name of id parameters used in route URLs
        /// </summary>
        /// <param name="convention"></param>
        public void CustomiseIdNames(IIdNameConvention convention)
        {
            if (convention == null) throw new ArgumentNullException("convention");
            idNameConvention = convention;
        }

        /// <inheritdoc />
        public RouteOptions Build()
        {
            return new RouteOptions(resourcePathFormatter, idNameConvention);
        }
    }
}