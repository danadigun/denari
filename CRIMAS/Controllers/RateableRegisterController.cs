using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace CRIMAS.Controllers
{
    [Authorize]
    public class RateableRegisterController : Controller
    {
        private Models.CrimasDb db;
        public RateableRegisterController()
        {
            db = new CRIMAS.Models.CrimasDb();
        }
        //
        // GET: /RateableRegister/

        public ActionResult Index(int? page)
        {
            //check if there are any dividends in the db
            if (!db.CustomerSavings.Any(x=>x.TransactionMsg=="Dividends"))
            {
                return Redirect("~/Error/ErrorCode?ErrorCode=1586D");
            }

            var customers = db.Customers.ToList();

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(customers.ToPagedList(pageNumber, pageSize));

            return View();
        }
        public ActionResult ViewRateableRegister(string month)
        {          
            ViewBag.Month = month;
            return View();
        }

    }
}
