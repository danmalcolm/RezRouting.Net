using System.Web.Mvc;
using System.Web.Security;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Session
{
    public class SignOutController : Controller
    {
        public ActionResult Handle()
        {
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.LoginUrl);
        } 
    }
}