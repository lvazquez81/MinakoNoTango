using MinakoNoTangoLib.Entities;
using MinakoNoTangoLib.Library;
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
        private IMinakoNoTangoLibrary _lib;
        private SecurityToken _token;

        public HomeController()
        {
            _token = new SecurityToken()
            {
                Username = "Luis",
                ExpirationDate = DateTime.Now.AddDays(1),
                Token = "abc123"
            };

            var repository = new MemoryRepository(1);
            _lib = new MinakoNoTangoLibrary(repository);
        }

        public ActionResult Index()
        {
            IList data = _lib.GetAllPhrases(_token).ToList();
            return View(data);
        }

        [HttpGet]
        public ActionResult AddPhrase()
        {
            return View(new PhraseEntity());
        }

        [HttpPost]
        public ActionResult Save(PhraseEntity viewPhrase)
        {
            PhraseEntity phrase = _lib.AddEnglishPhrase(_token, viewPhrase.Expression);
            if (phrase != null)
            {
                IList data = _lib.GetAllPhrases(_token).ToList();
                return View("Index", data);
            }
            else
            {
                return View(phrase);
            }
            
        }

        [HttpGet]
        public ActionResult ViewPhrase(int idPhrase = -1)
        {
            PhraseEntity phrase = _lib.GetPhraseDetail(_token, idPhrase);
            return View("Detail", phrase);
        }

    }
}
