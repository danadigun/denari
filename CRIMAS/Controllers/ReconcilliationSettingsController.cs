using CRIMAS.Models;
using CRIMAS.Repository.artifacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRIMAS.Controllers
{
    public class ReconcilliationSettingsController : Controller
    {
        private Repository<ReconciliationProperties> _repo;
        public ReconcilliationSettingsController()
        {
            _repo = new Repository<ReconciliationProperties>();
        }
        //
        // GET: /ReconcilliationSettings/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /ReconcilliationSettings/Details/5

        public ActionResult Details(int id)
        {
            return View(_repo.GetById(id));
        }

        //
        // GET: /ReconcilliationSettings/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ReconcilliationSettings/Create
        [HttpPost]
        public ActionResult Create(ReconciliationProperties properties)
        {
            try
            {              
                _repo.Add(properties);
                return Json(new { success = true });
                //return RedirectToAction("Index");
            }
            catch
            {
                return Json(new { success = false });
                //return View();
            }
        }

        //
        // GET: /ReconcilliationSettings/Edit/5

        public ActionResult Edit(int id)
        {
            return View(_repo.GetById(id));
        }

        //
        // POST: /ReconcilliationSettings/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, ReconciliationProperties properties)
        {
            try
            {
                _repo.Update(properties, id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ReconcilliationSettings/Delete/5

        public ActionResult Delete(int id)
        {
            return View(_repo.GetById(id));
        }

        //
        // POST: /ReconcilliationSettings/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _repo.RemoveById(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
