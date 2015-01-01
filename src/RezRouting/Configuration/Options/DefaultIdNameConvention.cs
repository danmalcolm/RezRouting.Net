using RezRouting.Utility;

namespace RezRouting.Configuration.Options
{
    /// <summary>
    /// Built-in strategy used to generate id component within route URLs
    /// </summary>
    public class DefaultIdNameConvention : IIdNameConvention
    {
        private readonly bool fullNameForCurrent;
        private readonly string idName;
        private readonly string idNamePascal;

        /// <summary>
        /// Creates a new DefaultIdNameConvention
        /// </summary>
        /// <param name="idName">Sets the name of the identifier parameter, e.g. "id"</param>
        /// <param name="fullNameForCurrent">Determines whether full name is used within routes at the current 
        /// resource type, e.g. products/{productId}</param>
        public DefaultIdNameConvention(string idName = null, bool fullNameForCurrent = false)
        {
            this.fullNameForCurrent = fullNameForCurrent;
            this.idName = idName ?? "id";
            this.idNamePascal = this.idName.Pascalize();
        }

        /// <inheritdoc />
        public string GetIdName(string resourceName)
        {
            return fullNameForCurrent ? FullIdName(resourceName) : idName;
        }

        /// <inheritdoc />
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