using System.Web.Mvc;

namespace RezRouting2.Tests.AspNetMvc.RouteTypes.Crud.TestModel.Controllers.Products
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