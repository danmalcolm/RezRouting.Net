using RezRouting.Utility;

namespace RezRouting.Configuration
{
    public class DefaultIdNameConvention : IIdNameConvention
    {
        private readonly bool fullNameForCurrent;
        private readonly string idName;
        private readonly string idNamePascal;

        /// <summary>
        /// The base name used for the identity, e.g. "id"
        /// </summary>
        /// <param name="idName"></param>
        /// <param name="fullNameForCurrent"></param>
        public DefaultIdNameConvention(string idName = null, bool fullNameForCurrent = false)
        {
            this.fullNameForCurrent = fullNameForCurrent;
            this.idName = idName ?? "id";
            this.idNamePascal = this.idName.Pascalize();
        }


        public string GetIdName(ResourceName resourceName)
        {
            return fullNameForCurrent ? FullIdName(resourceName) : idName;
        }

        public string GetIdNameAsAncestor(ResourceName resourceName)
        {
            return FullIdName(resourceName);
        }

        private string FullIdName(ResourceName resourceName)
        {
            return resourceName.Singular.Camelize() + idNamePascal;
        }
    }
}