using System.Linq;
using System.Web.Mvc;
using RezRouting.Demos.Crud.DataAccess;

namespace RezRouting.Demos.Crud.Controllers.Home
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new HomeModel { LatestProducts = DemoData.Products.OrderBy(x => x.CreatedOn).Take(3).ToList() };
            return View(model);
        }
    }
}