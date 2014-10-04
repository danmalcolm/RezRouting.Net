using RezRouting.Options;

namespace RezRouting
{
    public interface IConfigureOptions
    {
        /// <summary>
        /// Specifies settings used to format paths within resource and task URLs
        /// </summary>
        /// <param name="settings"></param>
        void FormatUrlPaths(UrlPathSettings settings);

        /// <summary>
        /// Sets the strategy used to format name of id parameters used in route URLs
        /// </summary>
        /// <param name="convention"></param>
        void CustomiseIdNames(IIdNameConvention convention);
    }
}