using System;
using System.Web.Mvc;
using RezRouting.Demos.MvcWalkthrough2.DataAccess;

namespace RezRouting.Demos.MvcWalkthrough2.Controllers.Manufacturers
{
    public class CreateManufacturerController : Controller
    {
        public ActionResult New()
        {
            return DisplayNewView(new CreateManufacturerRequest());
        }

        private ActionResult DisplayNewView(CreateManufacturerRequest manufacturerRequest)
        {
            var model = new CreateModel
            {
                ManufacturerRequest = manufacturerRequest
            };
            return View("New", model);
        }

        public ActionResult Create(CreateManufacturerRequest manufacturerRequest)
        {
            if (!ModelState.IsValid)
            {
                return DisplayNewView(manufacturerRequest);
            }

            var manufacturer = new DataAccess.Manufacturer
            {
                Id = DemoData.Manufacturers.Count + 1,
                Name = manufacturerRequest.Name,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
            };
            DemoData.Manufacturers.Add(manufacturer);

            TempData["alert-success"] = "Manufacturer Created";
            return RedirectToAction("Index");
        }
    }
}