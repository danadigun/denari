using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRIMAS.SupportClasses;
using CRIMAS.Models;
using CRIMAS.Services;
using CRIMAS.Repository;
using System.Net.Mail;

namespace CRIMAS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult request(RequestDemoViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new CrimasDb())
                {
                    db.DenariCustomers.Add(new DenariCustomer {
                        CompanyName = model.companyName,
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
