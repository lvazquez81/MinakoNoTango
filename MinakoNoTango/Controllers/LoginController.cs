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
        public ActionResult IndexGet()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IndexPost()
        {
            return View();
        }


    }
}
