namespace RezRouting
{
    public interface IConfigureSingular : IConfigureResource
    {
        void UrlPath(string path);
    }
}