using System.Linq;
using System.Web.Mvc;
using RezRouting.Demos.MvcWalkthrough1.DataAccess;

namespace RezRouting.Demos.MvcWalkthrough1.Controllers.Reviews
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

        public ActionResult Show(int productId, int id)
        {
            var review = DemoData.Reviews
                .SingleOrDefault(x => x.Product.Id == productId && x.Id == id);
            if (review == null)
            {
                return HttpNotFound();
            }

            var model = new ReviewDetailsModel
            {
                Review = review
            };
            return View(model);
        } 
    }
}