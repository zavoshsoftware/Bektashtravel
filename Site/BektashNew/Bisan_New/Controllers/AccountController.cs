using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web.Security;
using ViewModels;
using System.Text.RegularExpressions;
using CaptchaMvc.HtmlHelpers;
using Models;
using Helpers;

namespace Bisan.Controllers
{

    public class AccountController : Controller
    {

        private DatabaseContext db = new DatabaseContext();
        MenuHelper menu = new MenuHelper();
        public ActionResult Login(string ReturnUrl = "")
        {
            ViewBag.Message = "";
            ViewBag.ReturnUrl = ReturnUrl;
            LoginViewModel loginViewModel = new LoginViewModel();
            loginViewModel.Menu = menu.ReturnMenuTours();
            loginViewModel.Footer = menu.ReturnFooter();
            loginViewModel.MenuBlogGroups = menu.ReturnBlogGroups();
            return View(loginViewModel);

        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            LoginPageViewModel login = new LoginPageViewModel();
            login.login = model;
            LoginViewModel loginViewModel = new LoginViewModel();
          

                if (ModelState.IsValid)
                {
                    User oUser = db.Users.Where(a => a.Username == model.Username && a.Password == model.Password)
                        .FirstOrDefault();

                    if (oUser != null)
                    {
                        Role role = db.Roles.Find(oUser.RoleId);

                        var ident = new ClaimsIdentity(
                            new[]
                            {

                                new Claim(ClaimTypes.NameIdentifier, oUser.Username),
                                new Claim(
                                    "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                                    "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),

                                new Claim(ClaimTypes.Name, oUser.Username),


                                new Claim(ClaimTypes.Role, role.Name),

                            },
                            DefaultAuthenticationTypes.ApplicationCookie);

                        HttpContext.GetOwinContext().Authentication.SignIn(
                            new AuthenticationProperties {IsPersistent = true}, ident);
                        return RedirectToLocal(returnUrl, role.Name); // auth succeed 
                    }
                    else
                    {

                        TempData["WrongPass"] = "نام کاربری و یا کلمه عبور وارد شده صحیح نمی باشد.";
                    }
                }

         
                loginViewModel.Menu = menu.ReturnMenuTours();
                loginViewModel.Footer = menu.ReturnFooter();
                loginViewModel.MenuBlogGroups = menu.ReturnBlogGroups();
                return View(loginViewModel);
 
            
        }
        
        private ActionResult RedirectToLocal(string returnUrl,string role)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {

                if (role.ToLower().Contains("tour"))
                    return RedirectToAction("Index", "TourCategories");
                if (role.ToLower().Contains("hotel"))
                    return RedirectToAction("Index", "TourHotels");
                if (role.ToLower().Contains("admin"))
                    return RedirectToAction("Index", "TourCategories");
                else
                    return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult LogOff()
        {
            if (User.Identity.IsAuthenticated)
            {
                HttpContext.GetOwinContext().Authentication.SignOut();
            }
            return Redirect("/");
        }
    }
}
