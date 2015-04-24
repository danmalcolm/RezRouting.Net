using System.Linq;
using System.Web.Mvc;
using RezRouting.Demos.MvcWalkthrough1.DataAccess;

namespace RezRouting.Demos.MvcWalkthrough1.Controllers.Home
{
    public class HomeController : Controller
    {
        public ActionResult Show()
        {
            var model = new HomeModel { LatestProducts = DemoData.Products.OrderBy(x => x.CreatedOn).Take(3).ToList() };
            return View(model);
        }
    }
}