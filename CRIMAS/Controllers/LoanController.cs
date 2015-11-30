using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRIMAS.Models;
using System.Globalization;
using System.Threading;
using CRIMAS.Services;

namespace CRIMAS.Controllers
{
    public class LoanController : Controller
    {
        private CrimasDb _context;
        private ICurrency _currency;


        public LoanController(CrimasDb context, ICurrency currency)
        {
            _context = context;
            _currency = currency;
        }
        //
        // GET: /Loan/
        public ActionResult Index()
        {
            //NOTE: Loan status can either be "active" or "completed"

            string cashier = User.Identity.Name.ToString();

            var UserLoanAgreements = from s in _context.Loans
                                     where s.LoanStatus == "active"
                                     select s;

            return View(UserLoanAgreements.ToList());
            //return View(db.Loans.ToList());
        }
        //
        // GET: /Loan/Details/5
        public ActionResult Details(int id = 0)
        {
            Loan loan = _context.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }
        //
        // GET: /Loan/Create
        public ActionResult Create()
        {
            return View();
        }
        //
        // POST: /Loan/Create
        [HttpPost]
        public ActionResult Create(Loan loan)
        {

            if (ModelState.IsValid)
            {
                loan.LoanStatus = "active"; //set loan status to active

                loan.Customername =
                    (from s in _context.Customers
                     where s.AccountNo == loan.AccountNo
                     select s.Name).FirstOrDefault();
                if (loan.Customername == null || loan.Customername == "") 
                {
                    //return customer not registered errorr:09256A 
                    return Redirect("~/Error/ErrorCode?ErrorCode=09256A");
                }
                //if we get here customer has been enrolled
                loan.createdby = User.Identity.Name.ToString();
                //loan.DateCreated = DateTime.Now.ToShortDateString();
                loan.DateCreated = DateTime.Now;

                //DateTime completionDate = DateTime.Parse(loan.DateOfCommencement).AddMonths(Int32.Parse(loan.Duration));
                DateTime completionDate = loan.DateOfCommencement.AddMonths(Int32.Parse(loan.Duration));

                //loan.DateOfTermination = completionDate.ToShortDateString();
                loan.DateOfTermination = completionDate;

                //save amount borrowed
                var amountBorred = new Borrowed
                {
                    DateCreated = DateTime.Now.ToShortDateString(),
                    accountNo = loan.AccountNo,
                    amountborrowed = loan.amount
                };

                //create a loan transaction record             
                var loanTransaction = new LoanTransaction
                {
                    DateCreated = DateTime.Now.ToShortDateString(),
                    AccountNo = loan.AccountNo,
                    amount = loan.amount,
                    refund = decimal.Parse("0"),//credit
                    createdby = User.Identity.Name.ToString()
                };

                //customer must pay 10% of loan upfront. create loan interest record.
                var loanInterest = new LoanInterest
                {
                    accountNo = loan.AccountNo,
                    intrestAmount = Decimal.Parse("0.1") * loan.amount,
                };
                _context.LoanInterests.Add(loanInterest);
                _context.Borrows.Add(amountBorred);
                _context.Loans.Add(loan);
                _context.LoanTransactions.Add(loanTransaction);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loan);
        }
        //
        // GET: /Loan/CreditLoan
        public ActionResult CreditLoan()
        {
            return View();
        }
        //
        // POST: /Loan/CreditLoan
        [HttpPost]
        public ActionResult CreditLoan(LoanTransaction loan)
        {


            var customerAccount = _context.CustomerSavings.Where(m => m.AccountNo == loan.AccountNo);

            if (ModelState.IsValid)
            {
                foreach (var item in customerAccount)
                {
                    if (item.AccountNo == null)
                    {
                        return Redirect("~/CustomerSavings/AccountNotFound");
                    }
                }
                loan.DateCreated = DateTime.Now.ToShortDateString();
                loan.createdby = User.Identity.Name.ToString();

                _context.LoanTransactions.Add(loan);
                _context.SaveChanges();
            }
            return View("LoanCreditSuccess");
        }
        //
        //GET: /Loan/LoanCreditSuccess
        public ActionResult LoanCreditSuccess()
        {
            return View();
        }
        //
        //GET: /Loan/EnterAccountNo
        public ActionResult EnterAccountNo()
        {
            return View();
        }
        //
        //GET: /Loan/CheckLoanStatus
        public ActionResult CheckLoanStatus(string accountNo)
        {
            //set currency to naira
            _currency.SetCurrencyToNaira();

            ViewBag.AccountNo = accountNo;
            ViewBag.NoOfLoanAgreements = _context.Loans.Where(s => s.AccountNo == accountNo).Count();

            var totalCredits = (from s in _context.LoanTransactions where s.AccountNo == accountNo select s.amount);
            var totalDebits = (from s in _context.LoanTransactions where s.AccountNo == accountNo select s.refund);

            //check if materialized value is null 
            if (totalCredits.Count() == 0)
            {
                //return empty loan repo errorr:02547B 
                return Redirect("~/Error/ErrorCode?ErrorCode=02547B");
            }
            ViewBag.totalCredits = totalCredits.Sum();
            ViewBag.totalDebits = totalDebits.Sum();

            
            //var LoanStatus = from s in db.Loans
            //                 join n in db.Borrows on s.Id equals n.id
            //                 select new
            //                 {
            //                     accountNo = s.AccountNo,
            //                     AmountBorrowed = n.amountborrowed
            //                 };
            var BorrowedAmount = from s in _context.Borrows where s.accountNo == accountNo select s.amountborrowed;

            //if customer has never borrowed
            if (BorrowedAmount.Count() == 0)
            {
                //return empty loan repo errorr:02547B 
                return Redirect("~/Error/ErrorCode?ErrorCode=02547B");
            }
            //if we get here customer has loaned cash
            var AllBorrowed = new List<String>();

            foreach (var item in BorrowedAmount)
            {
                AllBorrowed.Add(string.Format("{0:C}", item));
            }

            ViewBag.AmountBorrowed = AllBorrowed.ElementAt(AllBorrowed.Count() - 1);

            //Change customer's loan status to "completed" if balance is 0
            var balance = totalCredits.Sum() - totalDebits.Sum();

            if (balance <= 0)
            {
                //change the loan aggreement status to "completed"
                var CurrentLoan = (from s in _context.Loans
                                   where s.AccountNo == accountNo
                                   orderby s.DateCreated descending
                                   select s).First<Loan>();

                CurrentLoan.LoanStatus = "completed";

                _context.SaveChanges();
            }

            return View(_context.LoanTransactions.Where(s => s.AccountNo == accountNo).ToList());
        }

       
        //
        // GET: /Loan/Edit/5
        public ActionResult Edit(int id = 0)
        {
            Loan loan = _context.Loans.Find(id);

            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }
        //
        // POST: /Loan/Edit/5
        [HttpPost]
        public ActionResult Edit(Loan loan)
        {
            if (ModelState.IsValid)
            {
                //DateTime completionDate = DateTime.Parse(loan.DateOfCommencement).AddMonths(Int32.Parse(loan.Duration));
                //loan.DateOfTermination = completionDate.ToShortDateString();
                _context.Entry(loan).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loan);
        }
        //
        // GET: /Loan/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Loan loan = _context.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }
        //
        // POST: /Loan/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Loan loan = _context.Loans.Find(id);
            _context.Loans.Remove(loan);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }
    }
}