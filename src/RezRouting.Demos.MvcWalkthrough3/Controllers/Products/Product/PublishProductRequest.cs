namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Products.Product
{
    /// <summary>
    /// The values submitted to publish a product
    /// </summary>
    public class PublishProductRequest
    {
        public int Id { get; set; }

        public string PublishComments { get; set; }
    }
}