using System;
using System.Linq;
using System.Web.Mvc;
using RezRouting.Demos.MvcWalkthrough3.Controllers.Common;
using RezRouting.Demos.MvcWalkthrough3.DataAccess;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Products
{
    public class CreateProductController : TaskController<CreateProductRequest>
    {
        protected override TaskModel<CreateProductRequest> CreateModel(CreateProductRequest request)
        {
            return new CreateProductModel
            {
                ManufacturerOptions = new SelectList(DemoData.Manufacturers, "Id", "Name", request.ManufacturerId),
                Request = request
            };
        }

        protected override ActionResult ExecuteTask(CreateProductRequest request)
        {
            var manufacturer = DemoData.Manufacturers.Single(x => x.Id == request.ManufacturerId);
            var product = new DataAccess.Product
            {
                Id = DemoData.Products.Count + 1,
                Name = request.Name,
                Manufacturer = manufacturer,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                Published = false
            };
            DemoData.Products.Add(product);

            TempData["alert-success"] = "Product Created";
            return RedirectToIndex<ProductIndexController>();
        }
    }
}