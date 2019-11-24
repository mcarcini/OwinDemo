using OwinDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace OwinDemo.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            if (login.Username.Equals("marco") && login.Password.Equals("123"))
            {
                var identity = new ClaimsIdentity("ApplicationCookie");
                identity.AddClaims(new List<Claim>{
                    new Claim(ClaimTypes.NameIdentifier, login.Username),
                    new Claim(ClaimTypes.Name, login.Username)
                });
                HttpContext.GetOwinContext().Authentication.SignIn(identity); 
            }
            return View(login);
        }

        public ActionResult LogOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }

    }
}