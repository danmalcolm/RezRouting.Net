using System;
using System.Linq;
using System.Web.Mvc;
using RezRouting.Demos.MvcWalkthrough3.Controllers.Common;
using RezRouting.Demos.MvcWalkthrough3.DataAccess;
using RezRouting.Demos.MvcWalkthrough3.Utility;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Products.Product
{
    public class PublishProductController : ItemTaskController<PublishProductRequest,DataAccess.Product>
    {
        protected override void PrepareRequest(PublishProductRequest request, DataAccess.Product product)
        {
            request.Id = product.Id;
        }

        protected override ActionResult ExecuteTask(PublishProductRequest request, DataAccess.Product entity)
        {
            var product = DemoData.Products.Single(x => x.Id == request.Id);
            product.Published = true;
            product.PublishComments = request.PublishComments;

            TempData["alert-success"] = "Product Updated";
            return Redirect(Url.ResourceUrl((ProductDetailsController c) => c.Show(product.Id)));
        }
    }
}