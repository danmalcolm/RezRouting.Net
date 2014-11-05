using System.Web.Mvc;

namespace RezRouting.Tests.AspNetMvc.RouteTypes.Crud.TestModel.Controllers.Product
{
    public class ProductController : Controller
    {
        public ActionResult Show(string id)
        {
            return Content("");
        }

        public ActionResult Edit(string id)
        {
            return null;
        }

        public ActionResult Update(object input)
        {
            return null;
        }

        public ActionResult Delete(string id)
        {
            return null;
        }
    }
}