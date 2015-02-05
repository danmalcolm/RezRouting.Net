using RezRouting.Configuration.Options;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Sets up options applied during resource and route configuration
    /// </summary>
    public interface IOptionsConfigurator
    {
        /// <summary>
        /// Specifies the settings that control path formatting in route URL template format
        /// </summary>
        /// <param name="settings"></param>
        void UrlPaths(UrlPathSettings settings);

        /// <summary>
        /// Specifies the format of id values identifying collection item resources in 
        /// route URL templates. 
        /// </summary>
        /// <param name="formatter"></param>
        void IdFormat(IIdNameFormatter formatter);
    }
}