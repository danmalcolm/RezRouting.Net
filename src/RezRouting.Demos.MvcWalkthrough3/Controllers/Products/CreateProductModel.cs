using System.Web.Mvc;
using RezRouting.Demos.MvcWalkthrough3.Controllers.Common;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Products
{
    /// <summary>
    /// Model used to display screen to create a product
    /// </summary>
    public class CreateProductModel : TaskModel<CreateProductRequest>
    {
        public SelectList ManufacturerOptions { get; set; }
    }
}