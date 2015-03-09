using MinakoNoTangoLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinakoNoTango.Controllers
{
    public class HomeController : Controller
    {
        private List<PhraseEntity> getPhrases()
        {
            return new List<PhraseEntity>()
            {
                new PhraseEntity(){ Id = 1, EnglishPhrase = "Good morning!", AuthorName = "Minako" },
                new PhraseEntity(){ Id = 2, JapansePhrase = "Konnichi wa!", AuthorName = "Chibi" },
                new PhraseEntity(){ Id = 3, JapansePhrase = "Dokka e onsen e ikimashouka", AuthorName = "Chibi" },
                new PhraseEntity(){ Id = 4, EnglishPhrase = "I love to take photos", AuthorName = "Minako" }
            };
        }

        public ActionResult Index()
        {
            return View(getPhrases());
        }

        [HttpGet]
        public ActionResult AddPhrase()
        {
            return View();
        }

    }
}
