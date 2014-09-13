using System.Linq;
using System.Web.Mvc;
using RezRouting.Demos.Crud.DataAccess;

namespace RezRouting.Demos.Crud.Controllers.Products
{
    public class ProductsController : Controller
    {
        public ActionResult Index()
        {
            var model = new IndexModel
            {
                Products = DemoData.Products
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
                DisplayNewView(input);
            }

            var manufacturer = DemoData.Manufacturers.Single(x => x.Id == input.ManufacturerId);
            var product = new DataAccess.Product
            {
                Id = DemoData.Products.Count + 1,
                Name = input.Name,
                Manufacturer = manufacturer
            };
            DemoData.Products.Add(product);

            TempData["info"] = "Product Created";
            return RedirectToAction("Index");
        }
    }
}