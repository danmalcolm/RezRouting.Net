namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Common
{
    public class EntityCriteria
    {
        public EntityCriteria()
        {
            PageSize = 20;
            Keyword = "";
        }

        public int Page { get; set; } 

        public int PageSize { get; set; }

        public string Keyword { get; set; }
    }
}