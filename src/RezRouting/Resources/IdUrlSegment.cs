using RezRouting.Configuration;

namespace RezRouting.Resources
{
    /// <summary>
    /// An element within a resource URL template that contains the id of a resource
    /// </summary>
    public class IdUrlSegment : IUrlSegment
    {
        /// <summary>
        /// Creates a new IdUrlSegment
        /// </summary>
        /// <param name="idName"></param>
        /// <param name="idNameAsAncestor"></param>
        public IdUrlSegment(string idName, string idNameAsAncestor)
        {
            IdName = idName;
            IdNameAsAncestor = idNameAsAncestor;
        }

        /// <summary>
        /// The name of the id parameter placeholder that identifies the current resource
        /// </summary>
        public string IdName { get; private set; }

        /// <summary>
        /// The name of the id parameter placeholder that identifies the current resource
        /// within the URL for a child resource
        /// </summary>
        public string IdNameAsAncestor { get; private set; }

        /// <inheritdoc />
        public string Path
        {
            get
            {
                return string.Concat("{", IdName + "}");
            }
        }

        /// <inheritdoc />
        public string PathAsAncestor
        {
            get
            {
                return string.Concat("{", IdNameAsAncestor + "}");
            }
        }
    }
}