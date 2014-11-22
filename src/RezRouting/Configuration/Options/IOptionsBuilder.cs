namespace RezRouting.Configuration.Options
{
    /// <summary>
    /// Creates a RouteOption based on any configuration applied
    /// </summary>
    public interface IOptionsBuilder
    {
        /// <summary>
        /// Creates the RouteOption instance
        /// </summary>
        /// <returns></returns>
        RouteOptions Build();
    }
}