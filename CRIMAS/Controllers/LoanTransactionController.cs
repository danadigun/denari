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

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /LoanTransaction/Create
        [HttpPost]
        public ActionResult CheckActiveLoan(string accountNo)
        {
            var activeloans = db.Loans.Where(x => x.AccountNo == accountNo).Where(x => x.LoanStatus == "active").ToList();

            return Json(new { Loans = activeloans });
        }
        
        [HttpPost]
        public ActionResult add(int loanId, decimal amount)
        {
            var loan = db.Loans.SingleOrDefault(x=>x.Id==loanId);
            if(loan != null)
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
                return Json(true);
            }
            return Json(false);
        }
        [HttpPost]
        public ActionResult Create(LoanTransaction loantransaction)
        {
            ViewBag.AccountNo = loantransaction.AccountNo;

            var customer = db.Customers.Where(m => m.AccountNo == loantransaction.AccountNo);

            if (ModelState.IsValid)
            {
                if (customer != null)
                {
                    loantransaction.DateCreated = DateTime.Now.ToShortDateString();
                    loantransaction.Cr = 0;
                    loantransaction.createdby = User.Identity.Name.ToString();

                    db.LoanTransactions.Add(loantransaction);
                    db.SaveChanges();

                    return Json(new { Message = "Successfully made loan repayment of " + loantransaction.Dr + " for customer : "+loantransaction.AccountNo });
                }
                else
                {
                    return Json(new { Error = "Unable to make loan repayment" });
                }
              
            }

            return View(loantransaction);
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