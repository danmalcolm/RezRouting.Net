using System;
using System.Linq;
using System.Web.Mvc;
using RezRouting.Demos.Tasks.Controllers.Shared;
using RezRouting.Demos.Tasks.DataAccess;

namespace RezRouting.Demos.Tasks.Controllers.Products
{
    public class CreateProductController : TaskController
    {
        public ActionResult Edit()
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
            return DisplayTaskView(model);
        }

        public ActionResult Handle(CreateInput input)
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
            return RedirectToAction("Index", "ListProducts");
        }
    }
}