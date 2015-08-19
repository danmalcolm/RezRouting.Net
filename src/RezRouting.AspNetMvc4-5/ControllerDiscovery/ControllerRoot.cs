using System;
using System.Reflection;

namespace RezRouting.AspNetMvc.ControllerDiscovery
{
    /// <summary>
    /// Contains details of assembly and root namespace containing classes
    /// that handle a resource hierarchy's routes.
    /// </summary>
    public class ControllerRoot
    {
        public static ControllerRoot From<T>()
        {
            var type = typeof (T);
            return new ControllerRoot(type.Assembly, type.Namespace);
        }

        public ControllerRoot(Assembly assembly, string ns)
        {
            Assembly = assembly;
            Namespace = ns;

        }

        public Assembly Assembly { get; private set; }

        public string Namespace { get; private set; }

        /// <summary>
        /// Indicates whether the specified type is included 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool Includes(Type type)
        {
            if (type.Assembly != Assembly)
                return false;

            if (Equals(type.Namespace, Namespace))
                return true;

            if (type.Namespace != null
                && Namespace != null
                && type.Namespace.StartsWith(Namespace + "."))
                return true;

            return false;
        }
    }
}