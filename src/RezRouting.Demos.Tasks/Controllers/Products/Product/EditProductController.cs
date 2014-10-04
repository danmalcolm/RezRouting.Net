using System;
using System.Linq;
using System.Web.Mvc;
using RezRouting.Demos.Tasks.Controllers.Shared;
using RezRouting.Demos.Tasks.DataAccess;

namespace RezRouting.Demos.Tasks.Controllers.Products.Product
{
    public class EditProductController : TaskController
    {
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
            return DisplayTaskView(model);
        }

        public ActionResult Handle(EditInput input)
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
            return RedirectToAction("Show", "ShowProduct", new { id = product.Id });
        }
    }
}