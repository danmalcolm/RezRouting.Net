using System.ComponentModel.DataAnnotations;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Products.Product
{
    /// <summary>
    /// The values submitted to edit a product
    /// </summary>
    public class EditProductRequest
    {
        public int Id { get; set; }

        [Required]
        public int? ManufacturerId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}