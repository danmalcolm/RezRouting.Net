using System.Web.Mvc;

namespace RezRouting.Demos.Crud.Controllers.Products
{
    /// <summary>
    /// Model used to display screen to create a product
    /// </summary>
    public class CreateModel
    {
        public SelectList ManufacturerOptions { get; set; }

        public CreateInput Input { get; set; } 
    }
}