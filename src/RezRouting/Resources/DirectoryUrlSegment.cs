using RezRouting.Configuration;

namespace RezRouting.Resources
{
    /// <summary>
    /// Represents a fixed directory path within a Resource URL template
    /// </summary>
    public class DirectoryUrlSegment : IUrlSegment
    {
        /// <summary>
        /// Creates a DirectoryUrlSegment
        /// </summary>
        /// <param name="path"></param>
        public DirectoryUrlSegment(string path)
        {
            Path = path;
        }

        /// <inheritdoc />
        public string Path { get; private set; }

        /// <inheritdoc />
        public string PathAsAncestor
        {
            get { return Path; }
        }
    }
}