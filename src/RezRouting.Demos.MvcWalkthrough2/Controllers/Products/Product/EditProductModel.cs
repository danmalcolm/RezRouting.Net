using System.Web.Mvc;

namespace RezRouting.Demos.MvcWalkthrough2.Controllers.Products.Product
{
    /// <summary>
    /// Model used to display screen to create a product
    /// </summary>
    public class EditProductModel
    {
        public SelectList ManufacturerOptions { get; set; }

        public EditProductRequest Request { get; set; } 
    }
}