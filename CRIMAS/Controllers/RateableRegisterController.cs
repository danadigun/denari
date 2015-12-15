using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRIMAS.Controllers
{
    public class RateableRegisterController : Controller
    {
        private Models.CrimasDb db;
        public RateableRegisterController()
        {
            db = new CRIMAS.Models.CrimasDb();
        }
        //
        // GET: /RateableRegister/

        public ActionResult Index()
        {
            //check if there are any dividends in the db
            if (db.CustomerSavings.Where(x=>x.TransactionMsg=="Dividends").ToList().Count() == 0)
            {
                return Redirect("~/Error/ErrorCode?ErrorCode=1586D");
            }
            return View();
        }
        public ActionResult ViewRateableRegister(string month)
        {          
            ViewBag.Month = month;
            return View();
        }

    }
}
