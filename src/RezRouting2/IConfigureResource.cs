using System;

namespace RezRouting2
{
    public interface IConfigureResource
    {
        void Singular(string name, Action<IConfigureSingular> configure);
        void Collection(string name, Action<IConfigureCollection> configure);
        void HandledBy<T>();
        void HandledBy(Type type);
    }

    
}