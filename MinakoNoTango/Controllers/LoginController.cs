using MinakoNoTango.Models;
using MinakoNoTangoLib.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinakoNoTango.Controllers
{
    public class LoginController : Controller
    {
        private LoginModel _login;

        public LoginController()
        {
           _login = new LoginModel();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(new LoginView());
        }

        [HttpPost]
        public ActionResult Index(LoginView form)
        {
            return View();
        }

        public ActionResult Logout()
        {
            return View("Index", new LoginView());
        }


    }
}
