using CRIMAS.Models;
using CRIMAS.Repository.artifacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRIMAS.Controllers
{
    public class ReconciliationController : Controller
    {
        private Repository<BankReconciliation> _repository;
       
        public ReconciliationController()
        {
            _repository = new Repository<BankReconciliation>();
        }
        //
        // GET: /Reconciliation/

        public ActionResult Index()
        {
            return View();
        }

        //
        //GET: /Reconciliation/data
        public ActionResult data()
        {
            //depositors
            //deposits
            //interest on capital ()
            return View();
        }
        //
        // GET: /Reconciliation/Details/5
        public ActionResult Details(int id)
        {
            return View(_repository.GetById(id));
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
            _repository.Add(reconciliation);
            return RedirectToAction("index");
        }
       
        //
        // GET: /Reconciliation/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_repository.GetById(id));
        }

        //
        // POST: /Reconciliation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, BankReconciliation reconciliation)
        {
            try
            {
                _repository.Update(reconciliation, id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Reconciliation/Delete/5
        public ActionResult Delete(int id=0)
        {
            return View(_repository.GetById(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _repository.RemoveById(id);
            return RedirectToAction("index");
        }
       
    }
}
