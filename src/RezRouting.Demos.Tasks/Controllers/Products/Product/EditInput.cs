using System.ComponentModel.DataAnnotations;

namespace RezRouting.Demos.Tasks.Controllers.Products.Product
{
    /// <summary>
    /// The values submitted to edit a product
    /// </summary>
    public class EditInput
    {
        public int Id { get; set; }

        [Required]
        public int? ManufacturerId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}