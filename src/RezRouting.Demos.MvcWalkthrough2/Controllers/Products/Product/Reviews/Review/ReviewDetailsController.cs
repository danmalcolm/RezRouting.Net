using System.Linq;
using RezRouting.Demos.MvcWalkthrough2.Controllers.Common;
using RezRouting.Demos.MvcWalkthrough2.DataAccess;

namespace RezRouting.Demos.MvcWalkthrough2.Controllers.Products.Product.Reviews.Review
{
    public class ReviewDetailsController : EntityDetailsController<DataAccess.Review,ReviewDetailsModel>
    {
        protected override DataAccess.Review GetEntity(int id)
        {
            // Slightly clunky way of loading a child resource - there are
            // plenty of interesting ways in which this could be improved

            int productId;

            if (!RouteData.Values.ContainsKey("productId")
                || !int.TryParse((string) RouteData.Values["productId"], out productId))
            {
                return null;
            }
            var review = DemoData.Reviews
                .SingleOrDefault(x => x.Product.Id == productId && x.Id == id);
            return review;
        }

        protected override ReviewDetailsModel CreateModel(DataAccess.Review entity)
        {
            return new ReviewDetailsModel
            {
                Review = entity
            };
        }
    }
}