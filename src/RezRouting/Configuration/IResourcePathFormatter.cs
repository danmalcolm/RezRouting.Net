namespace RezRouting.Configuration
{
    public interface IResourcePathFormatter
    {
        string GetResourcePath(string resourceName);
    }
}