namespace RezRouting
{
    public class DirectoryUrlSegment : IUrlSegment
    {
        public DirectoryUrlSegment(string path)
        {
            Path = path;
        }

        public string Path { get; private set; }

        public string PathAsAncestor
        {
            get { return Path; }
        }
    }
}