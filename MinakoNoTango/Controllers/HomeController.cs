using MinakoNoTangoLib.Entities;
using MinakoNoTangoLib.Library;
using MinakoNoTangoLib.Library.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinakoNoTango.Controllers
{
    public class HomeController : Controller
    {
        private TeacherModel _teacher;
        private SecurityToken _token;

        public HomeController()
        {
            _token = new SecurityToken()
            {
                Username = "Luis",
                ExpirationDate = DateTime.Now.AddDays(1),
                Token = "abc123"
            };

            var repository = new MemoryRepository();
            repository.Add(_token.Username, "hitori bochi yoru", LanguageType.English, "Probado el sistema.");
            repository.Add(_token.Username, "I am hungry", LanguageType.English, "Probado el sistema.");


            _teacher = new TeacherModel(repository, _token);
        }

        /// <summary>
        /// Home
        /// </summary>
        [HttpGet]
        public ActionResult Index()
        {
            return View(_teacher);
        }

        /// <summary>
        /// Home
        /// </summary>
        [HttpGet]
        public ActionResult ViewDetail(int Id)
        {
            _teacher.LoadViewedExpression(Id);
            return View(viewName: "ViewDetail", model: _teacher.ViewedExpression);
        }
    }
}
