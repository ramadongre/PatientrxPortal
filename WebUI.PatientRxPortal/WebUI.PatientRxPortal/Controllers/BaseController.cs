using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace WebUI.PatientRxPortal.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;

            // Redirect on error:
            filterContext.Result = RedirectToAction("Index", "ErrorHandler");

            // OR set the result without redirection:
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/ErrorHandler/Index.cshtml"
            };
        }

        public int GetLoggedinUserID()
        {
            int uid = 0;

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var uidentity = (ClaimsIdentity)HttpContext.User.Identity;

                if (uidentity.HasClaim(a => a.Type == "UserID"))
                    int.TryParse(uidentity.FindFirst(a => a.Type == "UserID").Value, out uid);
            }

            return uid;
        }
    }
}