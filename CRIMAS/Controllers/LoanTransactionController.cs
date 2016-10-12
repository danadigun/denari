using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRIMAS.Models;

namespace CRIMAS.Controllers
{
    
    [Authorize]
    public class LoanTransactionController : Controller
    {
        private CrimasDb db = new CrimasDb();

        //
        // GET: /LoanTransaction/

        public ActionResult Index()
        {
            return View(db.LoanTransactions.ToList());
        }

        //
        // GET: /LoanTransaction/Details/5

        public ActionResult Details(int id = 0)
        {
            LoanTransaction loantransaction = db.LoanTransactions.Find(id);
            if (loantransaction == null)
            {
                return HttpNotFound();
            }
            return View(loantransaction);
        }

        //
        // GET: /LoanTransaction/Create

        public ActionResult Create(string accountNo = "")
        {
            ViewBag.AccountNo = accountNo;
            return View();
        }

        //
        // POST: /LoanTransaction/Create
        [HttpPost]
        public ActionResult CheckActiveLoan(string accountNo)
        {
            var activeloans = db.Loans.Where(x => x.AccountNo == accountNo).Where(x => x.LoanStatus == "active").ToList();
            var Customer_Id = db.Customers.FirstOrDefault(x => x.AccountNo == accountNo).CustomerId;

            return Json(new {CustomerId = Customer_Id,  Loans = activeloans });
        }
        
        [HttpPost]
        public ActionResult add(int loanId, decimal amount)
        {
            var loan = db.Loans.SingleOrDefault(x=>x.Id==loanId);
        
            if(loan != null)
            {
                var total_cr = db.LoanTransactions.Where(x => x.Loan.Id == loanId).Select(x => x.Cr).Sum();
                var total_dr = db.LoanTransactions.Where(x => x.Loan.Id == loanId).Select(x => x.Dr).Sum();

                if(total_cr > total_dr)
                {
                    db.LoanTransactions.Add(new LoanTransaction
                    {
                        AccountNo = loan.AccountNo,
                        Cr = 0,
                        createdby = User.Identity.Name,
                        DateCreated = DateTime.Now.ToLongDateString(),
                        Dr = amount,
                        Loan = loan,
                        Narration = "Loan Repayment"
                    });
                    db.SaveChanges();

                    var cr = db.LoanTransactions.Where(x => x.Loan.Id == loanId).Select(x => x.Cr).Sum();
                    var dr = db.LoanTransactions.Where(x => x.Loan.Id == loanId).Select(x => x.Dr).Sum();
                    if (cr <= dr)
                    {
                        loan.LoanStatus = "Completed";
                        db.SaveChanges();
                    }
                }
                else
                {
                    loan.LoanStatus = "Completed";
                    db.SaveChanges();
                }
               
                return Json(true);
            }
            return Json(false);
        }

        
        //
        // GET: /LoanTransaction/Edit/5

        public ActionResult Edit(int id = 0)
        {
            LoanTransaction loantransaction = db.LoanTransactions.Find(id);
            if (loantransaction == null)
            {
                return HttpNotFound();
            }
            return View(loantransaction);
        }

        //
        // POST: /LoanTransaction/Edit/5

        [HttpPost]
        public ActionResult Edit(LoanTransaction loantransaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loantransaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loantransaction);
        }

        //
        // GET: /LoanTransaction/Delete/5

        public ActionResult Delete(int id = 0)
        {
            LoanTransaction loantransaction = db.LoanTransactions.Find(id);
            if (loantransaction == null)
            {
                return HttpNotFound();
            }
            return View(loantransaction);
        }

        //
        // POST: /LoanTransaction/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            LoanTransaction loantransaction = db.LoanTransactions.Find(id);
            string CustomerAccountNo = (from s in db.LoanTransactions where s.id == id select s.AccountNo).FirstOrDefault();

            db.LoanTransactions.Remove(loantransaction);
            db.SaveChanges();
            return Redirect("/Loan/CheckLoanStatus?accountno="+CustomerAccountNo);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}