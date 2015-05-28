using System;
using System.Linq;
using System.Web.Mvc;
using RezRouting.Demos.MvcWalkthrough2.DataAccess;
using RezRouting.Demos.MvcWalkthrough2.Utility;

namespace RezRouting.Demos.MvcWalkthrough2.Controllers.Manufacturers.Manufacturer
{
    public class EditManufacturerController : Controller
    {
        public ActionResult Edit(int id)
        {
            var manufacturer = DemoData.Manufacturers.SingleOrDefault(x => x.Id == id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }

            var input = new EditManufacturerRequest
            {
                Id = manufacturer.Id, 
                Name = manufacturer.Name
            };
            return DisplayEditView(input);
        }

        private ActionResult DisplayEditView(EditManufacturerRequest request)
        {
            var model = new EditManufacturerModel
            {
                Request = request
            };
            return View("Edit", model);
        }

        public ActionResult Update(EditManufacturerRequest request)
        {
            if (!ModelState.IsValid)
            {
                return DisplayEditView(request);
            }

            var manufacturer = DemoData.Manufacturers.Single(x => x.Id == request.Id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            manufacturer.Name = request.Name;
            manufacturer.ModifiedOn = DateTime.Now;

            TempData["alert-success"] = "Manufacturer Updated";
            return Redirect(Url.ResourceUrl((ManufacturerDetailsController c) => c.Show(manufacturer.Id)));
        }
    }
}