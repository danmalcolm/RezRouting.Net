using System;
using System.Linq;
using System.Web.Mvc;
using RezRouting.Demos.MvcWalkthrough3.Controllers.Common;
using RezRouting.Demos.MvcWalkthrough3.DataAccess;
using RezRouting.Demos.MvcWalkthrough3.Utility;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Products.Product
{
    public class EditProductController : ItemTaskController<EditProductRequest,DataAccess.Product>
    {
        protected override void PrepareRequest(EditProductRequest request, DataAccess.Product product)
        {
            request.Id = product.Id;
            request.ManufacturerId = product.Manufacturer.Id;
            request.Name = product.Name;
        }

        protected override TaskModel<EditProductRequest> CreateModel(EditProductRequest request, DataAccess.Product entity)
        {
            var model = new EditProductModel
            {
                ManufacturerOptions = new SelectList(DemoData.Manufacturers, "Id", "Name", request.ManufacturerId),
                Request = request
            };
            return model;
        }

        protected override ActionResult ExecuteTask(EditProductRequest request, DataAccess.Product entity)
        {
            var manufacturer = DemoData.Manufacturers.Single(x => x.Id == request.ManufacturerId);
            var product = DemoData.Products.Single(x => x.Id == request.Id);
            product.Name = request.Name;
            product.Manufacturer = manufacturer;
            product.ModifiedOn = DateTime.Now;

            TempData["alert-success"] = "Product Updated";
            return Redirect(Url.ResourceUrl((ProductDetailsController c) => c.Show(product.Id)));
        }
    }
}