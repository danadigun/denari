using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRIMAS.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Index()
        {
            return HttpNotFound();
        }
      
        public ActionResult ErrorCode(string ErrorCode)
        {
            switch(ErrorCode){
                    
                case "02547B"://Customer has never taken a loan
                    ViewBag.ErrorTitle = "Empty loan repository!";
                    ViewBag.ErrorDescription = "You are getting this message because the customer has never accessed a loan.";
                    break;

                case "09256A":
                    ViewBag.ErrorTitle = "Unregistered customer!";
                    ViewBag.ErrorDescription = "You are getting this error message because this customer does not exist and has NOT been enrolled. Please click on enroll customer on your task bar to enroll cutomer before applying for a loan agreement.";
                    break;
                case "1376D":
                    ViewBag.ErrorTitle = "Invalid Date String!";
                    ViewBag.ErrorDescription = "You are getting this error message because the date has not been selected or is null. select a valid date.";
                    break;
                case "1586D":
                    ViewBag.ErrorTitle = "Empty Dividends repository";
                    ViewBag.ErrorDescription = "You are getting this  message because there are no customer deposits with dividends for the rateable register yet.";
                    break;
                case "19086D":
                    ViewBag.ErrorTitle = "You need Customers on your platform!";
                    ViewBag.ErrorDescription = "You are getting this  message because there are no customers on your platform yet for us to manage. Enroll new customers now!";
                    break;
                case "19086C":
                    ViewBag.ErrorTitle = "There are no Loans to manage!";
                    ViewBag.ErrorDescription = "You are getting this  message because there are no Loans & deposits on your platform yet for us to manage. Enroll customers, credit their accounts and give them loans";
                    break;
                case"19086D1":
                     ViewBag.ErrorTitle = "Yes you have Customers but no deposits!";
                    ViewBag.ErrorDescription = "You are getting this  message because there are no Loans & deposits on your platform yet for us to manage. ";
                    break;
            }
            return View();
        }

    }
}
