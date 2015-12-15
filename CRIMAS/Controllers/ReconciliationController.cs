using CRIMAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRIMAS.Controllers
{
    public class ReconciliationController : Controller
    {
        private Models.CrimasDb _db;
        public ReconciliationController()
        {
            _db = new CRIMAS.Models.CrimasDb();
        }
        //
        // GET: /Reconciliation/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Reconciliation/Details/5

        public ActionResult Details(int id)
        {
            var Reconciliation = _db.BankReconciliations.Find(id);
            return View(Reconciliation);
        }

        //
        // GET: /Reconciliation/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Reconciliation/Create

        [HttpPost]
        public ActionResult Create(BankReconciliation reconciliation)
        {
            try
            {
                // TODO: Add insert logic here
                if (reconciliation != null)
                {
                    _db.BankReconciliations.Add(reconciliation);
                    _db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Reconciliation/Edit/5

        public ActionResult Edit(int id)
        {
            var Reconciliation = _db.BankReconciliations.Find(id);
            return View(Reconciliation);
        }

        //
        // POST: /Reconciliation/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, BankReconciliation reconciliation)
        {
            try
            {
                // TODO: Add update logic here
                var ReconciliationToUpdate = _db.BankReconciliations.Find(id);
                if (ReconciliationToUpdate != null)
                {
                    ReconciliationToUpdate = reconciliation;
                    _db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Reconciliation/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Reconciliation/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var record = _db.BankReconciliations.Find(id);
                if (record != null)
                {
                    _db.BankReconciliations.Remove(record);
                    _db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
