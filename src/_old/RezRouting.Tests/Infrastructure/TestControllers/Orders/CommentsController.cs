using System.Web.Mvc;

namespace RezRouting.Tests.Infrastructure.TestControllers.Orders
{
    public class CommentsController : Controller
    {
        public ActionResult Index(string orderId)
        {
            return null;
        }

        public ActionResult Show(string orderId, string commentId, string id)
        {
            return null;
        }

        public ActionResult New(string orderId, string commentId)
        {
            return null;
        }

        public ActionResult Create(object model, string commentId)
        {
            return null;
        }

        public ActionResult Edit(string orderId, string commentId, string id)
        {
            return null;
        }

        public ActionResult Update(object model)
        {
            return null;
        }

        public ActionResult Destroy(string orderId, string commentId, string id)
        {
            return null;
        }
    }
}