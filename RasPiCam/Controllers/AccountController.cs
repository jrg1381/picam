using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers;
using System.Web.Security;

namespace RasPiCam.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            // TODO: return JSON to help the client render a useful response
            if (Membership.ValidateUser(username, password))
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }

            MembershipCreateStatus x = new MembershipCreateStatus();
            var guid = Guid.NewGuid();
            Membership.CreateUser(username, password, "nobody@example.com", null, null, true, guid, out x);

            return RedirectToAction("Index", "Default");
        }

        public ActionResult Logout()
        {
            // TODO: Make this a POST form so that user can't be logged out by XSRF
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Default");
        }
    }
}