using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRIMAS.Controllers
{
    public class RateableRegisterController : Controller
    {
        //
        // GET: /RateableRegister/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewRateableRegister(string month)
        {          
            ViewBag.Month = month;
            return View();
        }

    }
}
