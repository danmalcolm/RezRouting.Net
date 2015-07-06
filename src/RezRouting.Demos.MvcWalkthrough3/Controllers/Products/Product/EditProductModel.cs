using System.Web.Mvc;
using RezRouting.Demos.MvcWalkthrough3.Controllers.Common;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Products.Product
{
    /// <summary>
    /// Model used to display screen to edit a product
    /// </summary>
    public class EditProductModel : TaskModel<EditProductRequest>
    {
        public SelectList ManufacturerOptions { get; set; }
    }
}