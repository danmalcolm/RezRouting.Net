using System.Linq;
using System.Web.Mvc;
using RezRouting.Demos.MvcWalkthrough3.Controllers.Common;
using RezRouting.Demos.MvcWalkthrough3.DataAccess;
using RezRouting.Demos.MvcWalkthrough3.Utility;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Products.Product
{
    public class ArchiveProductController : ItemTaskController<ArchiveProductRequest,DataAccess.Product>
    {
        protected override void PrepareRequest(ArchiveProductRequest request, DataAccess.Product product)
        {
            request.Id = product.Id;
        }

        protected override ActionResult ExecuteTask(ArchiveProductRequest request, DataAccess.Product entity)
        {
            var product = DemoData.Products.Single(x => x.Id == request.Id);
            product.Published = false;
            product.ArchiveComments = request.ArchiveComments;

            TempData["alert-success"] = "Product archived";
            return Redirect(Url.ResourceUrl((ProductDetailsController c) => c.Show(product.Id)));
        }
    }
}