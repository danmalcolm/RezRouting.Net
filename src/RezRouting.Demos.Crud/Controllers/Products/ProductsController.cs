using System.Web.Mvc;
using System.Web.UI.WebControls;
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
    }
}