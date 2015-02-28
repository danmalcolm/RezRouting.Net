using System;
using System.Linq;
using System.Web.Mvc;
using RezRouting.Demos.Crud.DataAccess;

namespace RezRouting.Demos.Crud.Controllers.Manufacturers
{
    public class ManufacturersController : Controller
    {
        public ActionResult Index()
        {
            var model = new ManufacturersIndexModel
            {
                Manufacturers = DemoData.Manufacturers.ToList()
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

            var manufacturer = new DataAccess.Manufacturer
            {
                Id = DemoData.Manufacturers.Count + 1,
                Name = input.Name,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
            };
            DemoData.Manufacturers.Add(manufacturer);

            TempData["alert-success"] = "Manufacturer Created";
            return RedirectToAction("Index");
        }
    }
}