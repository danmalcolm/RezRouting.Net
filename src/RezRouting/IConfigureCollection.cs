using System;

namespace RezRouting
{
    public interface IConfigureCollection : IConfigureResource
    {
        void Items(Action<CollectionItemBuilder> configure);
        void UrlPath(string path);
    }
}