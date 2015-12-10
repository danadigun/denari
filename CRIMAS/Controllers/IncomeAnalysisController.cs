using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace CRIMAS.Controllers
{
    public class IncomeAnalysisController : Controller
    {
        //
        // GET: /IncomeAnalysis/

        public ActionResult Index()
        {
            SetCurrencyToNaira();
            return View();
        }

        //
        //GET: /IncomeAnalysis/ViewIncomeAnalysis?month="January"&year="1999"
        public ActionResult ViewIncomeAnalysis(string month)
        {
            SetCurrencyToNaira();
            ViewBag.Month = month;
            
            return View();
        }

        private static void SetCurrencyToNaira()
        {
            //Set Culture info for currency
            CultureInfo cultureInfo = new CultureInfo("en-US");
            cultureInfo.NumberFormat.CurrencySymbol = "₦";

            Thread.CurrentThread.CurrentCulture = cultureInfo;
        }
    }
}
