namespace RezRouting2
{
    public class SingularBuilder : ResourceBuilder
    {
        public SingularBuilder(string name) : base(name, ResourceLevel.Singular)
        {
            
        }

        public void UrlPath(string path)
        {
            UrlSegment = new DirectoryUrlSegment(path);
        }
    }
}