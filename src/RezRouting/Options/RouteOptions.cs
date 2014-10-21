namespace RezRouting.Options
{
    /// <summary>
    /// Options applied during route creation
    /// </summary>
    public class RouteOptions
    {
        public RouteOptions(UrlPathFormatter pathFormatter, IIdNameConvention idNameConvention)
        {
            IdNameConvention = idNameConvention;
            PathFormatter = pathFormatter;
        }

        public UrlPathFormatter PathFormatter { get; private set; }

        public IIdNameConvention IdNameConvention { get; private set; }
    }
}