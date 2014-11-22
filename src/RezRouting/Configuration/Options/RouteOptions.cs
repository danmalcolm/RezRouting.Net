namespace RezRouting.Configuration.Options
{
    /// <summary>
    /// Options applied during route creation
    /// </summary>
    public class RouteOptions
    {
        /// <summary>
        /// Creates a RouteOptions
        /// </summary>
        /// <param name="pathFormatter"></param>
        /// <param name="idNameConvention"></param>
        public RouteOptions(UrlPathFormatter pathFormatter, IIdNameConvention idNameConvention)
        {
            IdNameConvention = idNameConvention;
            PathFormatter = pathFormatter;
        }

        /// <summary>
        /// The UrlPathFormatter used to format paths when configuring routes
        /// </summary>
        public UrlPathFormatter PathFormatter { get; private set; }

        /// <summary>
        /// The convention used to format id names when configuring routes
        /// </summary>
        public IIdNameConvention IdNameConvention { get; private set; }
    }
}