using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Model;
using Service;
using System.Web.Security;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        public HomeController()
        {
            _userService = new UserService();
        }
        public ActionResult Index(string uname,string pwd)
        {
            User user = new User { username = uname, password = pwd };
            user = _userService.GetUserDetails(user);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(uname, false);
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, uname, DateTime.Now, DateTime.Now.AddMinutes(20), false, user.Roles);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);
                ViewBag.Message = "Welcome," + uname;
            }
            else
            {
                ViewBag.Message = "Invalid Login Attempt";   
            }
            return View();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("About", "Home");
        }

        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}