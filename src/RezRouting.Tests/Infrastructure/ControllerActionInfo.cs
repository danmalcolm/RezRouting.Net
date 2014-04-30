using System;
using System.Linq;
using System.Web.Routing;

namespace RezRouting.Tests.Infrastructure
{
    /// <summary>
    /// Contains a controller and action names - mainly used to parse values from strings in format "controller#action"
    /// that are used to make test code more concise
    /// </summary>
    public class ControllerActionInfo : IEquatable<ControllerActionInfo>
    {
        /// <summary>
        /// Extracts controller and action from a string like "controller#action"
        /// </summary>
        public static ControllerActionInfo Parse(string controllerAction)
        {
            var parts = controllerAction.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
            if(parts.Length != 2 || parts.Any(string.IsNullOrWhiteSpace))
                throw new ArgumentException("Expected value in format controller#action", "controllerAction");
            return new ControllerActionInfo(parts[0], parts[1]);
        }

        public ControllerActionInfo(RouteValueDictionary values)
        {
            string controller = values["controller"] as string;
            string action = values["action"] as string;
            if(string.IsNullOrWhiteSpace(controller) || string.IsNullOrWhiteSpace(action))
                throw new ArgumentException("controller and action values not found", "values");

            Controller = controller;
            Action = action;
            Name = string.Format("{0}#{1}", controller, action);
        }

        private ControllerActionInfo(string controller, string action)
        {
            Controller = controller;
            Action = action;
            Name = string.Format("{0}#{1}", controller, action);
        }

        public string Name { get; private set; }

        public string Controller { get; private set; }

        public string Action { get; private set; }

        public bool Equals(ControllerActionInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ControllerActionInfo) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.ToLowerInvariant().GetHashCode() : 0);
        }

        public static bool operator ==(ControllerActionInfo left, ControllerActionInfo right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ControllerActionInfo left, ControllerActionInfo right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}