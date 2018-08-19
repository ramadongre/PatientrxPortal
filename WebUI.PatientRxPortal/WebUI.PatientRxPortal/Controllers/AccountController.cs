using DataContracts.PatientPortal;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebUI.PatientRxPortal.Models;

namespace WebUI.PatientRxPortal.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        /// <summary>
        /// Redirect to local method.
        /// </summary>
        /// <param name="returnUrl">Return URL parameter.</param>
        /// <returns>Return redirection action</returns>
        private ActionResult RedirectToLocal(string returnUrl)
        {
            try
            {
                // Verification.
                if (Url.IsLocalUrl(returnUrl))
                {
                    // Info.
                    return this.Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                // Info
                throw ex;
            }

            // Info.
            return this.RedirectToAction("ManagePatients", "Home");
        }

        [AllowAnonymous]
        // GET: Account
        public ActionResult login(string returnUrl)
        {
            try
            {
                // Verification.
                if (this.Request.IsAuthenticated)
                {
                    // Info.
                    return this.RedirectToLocal(returnUrl);
                }
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // Info.
            return this.View();
        }

        public ActionResult logout()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;

            // Sign Out.
            authenticationManager.SignOut();

            return RedirectToAction("Login", "Account");
        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                DataService.Patientportal.PatientPortalService cls = new DataService.Patientportal.PatientPortalService();

                try
                {
                    string userName = model.UserName;
                    string password = model.Password;

                    CommonStatus cs = cls.VerifyLoginUser(userName, Utilities.PatientRxPortal.Helper.CreateMD5(password));

                    if (cs != null && cs.OpStatus)
                    {
                        PortalUser u = (PortalUser)cs.OpPayload;

                        var claims = new List<Claim>();

                        claims.Add(new Claim(ClaimTypes.NameIdentifier, userName));
                        claims.Add(new Claim(ClaimTypes.Name, u.DisplayName));
                        claims.Add(new Claim(ClaimTypes.GivenName, u.DisplayName));

                        claims.Add(new Claim("UserID", u.PortalUser_ID.ToString()));

                        claims.Add(new Claim(ClaimTypes.Hash, u.HashedPassword));

                        var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                        var ctx = Request.GetOwinContext();
                        var authenticationManager = ctx.Authentication;
                        authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, claimIdenties);

                        return RedirectToAction("ManagePatients", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid user-name or password");
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    cls = null;
                }

                return View();
            }

            return View();
        }
    }
}