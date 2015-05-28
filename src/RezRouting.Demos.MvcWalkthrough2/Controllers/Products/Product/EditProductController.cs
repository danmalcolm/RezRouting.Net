using System;
using System.Linq;
using System.Web.Mvc;
using RezRouting.Demos.MvcWalkthrough2.DataAccess;
using RezRouting.Demos.MvcWalkthrough2.Utility;

namespace RezRouting.Demos.MvcWalkthrough2.Controllers.Products.Product
{
    public class EditProductController : Controller
    {
        public ActionResult Edit(int id)
        {
            var product = DemoData.Products.SingleOrDefault(x => x.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }

            var input = new EditProductRequest
            {
                Id = product.Id, 
                ManufacturerId = product.Manufacturer.Id, 
                Name = product.Name
            };
            return DisplayEditView(input);
        }

        private ActionResult DisplayEditView(EditProductRequest request)
        {
            var model = new EditProductModel
            {
                ManufacturerOptions = new SelectList(DemoData.Manufacturers, "Id", "Name", request.ManufacturerId),
                Request = request
            };
            return View("Edit", model);
        }

        public ActionResult Update(EditProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return DisplayEditView(request);
            }

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