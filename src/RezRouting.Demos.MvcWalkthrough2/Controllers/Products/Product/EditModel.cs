using System.Web.Mvc;

namespace RezRouting.Demos.MvcWalkthrough2.Controllers.Products.Product
{
    /// <summary>
    /// Model used to display screen to create a product
    /// </summary>
    public class EditModel
    {
        public SelectList ManufacturerOptions { get; set; }

        public EditInput Input { get; set; } 
    }
}