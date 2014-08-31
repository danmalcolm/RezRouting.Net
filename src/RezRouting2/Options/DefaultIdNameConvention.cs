using RezRouting2.Utility;

namespace RezRouting2.Options
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
        /// <param name="fullNameForCurrent">Determines whether full name is used within routes at the current 
        /// resource level, e.g. products/{productId}</param>
        public DefaultIdNameConvention(string idName = null, bool fullNameForCurrent = false)
        {
            this.fullNameForCurrent = fullNameForCurrent;
            this.idName = idName ?? "id";
            this.idNamePascal = this.idName.Pascalize();
        }


        public string GetIdName(string resourceName)
        {
            return fullNameForCurrent ? FullIdName(resourceName) : idName;
        }

        public string GetIdNameAsAncestor(string resourceName)
        {
            return FullIdName(resourceName);
        }

        private string FullIdName(string resourceName)
        {
            return resourceName.Camelize() + idNamePascal;
        }
    }
}