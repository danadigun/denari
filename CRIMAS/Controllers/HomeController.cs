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

        [HttpPost]
        public ActionResult orderRequest(string plan)
        {
            string plan_name = null;
            string members = null;

            //remove comma from numbers
            plan = plan.Replace(",", "");

            decimal amount = Convert.ToInt32(plan) * 100;

            switch (plan)
            {
                case "25250":
                    {
                         plan_name = "Bronze - 3Months";
                         members = "0-100";
                         break;
                    }
                case "23900":
                    {
                        plan_name = "Bronze - 12Months";
                        members = "0-100";
                        break;
                    }
                case "20200":
                    {
                        plan_name = "Bronze - 36Months";
                        members = "0-100";
                        break;
                    }
                case "32250":
                    {
                         plan_name = "Silver - 3Months";
                        members = "101-300";
                        break;
                    }
                case "30600":
                    {
                        plan_name = "Silver - 12Months";
                        members = "101-300";
                        break;
                    }
                case "25800":
                    {
                         plan_name = "Silver - 36Months";
                        members = "101-300";
                        break;
                    }
                case "42250":
                    {
                        plan_name = "Gold - 3 Months";
                        members = "Above 300";
                        break;
                    }
                case "40100":
                    {
                        plan_name = "Gold - 12 Months";
                        members = "Above 300";
                        break;
                    }
                case "36200":
                    {
                        plan_name = "Gold - 36 Months";
                        members = "Above 300";
                        break;
                    }
                default:
                    {
                        plan_name = null;
                        break;
                    }
            }

            ViewBag.plan_name = plan_name;
            ViewBag.members = members;
            ViewBag.amount = amount;

            return View();
        }

        public ActionResult requestConfirm()
        {
            return View();
        }

    }
}
