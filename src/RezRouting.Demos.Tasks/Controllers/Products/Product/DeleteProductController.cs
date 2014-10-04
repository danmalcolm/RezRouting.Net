using System.Linq;
using System.Web.Mvc;
using RezRouting.Demos.Tasks.Controllers.Shared;
using RezRouting.Demos.Tasks.DataAccess;

namespace RezRouting.Demos.Tasks.Controllers.Products.Product
{
    public class DeleteProductController : TaskController
    {
        public ActionResult Handle(int id)
        {
            var product = DemoData.Products.SingleOrDefault(x => x.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }

            product.IsActive = false;

            TempData["alert-success"] = "Product Deleted";
            return RedirectToAction("Index", "ListProducts");
        }
    }
}