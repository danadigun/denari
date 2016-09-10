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
    [Authorize]
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
                viewModel.year = year.Value;            
                populateAdminViewModel(year, viewModel);
                return View(viewModel);
            }
            else
            {
                year = DateTime.Now.Year;
                viewModel.year = year.Value;

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
            viewModel.year = year.Value;

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

            #region profit monthly
            
            viewModel.profit_jan = getmonthlyProfit(1, year.Value);
            viewModel.profit_feb = getmonthlyProfit(2, year.Value);
            viewModel.profit_mar = getmonthlyProfit(3, year.Value);
            viewModel.profit_apr = getmonthlyProfit(4, year.Value);
            viewModel.profit_may = getmonthlyProfit(5, year.Value);
            viewModel.profit_jun = getmonthlyProfit(6, year.Value);
            viewModel.profit_jul = getmonthlyProfit(7, year.Value);
            viewModel.profit_aug = getmonthlyProfit(8, year.Value);
            viewModel.profit_sept = getmonthlyProfit(9, year.Value);
            viewModel.profit_oct = getmonthlyProfit(10, year.Value);
            viewModel.profit_nov = getmonthlyProfit(11, year.Value);
            viewModel.profit_dec = getmonthlyProfit(12, year.Value);

            #endregion

            #region top five borrowers
                 viewModel.outstanding_LoanApplicants = getTopLoanApplicants(year.Value);
            #endregion
        }
        /// <summary>
        /// returns monthly profit within a particular year
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        private decimal getmonthlyProfit(int month, int year)
        {
            var repayments = _context.LoanTransactions.Where(x => x.DateCreated.Contains(year.ToString())).ToList()
                                    .Where(x => x.Narration == "Loan Repayment").ToList();

            var monthly_loans_applied = _context.Loans.Where(x => x.DateCreated.Year == year).ToList()
                                        .Where(x => x.DateCreated.Month == month).Select(x => x.amount).Sum();

            var monthly_repayment = (from s in repayments where Convert.ToDateTime(s.DateCreated).Month == month select s.Cr).ToList().Sum();

            var profit = monthly_repayment - monthly_loans_applied;

            return Convert.ToDecimal(profit);

        }

        /// <summary>
        /// returns the top five borrowers
        /// </summary>
        /// <returns></returns>
        private List<Customer> getTopLoanApplicants(int year)
        {
            //get all loan applications that are active
            var activeLoans = _context.Loans.Where(x => x.LoanStatus == "active")
                .Where(x => x.DateCreated.Year == year)
                .OrderBy(x => x.amount).Take(5).ToList();

            //get all top five customers
            List<Customer> topFiveBorrowers = new List<Customer>();
            foreach(var loan in activeLoans)
            {
                var customer = _context.Customers.Where(x => x.AccountNo == loan.AccountNo).FirstOrDefault();
                topFiveBorrowers.Add(customer);
            }
            return topFiveBorrowers;
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
