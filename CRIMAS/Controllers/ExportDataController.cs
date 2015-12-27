using CRIMAS.Models;
using CRIMAS.Repository.artifacts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRIMAS.Controllers
{
    /// <summary>
    /// THIS CONTROLLER IS USED FOR EXPORTING DATA TO EXCEL
    /// </summary>
    public class ExportDataController : Controller 
    {
        private Repository<Customer> _customerRepository;
        private Repository<BankReconciliation> _reconciliationRepostory;
        private Repository<Loan> _loanAgreement;
        public ExportDataController()
        {
            _customerRepository = new Repository<Customer>();
            _reconciliationRepostory = new Repository<BankReconciliation>();
            _loanAgreement = new Repository<Loan>();
        }

        public ActionResult ExportCustomerData()
        {
            Export(_customerRepository.GetAll(), "customerData_" + DateTime.Now);
            return Redirect("~/Customer/ViewAllCustomers");
        }
        public ActionResult ExportReconciliationData()
        {
            Export(_reconciliationRepostory.GetAll(), "reconciliationData_" + DateTime.Now);
            return Redirect("~/Reconciliation/data");
        }
        public ActionResult ExportLoanAgreement()
        {
            Export(_loanAgreement.GetAll(), "LoanAgreements_" + DateTime.Now);
            return Redirect("~/Loan/");
        }
        public void Export(IEnumerable obj, string filename)
        {
            GridView gv = new GridView();
            gv.DataSource = obj;
            gv.DataBind();

            Response.AddHeader("content-disposition", "attachment; filename=" + filename + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }              
    }
}
