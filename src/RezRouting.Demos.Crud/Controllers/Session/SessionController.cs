﻿using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using RezRouting.Demos.Crud.DataAccess;

namespace RezRouting.Demos.Crud.Controllers.Session
{
    public class SessionController : Controller
    {
        public ActionResult New()
        {
            return DisplayNewView();
        }

        public ActionResult Create(Credentials credentials)
        {
            if (!ModelState.IsValid)
            {
                return DisplayNewView(credentials);
            }
            var user =
                DemoData.Users.SingleOrDefault(
                    x => x.UserName == credentials.UserName && x.Password == credentials.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Not recognised");
                return DisplayNewView(credentials);
            }
            FormsAuthentication.SetAuthCookie(user.UserName, false);
            return RedirectToAction("Show");
        }

        private ActionResult DisplayNewView(Credentials credentials = null)
        {
            return View("New", credentials);
        }

        public ActionResult Show()
        {
            return View("Show");
        }

        public ActionResult Delete()
        {
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.LoginUrl);
        }
    }
}