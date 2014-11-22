using System.Web.Mvc;
using RezRouting.AspNetMvc.RouteConventions.Crud;
using RezRouting.Configuration;

namespace RezRouting.Tests.AspNetMvc.RouteConventions.Tasks
{
    public static class TestResourceModel
    {
        public static RouteMapper Configure()
        {
            var mapper = new RouteMapper();
            mapper.RouteConventions(new CrudRouteConventionBuilder().Build());
            mapper.Collection("Products", products =>
            {
                products.HandledBy<ProductsController>();
                products.Items(product => product.HandledBy<ProductController>());
            });
            mapper.Singular("Profile", profile => profile.HandledBy<ProfileController>());

            return mapper;
        }
    }


    public interface ICollectionTaskController {}
    public interface ICollectionItemTaskController {}
    public interface ISingularTaskController {}

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

    public class NewProductController : Controller
    {
        public ActionResult Edit()
        {
            return null; 
        }

        public ActionResult Handle()
        {
            return null; 
        }
    }

    public class ProductController : Controller
    {
        public ActionResult Show(string id)
        {
            return null;
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