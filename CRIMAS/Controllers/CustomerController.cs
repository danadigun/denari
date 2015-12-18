using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRIMAS.Models;
using CRIMAS.Repository.artifacts;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Collections;

namespace CRIMAS.Controllers
{
    public class CustomerController : Controller
    {
        private CrimasDb db = new CrimasDb();
        private Repository<Customer> _customerRepository;
        private ExportDataController _export;
        public CustomerController()
        {
            _customerRepository = new Repository<Customer>();
            _export = new ExportDataController();
        }

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

            //return View(customer.ToList());
            return RedirectToAction("ViewAllCustomers");
        }
        //
        //GET: /Customer/FindCustomer/?searchString=09876
        public ActionResult FindCustomer(string searchString)
        {
            ViewBag.searchString = searchString;

            return View(db.Customers.Where(s => s.AccountNo == searchString||s.Name.Contains(searchString)).ToList());
        }

        //GET: /Customer/ViewAllCustomers
        public ActionResult ViewAllCustomers()
        {
            return View(db.Customers.ToList());
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
                    Credit = 0,
                    Debit=0,
                    //DateCreated=DateTime.Now.ToShortDateString(),
                    DateCreated = DateTime.Now,
                    Name = customer.Name,
                    Transactionby = User.Identity.Name                   
                };
                db.CustomerSavings.Add(credit);
                db.SaveChanges();

                //Fire and forget cron-job for dividend generation for this customer
                //new DenariCronJobs().initateDividends(CustomerAccountNo);

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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles="Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            CustomerSavings customerSavings = db.CustomerSavings.Find(db.CustomerSavings.Where(x => x.AccountNo == customer.AccountNo).Select(x => x.Id).FirstOrDefault());

            db.Customers.Remove(customer);
            db.CustomerSavings.Remove(customerSavings);

            db.SaveChanges();
            return RedirectToAction("ViewAllCustomers");
        }

        public ActionResult ExportData()
        {
           
            Export(_customerRepository.GetAll(), "customerData");
            return RedirectToAction("Index");
        }

        public void Export(IEnumerable obj, string filename)
        {
            GridView gv = new GridView();
            gv.DataSource = obj;
            gv.DataBind();

            Response.AddHeader("content-disposition", "attachment; filename=" + filename + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }       

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        
    }
}