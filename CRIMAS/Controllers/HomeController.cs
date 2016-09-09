using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRIMAS.SupportClasses;
using CRIMAS.Models;
using CRIMAS.Services;
using CRIMAS.Repository;
using CRIMAS.Models.ViewModels;

namespace CRIMAS.Controllers
{
    public class HomeController : Controller
    {
        private ICurrency _currency;
        private CrimasDb _context;
        private ILoan _loan;

        public HomeController(ICurrency currency,CrimasDb context,ILoan loan)
        {
            _currency = currency;
           _context = context;
            _loan = loan;
        }

        /*Cashier DashBoard*/
        [Authorize(Roles="Cashier")]
        public ActionResult Cashier()
        {
           
            _currency.SetCurrencyToNaira();

            var activeLoanAgreements = _loan.getAll().Where(p => p.LoanStatus == "active");

            return View(activeLoanAgreements);
        }

        /*Supervisor DashBoard*/
        [Authorize(Roles = "Supervisor")]
        public ActionResult Supervisor()
        {
            return View();
        }

        /*Manager DashBoard*/
        [Authorize(Roles = "Manager")]
        public ActionResult Manager()
        {
            return View();
        }

        /*
         * Denari - Admin DashBoard. Display dashboard data according to year.
         * Default to current year if no year is passed as parameter
         */
        [Authorize(Roles = "Admin")]      
        public ActionResult Admin(int? year)
        {
            var viewModel = new AdminManagementViewModel();
            if (year.HasValue)
            {              
                populateAdminViewModel(year, viewModel);
                return View(viewModel);
            }
            else
            {
                year = DateTime.Now.Year;

                populateAdminViewModel(year, viewModel);
                return View(viewModel);

            }         

        }

        /// <summary>
        /// Populates Admin-View-Model
        /// </summary>
        /// <param name="year">year of report</param>
        /// <param name="viewModel">viewmodel to populate</param>
        private void populateAdminViewModel(int? year, AdminManagementViewModel viewModel)
        {

            #region Loan Management

                 #region all loan applications
                 viewModel.total_loanApplications = _context.Loans.Where(x=>x.DateCreated.Year==year).ToList()
                                .Select(x=>x.amount).Sum();

                  #endregion

                 #region completed loans
                     viewModel.total_CompletedLoans = _context.Loans.Where(x=>x.LoanStatus=="completed").ToList()
                            .Where(x=>x.DateCreated.Year==year).Select(x=>x.amount).Sum();
                 #endregion

                 #region outstanding loans

                    viewModel.total_OutstandingLoans = _context.Loans.Where(x => x.LoanStatus == "active").ToList()
                                                       .Where(x=>x.DateCreated.Year==year).Select(x => x.amount).Sum();

            #endregion

            #endregion

            #region Customer Management

                 #region total customers
                 viewModel.total_customers = _context.Customers.Count();
            #endregion

                 #region Customer Savings
          
                 var credit   = _context.CustomerSavings.Where(x => x.DateCreated.Year == year).ToList().Select(x => x.Credit).Sum();
                 var debit = _context.CustomerSavings.Where(x => x.DateCreated.Year == year).ToList().Select(x => x.Debit).Sum();
                 viewModel.total_savingsBalance = credit - debit;

            #endregion

            #endregion

            #region Dividends management

                #region Dividends Paid
                     viewModel.total_DividendsPaid = _context.DividendSummary.ToList()
                                        .Where(x=>x.dateCreated.Year==year)
                                        .Select(x => x.total_amount).Sum();
                #endregion

                #region percentage dividend
             viewModel.total_percentageDividends = _context.Dividends.ToList()
                                        .Where(x => x.dateCreated.Year == year)
                                        .Select(x => x.percentage).Sum();
            #endregion

            #endregion

            #region profit
            var loanRepayment = _context.LoanTransactions.Where(x => x.DateCreated.Contains(year.ToString())).ToList()
                                        .Where(x => x.Narration == "Loan Repayment").ToList()
                                        .Select(x => x.Cr).Sum();
            viewModel.total_loanRepayment = loanRepayment;
            viewModel.total_profit = loanRepayment - viewModel.total_loanApplications;
            #endregion
        }

        public ActionResult Index()
        {
            return Redirect("~/Account/Login");
        }

        public ActionResult SendEmail(string email)
        {
            ViewBag.IsMailSent = EmailHelper.SendMail(email, "Email Subject", "EmailBody");
            return View();
        }

    }
}
