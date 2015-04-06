using MinakoNoTango.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinakoNoTango.Controllers
{
    public class TangoController : Controller
    {
        public ActionResult Index()
        {
            return View(new TangoView());
        }

        public ActionResult Add()
        {
            return View(new TangoView());
        }

    }
}
