using System;
using System.Linq;
using System.Web.Mvc;
using RezRouting.Demos.MvcWalkthrough2.DataAccess;

namespace RezRouting.Demos.MvcWalkthrough2.Controllers.Products
{
    public class CreateProductController : Controller
    {
        public ActionResult New()
        {
            return DisplayNewView(new CreateProductRequest());
        }

        private ActionResult DisplayNewView(CreateProductRequest request)
        {
            var model = new CreateModel
            {
                ManufacturerOptions = new SelectList(DemoData.Manufacturers, "Id", "Name", request.ManufacturerId),
                Request = request
            };
            return View("New", model);
        }

        public ActionResult Create(CreateProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return DisplayNewView(request);
            }

            var manufacturer = DemoData.Manufacturers.Single(x => x.Id == request.ManufacturerId);
            var product = new DataAccess.Product
            {
                Id = DemoData.Products.Count + 1,
                Name = request.Name,
                Manufacturer = manufacturer,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                IsActive = true
            };
            DemoData.Products.Add(product);

            TempData["alert-success"] = "Product Created";
            return RedirectToAction("Index");
        }
    }
}