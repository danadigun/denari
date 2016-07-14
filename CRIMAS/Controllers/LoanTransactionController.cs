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
        public ActionResult Create(LoanTransaction loantransaction, string accountNO)
        {
            ViewBag.AccountNo = accountNO;

            var CustomerAccount = db.Customers.Where(m => m.AccountNo == loantransaction.AccountNo);

            if (ModelState.IsValid)
            {
                if (CustomerAccount.Count() != 0)
                {
                    loantransaction.DateCreated = DateTime.Now.ToShortDateString();
                    loantransaction.amount = 0;
                    loantransaction.createdby = User.Identity.Name.ToString();

                    db.LoanTransactions.Add(loantransaction);
                    db.SaveChanges();
                    return Redirect("~/LoanTransaction/Details/"+loantransaction.id);
                }
                else
                {
                    return Redirect("~/CustomerSavings/AccountNotFound");
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