using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRIMAS.Models;
using System.Net.Mail;

namespace CRIMAS.Controllers
{
    public class HomeController : Controller
    {
        private DenariDb _context;

        public HomeController()
        {
            _context = new DenariDb();
        }
        public ActionResult index()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult index(string email)
        {
            var _email = _context.DenariCustomers.Where(x=>x.email==email).FirstOrDefault();

            if(_email != null)
            {
                return View();
            }else
            {

                return Redirect("~/Home/start?email=" + email);

            }

        }

        public ActionResult start(string email)
        {
            ViewBag.email = email;
            return View();
        }
        [HttpPost]
        public ActionResult request(DenariCustomer model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DenariDb())
                {
                    db.DenariCustomers.Add(new DenariCustomer {
                        CompanyName = model.CompanyName,
                        email = model.email,
                        Location = model.Location,
                        name = model.name,
                        phone = model.phone
                    });
                    db.SaveChanges();
                }
            }

            return RedirectToAction("requestConfirm");
        }

        public ActionResult requestConfirm()
        {
            return View();
        }

    }
}
