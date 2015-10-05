using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRIMAS.SupportClasses;
using CRIMAS.Models;
using CRIMAS.Services;
using CRIMAS.Repository;

namespace CRIMAS.Controllers
{
    public class HomeController : Controller
    {
        private ICurrency _currency;
        private CrimasDb _context;
        private ILoan _loan;

        public HomeController(
            ICurrency currency,
            CrimasDb context, 
            ILoan loan
            )
        {
            _currency = currency;
            _context = context;
            _loan = loan;
        }
        /*CRIMAS Webportal - Cashier DashBoard*/
        [Authorize(Roles="Cashier")]
        public ActionResult Cashier()
        {
           
            _currency.SetCurrencyToNaira();

            var activeLoanAgreements = _loan.getAll().Where(p => p.LoanStatus == "active");

            return View(activeLoanAgreements);
        }

        /*CRIMAS Webportal - Supervisor DashBoard*/
        [Authorize(Roles = "Supervisor")]
        public ActionResult Supervisor()
        {
            return View();
        }

        /*CRIMAS Webportal - Manager DashBoard*/
        [Authorize(Roles = "Manager")]
        public ActionResult Manager()
        {
            return View();
        }

        /*CRIMAS Webportal - Admin DashBoard*/
        [Authorize(Roles = "Admin")]
        [Authorize]
        public ActionResult Admin()
        {
            return View();
        }


        public ActionResult Index()
        {
            return Redirect("~/Account/Login");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}
