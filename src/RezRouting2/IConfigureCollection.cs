using System;

namespace RezRouting2
{
    public interface IConfigureCollection : IConfigureResource
    {
        void Items(Action<CollectionItemBuilder> configure);
        void UrlPath(string path);
    }
}