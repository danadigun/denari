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
    public class CustomerController : Controller
    {
        private CrimasDb db = new CrimasDb();

        //
        // GET: /Customer/

        public ActionResult Index(int?page, string sortOrder, string searchString)
        {
            ViewBag.SearchString = searchString;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date desc" : "Date";

            var customer = from s in db.Customers select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                customer = customer.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())
                                    || s.AccountNo.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Name desc":
                    customer = customer.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    customer = customer.OrderBy(s => s.DateCreated);
                    break;
                case "Date desc":
                    customer = customer.OrderByDescending(s => s.DateCreated);
                    break;
                default:
                    customer = customer.OrderBy(s => s.Name);
                    break;
            }
            //const int pageSize = 10;

            //var pagenatedcustomers = new PaginatedList<Customer>(customer, page ?? 0, pageSize);

            return View(customer.ToList());
        }
        //
        //GET: /Customer/FindCustomer/?searchString=09876
        public ActionResult FindCustomer(string searchString)
        {
            return View(db.Customers.Where(s => s.AccountNo == searchString||s.Name.Contains(searchString)).ToList());
        }
        //
        // GET: /Customer/Details/5

        public ActionResult Details(int id = 0)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // GET: /Customer/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Customer/Create

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                string CustomerAccountNo = new Random().Next(10000, 90000).ToString();

                customer.AccountNo = CustomerAccountNo;
                customer.DateCreated = DateTime.Now.ToShortDateString();
              
                db.Customers.Add(customer);

                //Credit the customer's account with seed money
                var credit = new CustomerSavings
                {
                    AccountNo = CustomerAccountNo,
                    Credit = 1,
                    Debit=0,
                    //DateCreated=DateTime.Now.ToShortDateString(),
                    DateCreated = DateTime.Now,
                    Name = customer.Name,
                    Transactionby = User.Identity.Name                   
                };
                db.CustomerSavings.Add(credit);
                db.SaveChanges();
                return Redirect("~/Customer/Details/"+customer.CustomerId);
            }

            return View(customer);
        }

        //
        // GET: /Customer/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // POST: /Customer/Edit/5

        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }
        
        //
        // GET: /Customer/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // POST: /Customer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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