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
        private IRepository _repository;

        public HomeController()
        {
            _token = new SecurityToken()
            {
                Username = "Luis",
                ExpirationDate = DateTime.Now.AddDays(1),
                Token = "abc123"
            };

            _repository = new MemoryRepository();
            if (_repository.GetAll().Count == 0)
            {
                _repository.Add(_token.Username, "hitori bochi yoru", LanguageType.English, "Probado el sistema.");
                _repository.Add(_token.Username, "I am hungry", LanguageType.English, "Probado el sistema.");
            }

            _teacher = new TeacherModel(_repository, _token);
        }

        #region Index
        /// <summary>
        /// Home
        /// </summary>
        [HttpGet]
        public ActionResult Index()
        {
            return View(_teacher);
        }
        #endregion

        #region View
        /// <summary>
        /// Home
        /// </summary>
        [HttpGet]
        public ActionResult View(int Id)
        {
            _teacher.LoadViewedExpression(Id);
            return View(viewName: "Detail", model: _teacher.ViewedExpression);
        }
        #endregion

        #region Add
        [HttpGet]
        public ActionResult Add()
        {
            return View("Add", new PhraseEntity());
        }

        [HttpPost]
        public ActionResult Add(PhraseEntity phrase)
        {
            StudentModel student = new StudentModel(_repository, _token);

            if (student.SaveExpression(phrase))
            {
                return View("Index", new TeacherModel(_repository, _token));
            }
            else
            {
                return View("Add", student);
            }
        }
        #endregion
    }
}
