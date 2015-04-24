using System.Linq;
using System.Web.Mvc;
using RezRouting.Demos.MvcWalkthrough2.DataAccess;

namespace RezRouting.Demos.MvcWalkthrough2.Controllers.Products.Product.Reviews
{
    public class ReviewsController : Controller
    {
        public ActionResult Index(int productId)
        {
            var product = DemoData.Products.SingleOrDefault(x => x.Id == productId);
            if (product == null)
            {
                return HttpNotFound();
            }

            var reviews = DemoData.Reviews
                .Where(x => x.Product.Id == productId)
                .OrderByDescending(x => x.ReviewDate)
                .ToList();

            var model = new ReviewsIndexModel
            {
                Product = product,
                Reviews = reviews
            };
            return View(model);
        } 
    }
}