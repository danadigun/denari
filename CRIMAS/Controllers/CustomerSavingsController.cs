using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRIMAS.Models;
using CRIMAS.Models.ViewModels;
using CRIMAS.Repository.artifacts;

namespace CRIMAS.Controllers
{
    [Authorize]
    public class CustomerSavingsController : Controller
    {
        private CrimasDb db = new CrimasDb();

        //
        // GET: /CustomerSavings/

        public ActionResult Index()
        {
            return View(db.CustomerSavings.ToList());
        }

        //
        //GET: /CustomerSavings/CheckAccount/?accountno=0898763
        //
        public ActionResult CheckAccount(string accountno)
        {
           
            CheckAccountViewModel viewmodel = new CheckAccountViewModel();

            var customerSavings = db.CustomerSavings.Where(x => x.AccountNo == accountno).ToList();
            var orderedSavings = db.CustomerSavings.Where(x => x.AccountNo == accountno).OrderByDescending(x => x.DateCreated);
            var customerName = db.Customers.FirstOrDefault(x => x.AccountNo == accountno).Name;
            var customerDividend = (from d in db.Dividends where d.accountNo == accountno select (Decimal?)d.amount).Sum() ?? 0;
            var customer = db.Customers.Where(x => x.AccountNo == accountno).SingleOrDefault();
            var dividend_list = db.Dividends.Where(x => x.accountNo == accountno).ToList();

            viewmodel.account = accountno;
            viewmodel.name = customerName;
            viewmodel.customer = customer;
            viewmodel.total_dividend = customerDividend;
            viewmodel.customerSavings = customerSavings;
            viewmodel.orderedCustomerSavings = orderedSavings;
            viewmodel.dividendList = dividend_list;

            return View(viewmodel);
        }
        //
        //GET: /CustomerSavings/AccountNotFound
        public ActionResult AccountNotFound()
        {
            return View();
        }
        public ActionResult EnterAccount()
        {
            return View();
        }
        // GET: /CustomerSavings/Details/5

        public ActionResult Details(int id = 0)
        {
            CustomerSavings customersavings = db.CustomerSavings.Find(id);
            if (customersavings == null)
            {
                return HttpNotFound();
            }
            return View(customersavings);
        }

        //
        // GET: /CustomerSavings/CreditAccount

        public ActionResult CreditAccount(string accountNo)
        {
            ViewBag.accountNo = accountNo;
            return View();
        }

        //
        // POST: /CustomerSavings/CreditAccount

        [HttpPost]
        public ActionResult CreditAccount(CustomerSavings customersavings)
        {
            var customerAccount = db.CustomerSavings.Where(m => m.AccountNo == customersavings.AccountNo);
            var customer = db.Customers.Where(x => x.AccountNo == customersavings.AccountNo).FirstOrDefault();

            if (ModelState.IsValid)
            {
                
                foreach (var item in customerAccount)
                {
                    if (item.AccountNo == null)
                    {
                        return View("AccountNotFound");
                    }

                }
                //customersavings.DateCreated = DateTime.Now.ToString();
                customersavings.DateCreated = DateTime.Now;
                customersavings.Transactionby = User.Identity.Name.ToString();
                customersavings.Debit = 0;

                db.CustomerSavings.Add(customersavings);
                db.SaveChanges();

                //send sms credit alert
                var sms = new SmsTransactionManagement();
                sms.sendCreditAlert(customersavings.AccountNo, customersavings.Credit, customersavings.TransactionMsg);
                
                return Redirect("~/CustomerSavings/Details/" + customersavings.Id);
            }

            return View(customersavings);
        }
        //
        // GET: /CustomerSavings/DebitAccount

        public ActionResult DebitAccount()
        {
            return View();
        }

        //
        // POST: /CustomerSavings/DebitAccount

        [HttpPost]
        public ActionResult DebitAccount(CustomerSavings customersavings)
        {
            var customerAccount = db.CustomerSavings.Where(m => m.AccountNo == customersavings.AccountNo);

            if (ModelState.IsValid)
            {
                if (customerAccount.FirstOrDefault() == null)
                {
                    return RedirectToAction("AccountNotFound");
                }
                //customersavings.DateCreated = DateTime.Now.ToString();
                customersavings.DateCreated = DateTime.Now;
                customersavings.Transactionby = User.Identity.Name.ToString();

                db.CustomerSavings.Add(customersavings);
                db.SaveChanges();

                //send sms debit alert
                var sms = new SmsTransactionManagement();
                sms.sendDebitAlert(customersavings.AccountNo, customersavings.Debit, customersavings.TransactionMsg);

                return Redirect("~/CustomerSavings/DebitDetails/" + customersavings.Id);
            }

            return View(customersavings);
        }

        // GET: /CustomerSavings/DebitDetails/5
        public ActionResult DebitDetails(int id = 0)
        {
            CustomerSavings customersavings = db.CustomerSavings.Find(id);
            if (customersavings == null)
            {
                return HttpNotFound();
            }
            return View(customersavings);
        }
        //
        // GET: /CustomerSavings/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CustomerSavings customersavings = db.CustomerSavings.Find(id);
            if (customersavings == null)
            {
                return HttpNotFound();
            }
            return View(customersavings);
        }

        //
        // POST: /CustomerSavings/Edit/5

        [HttpPost]
        public ActionResult Edit(CustomerSavings customersavings)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customersavings).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customersavings);
        }

        //
        // GET: /CustomerSavings/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CustomerSavings customersavings = db.CustomerSavings.Find(id);
            if (customersavings == null)
            {
                return HttpNotFound();
            }
            return View(customersavings);
        }

        //
        // POST: /CustomerSavings/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerSavings customersavings = db.CustomerSavings.Find(id);
            db.CustomerSavings.Remove(customersavings);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}