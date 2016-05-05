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
using PagedList;
using CRIMAS.SupportClasses;
using System.Configuration;

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
        // GET:/Customer/
        public ActionResult Index(int? page, string search, string sortBy, string sortOrder)
        {
            var customer = from s in db.Customers select s;

            if (!String.IsNullOrEmpty(search))
            {
                search = search.ToUpper();
                customer = customer.Where(s =>
                            s.Name.ToUpper().Contains(search) ||
                            s.AccountNo.ToUpper().Contains(search));
            }

            string sort = sortBy + '-' + sortOrder;
            switch (sort)
            {
                case "name-desc":
                    customer = customer.OrderByDescending(s => s.Name);
                    break;
                case "date":
                    customer = customer.OrderBy(s => s.DateCreated);
                    break;
                case "date-desc":
                    customer = customer.OrderByDescending(s => s.DateCreated);
                    break;
                case "account":
                    customer = customer.OrderBy(s => s.AccountNo);
                    break;
                case "account-desc":
                    customer = customer.OrderByDescending(s => s.AccountNo);
                    break;
                default:
                    customer = customer.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            ViewBag.Search = search;
            ViewBag.Sortby = sortBy;
            ViewBag.SortOrder = sortOrder;

            return View(customer.ToPagedList(pageNumber, pageSize));
        }
        //
        //GET: /Customer/FindCustomer/?search=09876
        public ActionResult FindCustomer(string searchString)
        {
            ViewBag.Message = TempData["Message"];
            ViewBag.MessageType = TempData["MessageType"];

            ViewBag.search = searchString;
            return View(db.Customers.Where(s => s.AccountNo == searchString || s.Name.Contains(searchString)).ToList());
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
        public ActionResult Create(Customer customer, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string imageName = string.Empty;
                if (file != null && file.ContentLength > 0)
                {
                    string extension = Path.GetExtension(file.FileName);
                    if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
                    {
                        ModelState.AddModelError("Invalid Image", "Please upload .jpg or .png image.");
                        return View(customer);
                    }

                    imageName = Guid.NewGuid() + extension;
                    customer.ImageUrl = ConfigurationManager.AppSettings["Azure:StorageUrl"] + BlobContainer.customer.ToString() + "/" + imageName;
                }

                string CustomerAccountNo = new Random().Next(10000, 90000).ToString();

                customer.AccountNo = CustomerAccountNo;
                customer.DateCreated = DateTime.Now.ToShortDateString();

                db.Customers.Add(customer);

                //Credit the customer's account with seed money
                var credit = new CustomerSavings
                {
                    AccountNo = CustomerAccountNo,
                    Credit = 0,
                    Debit = 0,
                    //DateCreated=DateTime.Now.ToShortDateString(),
                    DateCreated = DateTime.Now,
                    Name = customer.Name,
                    Transactionby = User.Identity.Name,
                };

                db.CustomerSavings.Add(credit);
                db.SaveChanges();

                //Fire and forget cron-job for dividend generation for this customer
                //new DenariCronJobs().initateDividends(CustomerAccountNo);

                if (file != null && file.ContentLength > 0)
                {
                    FileHelper.UploadImage(file.InputStream, imageName, BlobContainer.customer);
                }

                return Redirect("~/Customer/Details/" + customer.CustomerId);
            }

            return View(customer);
        }

        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase file, int customerId)
        {
            var customer = db.Customers.FirstOrDefault(x => x.CustomerId == customerId);
            if (customer != null)
            {
                string oldFileName = customer.ImageUrl;
                string imageName = string.Empty;
                if (file != null && file.ContentLength > 0)
                {
                    string extension = Path.GetExtension(file.FileName);
                    if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
                    {
                        TempData["Message"] = "Please upload .jpg or .png image.";
                        TempData["MessageType"] = "Error";
                    }
                    else
                    {
                        imageName = Guid.NewGuid() + extension;
                        customer.ImageUrl = ConfigurationManager.AppSettings["Azure:StorageUrl"] + BlobContainer.customer.ToString() + "/" + imageName;

                        db.Entry(customer).State = EntityState.Modified;
                        db.SaveChanges();

                        FileHelper.UploadImage(file.InputStream, imageName, BlobContainer.customer);
                        FileHelper.DeleteBlob(BlobContainer.customer, oldFileName);

                        TempData["Message"] = "Image has been uploaded successfully.";
                        TempData["MessageType"] = "Success";
                    }
                }
                else
                {
                    TempData["Message"] = "Please select .jpg or .png image.";
                    TempData["MessageType"] = "Error";
                }
            }

            return RedirectToAction("FindCustomer", "Customer", new { searchString = customer.AccountNo });
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
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            CustomerSavings customerSavings = db.CustomerSavings.Find(db.CustomerSavings.Where(x => x.AccountNo == customer.AccountNo).Select(x => x.Id).FirstOrDefault());

            FileHelper.DeleteBlob(BlobContainer.customer, customer.ImageUrl);

            db.Customers.Remove(customer);
            db.CustomerSavings.Remove(customerSavings);

            db.SaveChanges();
            return RedirectToAction("Index");
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