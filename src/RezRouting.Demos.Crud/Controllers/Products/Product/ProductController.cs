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
    }
}