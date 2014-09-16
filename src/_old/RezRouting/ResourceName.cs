using System;
using RezRouting.Utility;

namespace RezRouting
{
    /// <summary>
    /// Contains properties that relate to the name of a resource, used in route names, URLs and parameters
    /// </summary>
    public class ResourceName : IEquatable<ResourceName>
    {
        public ResourceName(string singular = null, string plural = null)
        {
            if(string.IsNullOrWhiteSpace(singular) && string.IsNullOrWhiteSpace(plural))
                throw new ArgumentException("Both singular or plural values cannot be null or empty. At least one valid value must be supplied");
            Singular = singular ?? plural.Singularize(Plurality.Plural);
            Plural = plural ?? singular.Pluralize(Plurality.Singular);
        }

        public string Singular { get; private set; } 

        public string Plural { get; private set; }

        public bool Equals(ResourceName other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Singular, other.Singular) && string.Equals(Plural, other.Plural);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ResourceName) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Singular.GetHashCode()*397) ^ Plural.GetHashCode();
            }
        }

        public static bool operator ==(ResourceName left, ResourceName right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ResourceName left, ResourceName right)
        {
            return !Equals(left, right);
        }
    }
}