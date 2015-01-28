using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RasPiCam.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Login(string username, string password)
        {
            if (Membership.ValidateUser(username, password))
            {
                FormsAuthentication.SetAuthCookie(username, false);
                return Json(ResultWithException.Success);
            }
            return Json(new ResultWithException(new SecurityException("Incorrect username or password")));
        }

        public ActionResult Logout()
        {
            // TODO: Make this a POST form so that user can't be logged out by XSRF
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Default");
        }
    }
}