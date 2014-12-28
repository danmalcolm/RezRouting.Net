using System;
using RezRouting.Resources;
using RezRouting.Utility;

namespace RezRouting.AspNetMvc
{
    /// <summary>
    /// Defines an ASP.Net MVC controller action responsible for handling an individual route
    /// </summary>
    public class MvcAction : IRouteHandler
    {
        public MvcAction(Type controllerType, string actionName)
        {
            if (controllerType == null) throw new ArgumentNullException("controllerType");
            if (actionName == null) throw new ArgumentNullException("actionName");

            ControllerType = controllerType;
            ActionName = actionName;
        }

        public Type ControllerType { get; private set; }

        public string ActionName { get; private set; }

        public override string ToString()
        {
            return string.Format("ControllerType: {0}, ActionName: {1}", ControllerType, ActionName);
        }

        protected bool Equals(MvcAction other)
        {
            return ControllerType.Equals(other.ControllerType) && ActionName.EqualsIgnoreCase(other.ActionName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MvcAction) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (ControllerType.GetHashCode()*397) ^ ActionName.ToLowerInvariant().GetHashCode();
            }
        }
    }
}