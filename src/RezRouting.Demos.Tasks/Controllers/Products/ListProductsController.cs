using System.Linq;
using System.Web.Mvc;
using RezRouting.Demos.Tasks.Controllers.Shared;
using RezRouting.Demos.Tasks.DataAccess;

namespace RezRouting.Demos.Tasks.Controllers.Products
{
    public class ListProductsController : TaskController
    {
        public ActionResult Index()
        {
            var model = new DisplayModel
            {
                Products = DemoData.Products.Where(x => x.IsActive).ToList()
            };
            return DisplayTaskView(model);
        }
    }
}