using CRIMAS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRIMAS.Controllers
{
    public class LoanDocumentController : Controller
    {
        private CrimasDb _context;

        public LoanDocumentController()
        {
            _context = new CrimasDb();
        }
        //
        // GET: /LoanDocument/
        public ActionResult update()
        {
            bool isUpdated = true;
            int docId = Convert.ToInt32(Request.Form["DocId"].ToString());
            var document = _context.LoanDocuments.Find(docId);

            if(document != null)
            {
                isUpdated = updateFile(document);
                return Json(new { message = "successfully updated file" });
            }
            isUpdated = false;
            return Json(new { error = "unable to update file" });
            
        }
        public ActionResult add()
        {
            int loanId = Convert.ToInt32(Request.Form["LoanId"].ToString());
            int accountNo = Convert.ToInt32(Request.Form["AccountNo"].ToString());

            bool isUploaded = uploadDropZone(loanId, accountNo);
            if (isUploaded)
            {
                return Json(new {Message = "Successfully uploaded documents" });
            }
            return Json(new { Error = "Unable to upload documents" });
        }
        public ActionResult remove(int docId)
        {
            var doc = _context.LoanDocuments.Find(docId);

            bool isRemoved = removeFile(doc);
            if (isRemoved)
            {
                return Json(new { Message = "successfully removed " + doc.filename });
            }else
            {
                return Json(new { error = "unable to remove file " + doc.filename });
            }
           
        }

        public bool uploadDropZone(int? loanId, int?accountNo)
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            string header = Request.Headers.Get("Origin");

            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {

                        var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\LoanDocs", Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(path);


                        //update db
                        var loanDocument = new LoanDocument
                        {
                            accountNo = accountNo.Value,
                            docUrl = header + "\\Images\\LoanDocs\\imagepath\\" + file.FileName,
                            filename = file.FileName,
                            LoanId = loanId.Value
                        };
                        _context.LoanDocuments.Add(loanDocument);
                        _context.SaveChanges();                      

                    }

                }

            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }
            return isSavedSuccessfully;
        }
        public bool removeFile(LoanDocument doc)
        {
            bool isRemoved = true;
            string path = string.Format("{0}Images\\LoanDocs\\imagepath", Server.MapPath(@"\"));
            string file = string.Format("{0}\\{1}", path, doc.filename);
            try
            {
                var file_record = _context.LoanDocuments.Find(doc.Id);
                if (file_record != null)
                {
                    _context.LoanDocuments.Remove(file_record);
                }
                _context.SaveChanges();
                System.IO.File.Delete(file);
            }

            catch
            {
                isRemoved = false;
            }
            return isRemoved;
        }
        public bool removeFileOnly(LoanDocument doc)
        {
            bool isRemoved = true;
            string path = string.Format("{0}Images\\LoanDocs\\imagepath", Server.MapPath(@"\"));
            string file = string.Format("{0}\\{1}", path, doc.filename);
            try
            {               
                System.IO.File.Delete(file);
            }

            catch
            {
                isRemoved = false;
            }
            return isRemoved;
        }
        public bool updateFile(LoanDocument doc)
        {
            bool isUpdated = true;

            //get existing doc database record
            var document = _context.LoanDocuments.Find(doc.Id);

            //remove existing file from directory
            this.removeFileOnly(doc);

            //add new file to directory
            string fName = "";
            string header = Request.Headers.Get("Origin");

            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    fName = file.FileName;

                    if (file != null && file.ContentLength > 0)
                    {

                        var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\LoanDocs", Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(path);

                        //update changes to doc database record
                        if (document != null)
                        {
                            document.docUrl = header + "\\Images\\LoanDocs\\imagepath\\" + file.FileName;
                            document.filename = file.FileName;
                        }
                        _context.SaveChanges();
                    }

                }

            }
            catch (Exception ex)
            {
                //do nothing for now
                isUpdated = false;
            }
            return isUpdated;

        }
    }
}
