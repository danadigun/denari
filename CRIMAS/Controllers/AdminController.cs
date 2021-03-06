﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRIMAS.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        [Authorize(Roles = "Admin")]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /Admin/SecurityAndConfiguration
        [Authorize(Roles = "Admin")]
        [Authorize]
        public ActionResult SecurityAndConfiguration()
        {
            return View();
        }

        public ActionResult DashboardError()
        {
            return View();
        }


    }
}
