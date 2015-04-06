using MinakoNoTango.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinakoNoTango.Controllers
{
    public class PhraseController : Controller
    {
        public ActionResult Index()
        {
            return View(new PhraseView());
        }

        public ActionResult Add()
        {
            return View(new PhraseView());
        }

        public ActionResult Detail()
        {
            return View(new PhraseView());
        }

    }
}
