namespace RezRouting2
{
    public class RouteBuilder
    {
        private string httpMethod = "GET";
        private string path = "";
        private string action = "";
        private string name;
        private bool skip;

        public void Name(string name)
        {
            this.name = name;
        }

        public void HttpMethod(string method)
        {
            httpMethod = method;
        }

        public void Path(string path)
        {
            this.path = path;
        }

        public void Action(string action)
        {
            this.action = action;
        }

        public Route Build()
        {
            if (skip) return null;

            return new Route(name);
        }

        public void Skip()
        {
            this.skip = true;
        }
    }
}