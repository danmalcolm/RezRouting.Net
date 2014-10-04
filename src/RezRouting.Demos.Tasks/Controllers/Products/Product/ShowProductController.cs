using System.Linq;
using System.Web.Mvc;
using RezRouting.Demos.Tasks.Controllers.Shared;
using RezRouting.Demos.Tasks.DataAccess;

namespace RezRouting.Demos.Tasks.Controllers.Products.Product
{
    public class ShowProductController : TaskController
    {
        public ActionResult Show(int id)
        {
            var product = DemoData.Products.SingleOrDefault(x => x.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var model = new DetailsModel
            {
                Product = product
            };
            return DisplayTaskView(model);
        }
    }
}