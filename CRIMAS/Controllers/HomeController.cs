using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRIMAS.SupportClasses;
using CRIMAS.Models;
using CRIMAS.Services;
using CRIMAS.Repository;

namespace CRIMAS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult index()
        {
            return View();
        }

    }
}
