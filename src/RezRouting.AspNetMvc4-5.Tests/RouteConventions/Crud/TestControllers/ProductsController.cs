using System.Web.Mvc;

namespace RezRouting.AspNetMvc.Tests.RouteConventions.Crud.TestControllers
{
    public class ProductsController : Controller
    {
        public ActionResult Index()
        {
            return Content("");
        }

        public ActionResult New()
        {
            return null;
        }

        public ActionResult Create(object input)
        {
            return null;
        }
    }
}