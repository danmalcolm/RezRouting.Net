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

            object parentId;

            if (!RouteData.Values.TryGetValue("productId", out parentId))
                return null;
            int productId = (int) parentId;
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