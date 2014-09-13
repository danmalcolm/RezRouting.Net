using System.Linq;
using System.Web.Mvc;
using RezRouting.Demos.Crud.DataAccess;

namespace RezRouting.Demos.Crud.Controllers.Products.Product
{
    public class ProductController : Controller
    {
        public ActionResult Show(int id)
        {
            var product = DemoData.Products.SingleOrDefault(x => x.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var model = new ShowModel
            {
                Product = product
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
                DisplayEditView(input);
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
            return RedirectToAction("Show", new { id = product.Id });
        }
    }
}