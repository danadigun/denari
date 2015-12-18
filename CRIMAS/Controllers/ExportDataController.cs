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
    public class ExportDataController : Controller 
    {
        //
        // GET: /ExportData/

        public ActionResult Index()
        {
            return View();
        }
        public void Export(IEnumerable obj, string filename, HttpResponse response)
        {
            GridView gv = new GridView();
            gv.DataSource = obj;
            gv.DataBind();
            
            response.AddHeader("content-disposition", "attachment; filename=" + filename + ".xls");
            response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            response.Output.Write(sw.ToString());
            response.Flush();
            response.End();
        }



       
    }
}
