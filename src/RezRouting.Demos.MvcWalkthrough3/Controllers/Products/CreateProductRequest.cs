using System.ComponentModel.DataAnnotations;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Products
{
    /// <summary>
    /// The values submitted to create a product
    /// </summary>
    public class CreateProductRequest
    {
        [Required]
        public int? ManufacturerId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}