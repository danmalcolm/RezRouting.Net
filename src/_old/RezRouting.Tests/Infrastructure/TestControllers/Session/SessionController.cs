using System.Web.Mvc;

namespace RezRouting.Tests.Infrastructure.TestControllers.Session
{
    /// <summary>
    /// Controller with all standard singular resource actions - a shared controller
    /// for use in route setup tests
    /// </summary>
    public class SessionController : Controller
    {
        public ActionResult Show()
        {
            return null;
        }

        public ActionResult New()
        {
            return null;
        }

        public ActionResult Create()
        {
            return null;
        }

        public ActionResult Edit()
        {
            return null;
        }

        public ActionResult Update()
        {
            return null;
        }

        public ActionResult Destroy()
        {
            return null;
        }

    }
}