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
            }
            return View();
        }

    }
}
