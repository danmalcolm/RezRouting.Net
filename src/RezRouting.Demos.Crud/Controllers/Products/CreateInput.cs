using System.ComponentModel.DataAnnotations;

namespace RezRouting.Demos.Crud.Controllers.Products
{
    /// <summary>
    /// The values submitted to create a product
    /// </summary>
    public class CreateInput
    {
        [Required]
        public int? ManufacturerId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; }
    }
}