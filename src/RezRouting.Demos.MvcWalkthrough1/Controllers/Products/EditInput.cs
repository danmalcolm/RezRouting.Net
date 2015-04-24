using System.ComponentModel.DataAnnotations;

namespace RezRouting.Demos.MvcWalkthrough1.Controllers.Products
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