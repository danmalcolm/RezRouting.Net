using System.Web.Mvc;
using RezRouting2.AspNetMvc.RouteTypes.Crud;

namespace RezRouting2.Tests.AspNetMvc.RouteTypes.Crud
{
    public static class TestResourceModel
    {
        public static RouteMapper Configure()
        {
            var mapper = new RouteMapper();
            mapper.RouteTypes(CrudRouteTypes.All);
            mapper.Collection("Products", products =>
            {
                products.HandledBy<ProductsController>();
                products.Items(product => product.HandledBy<ProductController>());
            });
            mapper.Singular("Profile", profile => profile.HandledBy<ProfileController>());

            return mapper;
        }
    }

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

    public class ProfileController : Controller
    {
        public ActionResult New()
        {
            return null;
        }

        public ActionResult Create(object input)
        {
            return null;
        }

        public ActionResult Show()
        {
            return null;
        }

        public ActionResult Edit()
        {
            return null;
        }

        public ActionResult Update(object input)
        {
            return null;
        }

        public ActionResult Delete()
        {
            return null;
        }
    }
}