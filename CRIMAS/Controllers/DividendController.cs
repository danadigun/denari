using CRIMAS.Repository.artifacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Http;
using System.Web.Mvc;

namespace CRIMAS.Controllers
{
    [Authorize]
    public class DividendController : Controller
    {
        private DividendManagement _ctx;

        public DividendController()
        {
            _ctx = new DividendManagement();
        }
        [Authorize(Roles ="Admin")]
        public ActionResult post()
        {
            return View();
        }


        //
        // POST: /Dividend/post
        [HttpPost]
        //[Authorize(Roles ="Admin")]
        public ActionResult post(decimal percentage)
        {
            _ctx.postDividend(percentage);

            return RedirectToAction("postSuccess"); 
        }

        //[Authorize(Roles ="Admin")]
        public ActionResult postSuccess()
        {
            return View();
        }

       
       
    }

    
}