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
            //Check if customer is registered in the system
            var customerAccount = db.Customers.Where(m => m.AccountNo == accountno);

            if (customerAccount.Count() != 0)
            {
                return View(db.CustomerSavings.Where(m=>m.AccountNo==accountno).ToList());
            }
            return RedirectToAction("AccountNotFound");

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

        public ActionResult CreditAccount()
        {
            return View();
        }

        //
        // POST: /CustomerSavings/CreditAccount

        [HttpPost]
        public ActionResult CreditAccount(CustomerSavings customersavings)
        {
            var customerAccount = db.CustomerSavings.Where(m => m.AccountNo == customersavings.AccountNo);

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
                //return RedirectToAction("~/CustomerSavings/Details/"+customersavings.Id);
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