namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Products.Product
{
    /// <summary>
    /// The values submitted to archive a product
    /// </summary>
    public class ArchiveProductRequest
    {
        public int Id { get; set; }

        public string ArchiveComments { get; set; }
    }
}