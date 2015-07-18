using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using RezRouting.Demos.MvcWalkthrough3.Controllers.Common;
using RezRouting.Demos.MvcWalkthrough3.DataAccess;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Session
{
    public class SignInController : TaskController<Credentials>
    {
        protected override ActionResult ExecuteTask(Credentials credentials)
        {
            var user =
                DemoData.Users.SingleOrDefault(
                    x => x.UserName == credentials.UserName && x.Password == credentials.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "User details not recognised. Please try again.");
                return DisplayForm(credentials);
            }
            FormsAuthentication.SetAuthCookie(user.UserName, false);
            return RedirectToAction("Show", "SessionDetails");
        }
    }
}