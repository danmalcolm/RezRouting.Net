using System;
using RezRouting.Configuration;
using RezRouting.Configuration.Conventions;

namespace RezRouting.AspNetMvc
{
    /// <summary>
    /// Defines an ASP.Net MVC controller class responsible for handling a resource's routes
    /// </summary>
    public class MvcController : IResourceHandler
    {
        public static MvcController Create<T>()
        {
            return new MvcController(typeof(T));
        }

        public MvcController(Type controllerType)
        {
            if (controllerType == null) throw new ArgumentNullException("controllerType");
            ControllerType = controllerType;
        }

        public Type ControllerType { get; private set; }

        public override string ToString()
        {
            return string.Format("ControllerType: {0}", ControllerType);
        }
    }
}