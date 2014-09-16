using RezRouting.Options;

namespace RezRouting
{
    public interface IConfigureOptions
    {
        void FormatUrlPaths(UrlPathSettings settings);

        /// <summary>
        /// Sets the strategy used to format name of id parameters used in route URLs
        /// </summary>
        /// <param name="convention"></param>
        void CustomiseIdNames(IIdNameConvention convention);
    }
}