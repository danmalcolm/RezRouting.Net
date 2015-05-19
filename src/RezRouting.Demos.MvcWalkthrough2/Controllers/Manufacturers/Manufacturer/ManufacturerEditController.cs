using System;
using System.Linq;
using System.Web.Mvc;
using RezRouting.Demos.MvcWalkthrough2.DataAccess;

namespace RezRouting.Demos.MvcWalkthrough2.Controllers.Manufacturers.Manufacturer
{
    public class ManufacturerEditController : Controller
    {
        public ActionResult Edit(int id)
        {
            var manufacturer = DemoData.Manufacturers.SingleOrDefault(x => x.Id == id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }

            var input = new EditInput
            {
                Id = manufacturer.Id, 
                Name = manufacturer.Name
            };
            return DisplayEditView(input);
        }

        private ActionResult DisplayEditView(EditInput input)
        {
            var model = new EditModel
            {
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

            var manufacturer = DemoData.Manufacturers.Single(x => x.Id == input.Id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            manufacturer.Name = input.Name;
            manufacturer.ModifiedOn = DateTime.Now;

            TempData["alert-success"] = "Manufacturer Updated";
            return RedirectToAction("Show", new { id = manufacturer.Id });
        }
    }
}