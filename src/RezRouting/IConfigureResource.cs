using System;

namespace RezRouting
{
    public interface IConfigureResource
    {
        void Singular(string name, Action<IConfigureSingular> configure);
        void Collection(string name, Action<IConfigureCollection> configure);
        void Collection(string name, string itemName, Action<IConfigureCollection> configure);
        void HandledBy<T>();
        void HandledBy(Type type);
    }

    
}