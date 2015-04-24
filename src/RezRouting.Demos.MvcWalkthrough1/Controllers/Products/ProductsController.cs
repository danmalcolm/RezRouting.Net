using System;
using System.Linq;
using System.Web.Mvc;
using RezRouting.Demos.MvcWalkthrough1.DataAccess;

namespace RezRouting.Demos.MvcWalkthrough1.Controllers.Products
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
            var product = new Product
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

        public ActionResult Show(int id)
        {
            var product = DemoData.Products.SingleOrDefault(x => x.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var reviews = DemoData.Reviews.Where(x => x.Product == product)
                .OrderByDescending(x => x.ReviewDate)
                .Take(3)
                .ToList();
            var model = new ProductDetailsModel
            {
                Product = product,
                Reviews = reviews
            };
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var product = DemoData.Products.SingleOrDefault(x => x.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }

            var input = new EditInput
            {
                Id = product.Id,
                ManufacturerId = product.Manufacturer.Id,
                Name = product.Name
            };
            return DisplayEditView(input);
        }

        private ActionResult DisplayEditView(EditInput input)
        {
            var model = new EditModel
            {
                ManufacturerOptions = new SelectList(DemoData.Manufacturers, "Id", "Name", input.ManufacturerId),
                Input = input
            };
            return View("Edit", model);
        }

        public ActionResult Update(EditInput input)
        {
            if (!ModelState.IsValid)
            {
                return DisplayEditView(input);
            }

            var manufacturer = DemoData.Manufacturers.Single(x => x.Id == input.ManufacturerId);
            var product = DemoData.Products.Single(x => x.Id == input.Id);
            product.Name = input.Name;
            product.Manufacturer = manufacturer;
            product.ModifiedOn = DateTime.Now;

            TempData["alert-success"] = "Product Updated";
            return RedirectToAction("Show", new { id = product.Id });
        }

        public ActionResult Delete(int id)
        {
            var product = DemoData.Products.SingleOrDefault(x => x.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }

            product.IsActive = false;

            TempData["alert-success"] = "Product Deleted";
            return RedirectToAction("Index", "Products");
        }
    }
}