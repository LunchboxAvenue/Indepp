using Indepp.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Indepp.Controllers
{
    public class AccountController : Controller
    {
        IAuthenticationManager Authentication
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginDetails loginDetails)
        {
            if (ModelState.IsValid)
            {
                var user = ConfigurationManager.AppSettings["ADMIN_USER"];
                var password = ConfigurationManager.AppSettings["ADMIN_PASSWORD"];

                bool isAdmin = loginDetails.User.Equals(user) && loginDetails.Password.Equals(password);

                if (isAdmin)
                {
                    var identity = new ClaimsIdentity(new[] {
                            new Claim(ClaimTypes.Name, loginDetails.User),
                        },
                        DefaultAuthenticationTypes.ApplicationCookie,
                        ClaimTypes.Name, ClaimTypes.Role);

                    identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                    // tell OWIN the identity provider, optional
                    // identity.AddClaim(new Claim(IdentityProvider, "Simplest Auth"));

                    Authentication.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = loginDetails.RememberMe
                    }, identity);

                    return RedirectToAction("Index", "Admin");
                }
            }

            ViewBag.WrongCredentials = "Please check your user/password.";
            return View(loginDetails);
        }

        public ActionResult Logout()
        {
            Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
    }
}