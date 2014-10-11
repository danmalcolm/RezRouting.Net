using System;
using System.Linq;
using System.Web.Mvc;
using RezRouting.Demos.Crud.DataAccess;

namespace RezRouting.Demos.Crud.Controllers.Products
{
    public class ProductsController : Controller
    {
        public ActionResult Index()
        {
            var model = new ProductsIndexModel
            {
                Products = DemoData.Products.Where(x => x.IsActive).ToList()
            };
            return View(model);
        }

        public ActionResult New()
        {
            return DisplayNewView(new CreateInput());
        }

        private ActionResult DisplayNewView(CreateInput input)
        {
            var model = new CreateModel
            {
                ManufacturerOptions = new SelectList(DemoData.Manufacturers, "Id", "Name", input.ManufacturerId),
                Input = input
            };
            return View("New", model);
        }

        public ActionResult Create(CreateInput input)
        {
            if (!ModelState.IsValid)
            {
                return DisplayNewView(input);
            }

            var manufacturer = DemoData.Manufacturers.Single(x => x.Id == input.ManufacturerId);
            var product = new DataAccess.Product
            {
                Id = DemoData.Products.Count + 1,
                Name = input.Name,
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