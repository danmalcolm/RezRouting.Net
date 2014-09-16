namespace RezRouting
{
    /// <summary>
    /// Sets the properties of a route
    /// </summary>
    public interface IConfigureRoute
    {
        void Configure(string name, string action, string httpMethod, string path);
    }
}