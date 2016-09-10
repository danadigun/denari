using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRIMAS.Models;
using System.Globalization;
using System.Threading;
using CRIMAS.Services;
using PagedList;
using System.IO;
using System.Configuration;
using CRIMAS.SupportClasses;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

namespace CRIMAS.Controllers
{
    [Authorize]
    public class LoanController : Controller
    {
        private CrimasDb _context;
        private ICurrency _currency;


        public LoanController(CrimasDb context, ICurrency currency)
        {
            _context = context;
            _currency = currency;
        }
        //
        // GET: /Loan/
        public ActionResult Index(int? page, string sortBy, string sortOrder)
        {
            //NOTE: Loan status can either be "active" or "completed"

            var userLoan = from s in _context.Loans
                           where s.LoanStatus == "active"
                           select s;

            string sort = sortBy + '-' + sortOrder;
            switch (sort.ToLower())
            {
                case "name-asc": userLoan = userLoan.OrderByDescending(s => s.Customername); break;

                case "account-asc": userLoan = userLoan.OrderBy(s => s.AccountNo); break;
                case "account-desc": userLoan = userLoan.OrderByDescending(s => s.AccountNo); break;

                case "amount-asc": userLoan = userLoan.OrderBy(s => s.amount); break;
                case "amount-desc": userLoan = userLoan.OrderByDescending(s => s.amount); break;

                case "interest-asc": userLoan = userLoan.OrderBy(s => s.InterestRate); break;
                case "interest-desc": userLoan = userLoan.OrderByDescending(s => s.InterestRate); break;

                case "duration-asc": userLoan = userLoan.OrderBy(s => s.InterestRate); break;
                case "duration-desc": userLoan = userLoan.OrderByDescending(s => s.InterestRate); break;

                //case "commencement-asc": userLoan = userLoan.OrderBy(s => s.DateOfCommencement); break;
                //case "commencement-desc": userLoan = userLoan.OrderByDescending(s => s.DateOfCommencement); break;

                //case "termination-asc": userLoan = userLoan.OrderBy(s => s.DateOfTermination); break;
                //case "termination-desc": userLoan = userLoan.OrderByDescending(s => s.DateOfTermination); break;

                default: userLoan = userLoan.OrderBy(s => s.Customername); break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.Sortby = sortBy;
            ViewBag.SortOrder = sortOrder;
            ViewBag.TotalLoan = userLoan.Count();

            return View(userLoan.ToPagedList(pageNumber, pageSize));
        }
        //
        // GET: /Loan/Details/5
        public ActionResult Details(int id = 0)
        {
            Loan loan = _context.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }
        //
        // GET: /Loan/Create
        public ActionResult Create()
        {
            return View();
        }
        //
        // POST: /Loan/Create
        [HttpPost]

        public ActionResult Create(Loan loan, HttpPostedFileBase fileAgreement, HttpPostedFileBase fileIrrevocable, HttpPostedFileBase fileGuarantors)
        {
            if (ModelState.IsValid)
            {

                if (!_context.Customers.Any(x => x.AccountNo == loan.AccountNo))
                {
                    ModelState.AddModelError("Account Not Exists", "Account does not exists.");
                    return View(loan);
                }

                var deposit = _context.CustomerSavings.Where(x => x.AccountNo == loan.AccountNo).Sum(x => x.Credit - x.Debit);
                var percent = loan.amount * Convert.ToDecimal(0.10);

                if (deposit <= percent)
                {
                    ModelState.AddModelError("No 10% Deposit", "This customer has no 10% deposit in his/her account.");
                    return View(loan);
                }

                loan.LoanStatus = "active"; //set loan status to active

                loan.Customername =
                    (from s in _context.Customers
                     where s.AccountNo == loan.AccountNo
                     select s.Name).FirstOrDefault();
                if (loan.Customername == null || loan.Customername == "")
                {
                    //return customer not registered errorr:09256A 
                    return Redirect("~/Error/ErrorCode?ErrorCode=09256A");
                }

                //if we get here customer has been enrolled
                loan.createdby = User.Identity.Name.ToString();

                //loan.DateCreated = DateTime.Now.ToShortDateString();
                loan.DateCreated = DateTime.Now;

                //DateTime completionDate = DateTime.Parse(loan.DateOfCommencement).AddMonths(Int32.Parse(loan.Duration));
                DateTime completionDate = loan.DateOfCommencement.AddMonths(Int32.Parse(loan.Duration));

                //loan.DateOfTermination = completionDate.ToShortDateString();
                loan.DateOfTermination = completionDate;

                //save amount borrowed
                var amountBorred = new Borrowed
                {
                    DateCreated = DateTime.Now.ToShortDateString(),
                    accountNo = loan.AccountNo,
                    amountborrowed = loan.amount
                };



                //customer must pay 10% of loan upfront. create loan interest record.
                var loanInterest = new LoanInterest
                {
                    accountNo = loan.AccountNo,
                    intrestAmount = Decimal.Parse("0.1") * loan.amount,
                };

                _context.LoanInterests.Add(loanInterest);
                _context.Borrows.Add(amountBorred);

                #region  Validate & upload scanned documents


                string extension_fileAgreementName = string.Empty;
                string extension_fileIrrevocableName = string.Empty;
                string extension_fileGuarantorsName = string.Empty;

                if ((fileAgreement != null && fileAgreement.ContentLength > 0)
                    && (fileIrrevocable != null && fileIrrevocable.ContentLength > 0)
                    && (fileGuarantors != null && fileGuarantors.ContentLength > 0))
                {
                    //check extensions
                    extension_fileAgreementName = Path.GetExtension(fileAgreement.FileName);
                    extension_fileIrrevocableName = Path.GetExtension(fileIrrevocable.FileName);
                    extension_fileGuarantorsName = Path.GetExtension(fileGuarantors.FileName);

                    bool checkAgreement = (extension_fileAgreementName != ".jpg" && extension_fileAgreementName != ".jpeg" && extension_fileAgreementName != ".png");
                    bool checkIrrevocable = (extension_fileIrrevocableName != ".jpg" && extension_fileIrrevocableName != ".jpeg" && extension_fileIrrevocableName != ".png");
                    bool checkGuarantor = (extension_fileGuarantorsName != ".jpg" && extension_fileGuarantorsName != ".jpeg" && extension_fileGuarantorsName != ".png");

                    if (checkAgreement && checkIrrevocable && checkGuarantor)
                    {
                        ModelState.AddModelError("Invalid Image", "Please upload .jpg or .png image.");
                        return View(loan);
                    }
                    else
                    {
                        using (var reader = new BinaryReader(fileAgreement.InputStream))
                        {
                            loan.ImgAgreement = reader.ReadBytes(fileAgreement.ContentLength);

                        }
                        using (var reader = new BinaryReader(fileIrrevocable.InputStream))
                        {
                            loan.ImgIrrevocable = reader.ReadBytes(fileIrrevocable.ContentLength);
                        }

                        using (var reader = new BinaryReader(fileGuarantors.InputStream))
                        {
                            loan.ImgGuarantors = reader.ReadBytes(fileGuarantors.ContentLength);

                        }
                    }

                }
                else
                {
                    ModelState.AddModelError("Invalid Image", "Please upload all three (3) scanned documents to proceed.");
                    return View(loan);
                }

                ////create a loan transaction record  for principal loan disbursement & total interest due                   

                loan.LoanTransactions = new List<LoanTransaction> {
                    new LoanTransaction {
                         DateCreated = DateTime.Now.ToShortDateString(),
                         AccountNo = loan.AccountNo,
                         Cr = loan.amount, //Dr
                         Dr = decimal.Parse("0"),//Cr
                         createdby = User.Identity.Name.ToString(),
                         Narration = "Loan Disbursement",
                         Loan = loan
                    },
                    new LoanTransaction
                    {
                        DateCreated = DateTime.Now.ToShortDateString(),
                        AccountNo = loan.AccountNo,
                        Cr = (loan.amount * (loan.InterestRate / 100) * decimal.Parse(loan.Duration)), //Dr
                        Dr = decimal.Parse("0"),//Cr
                        createdby = User.Identity.Name.ToString(),
                        Narration = "Interest Due",
                        Loan = loan
                    }

                };
                _context.Loans.Add(loan);
                _context.SaveChanges();


                #endregion

                return RedirectToAction("Index");
            }

            return View(loan);
        }

        public ActionResult UpdateForms(int loan_id, HttpPostedFileBase fileAgreement, HttpPostedFileBase fileIrrevocable, HttpPostedFileBase fileGuarantors)
        {
            #region  Validate & upload scanned documents
            var loan = _context.Loans.Where(x => x.Id == loan_id).FirstOrDefault();

            string extension_fileAgreementName = string.Empty;
            string extension_fileIrrevocableName = string.Empty;
            string extension_fileGuarantorsName = string.Empty;

            if ((fileAgreement != null && fileAgreement.ContentLength > 0)
                && (fileIrrevocable != null && fileIrrevocable.ContentLength > 0)
                && (fileGuarantors != null && fileGuarantors.ContentLength > 0))
            {
                //check extensions
                extension_fileAgreementName = Path.GetExtension(fileAgreement.FileName);
                extension_fileIrrevocableName = Path.GetExtension(fileIrrevocable.FileName);
                extension_fileGuarantorsName = Path.GetExtension(fileGuarantors.FileName);

                bool checkAgreement = (extension_fileAgreementName != ".jpg" && extension_fileAgreementName != ".jpeg" && extension_fileAgreementName != ".png");
                bool checkIrrevocable = (extension_fileIrrevocableName != ".jpg" && extension_fileIrrevocableName != ".jpeg" && extension_fileIrrevocableName != ".png");
                bool checkGuarantor = (extension_fileGuarantorsName != ".jpg" && extension_fileGuarantorsName != ".jpeg" && extension_fileGuarantorsName != ".png");

                if (checkAgreement && checkIrrevocable && checkGuarantor)
                {
                    ModelState.AddModelError("Invalid Image", "Please upload .jpg or .png image.");
                    return View();
                }
                else
                {
                    using (var reader = new BinaryReader(fileAgreement.InputStream))
                    {
                        loan.ImgAgreement = reader.ReadBytes(fileAgreement.ContentLength);
                        _context.SaveChanges();

                    }
                    using (var reader = new BinaryReader(fileIrrevocable.InputStream))
                    {
                        loan.ImgIrrevocable = reader.ReadBytes(fileIrrevocable.ContentLength);
                        _context.SaveChanges();
                    }

                    using (var reader = new BinaryReader(fileGuarantors.InputStream))
                    {
                        loan.ImgGuarantors = reader.ReadBytes(fileGuarantors.ContentLength);
                        _context.SaveChanges();

                    }
                }

            }
            else
            {
                ModelState.AddModelError("Invalid Image", "Please upload all three (3) scanned documents to proceed.");
                return View();
            }

           
            //_context.Loans.Add(loan);
            _context.SaveChanges();

            TempData["msg"] = "<script>swal(\"Good job!\""+","+"\"documents successfully uploaded \")</script>";



            return Redirect("~/Loan/statement?loanId="+loan_id);
            #endregion
        }

        public ActionResult RetrieveAgreement(int loan_id)
        {
            byte[] cover = GetAgreementFromDataBase(loan_id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }
        public ActionResult RetrieveIrrevocable(int loan_id)
        {
            byte[] cover = GetIrrevocableFromDataBase(loan_id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }
        public ActionResult RetrieveGurantor(int loan_id)
        {
            byte[] cover = GetGurantorFromDataBase(loan_id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }

        public byte[] GetAgreementFromDataBase(int Id)
        {
            var q = from temp in _context.Loans where temp.Id == Id select temp.ImgAgreement;
            byte[] cover = q.First();
            return cover;
        }
        public byte[] GetIrrevocableFromDataBase(int Id)
        {
            var q = from temp in _context.Loans where temp.Id == Id select temp.ImgIrrevocable;
            byte[] cover = q.First();
            return cover;
        }
        public byte[] GetGurantorFromDataBase(int Id)
        {
            var q = from temp in _context.Loans where temp.Id == Id select temp.ImgGuarantors;
            byte[] cover = q.First();
            return cover;
        }

        /// <summary>
        /// Converts Image to bytes
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(file.InputStream);
            imageBytes = reader.ReadBytes((int)file.ContentLength);
            return imageBytes;
        }
        //
        // GET: /Loan/CreditLoan
        public ActionResult CreditLoan()
        {
            return View();
        }
        //
        // POST: /Loan/CreditLoan
        [HttpPost]
        public ActionResult CreditLoan(LoanTransaction loan)
        {


            var customerAccount = _context.CustomerSavings.Where(m => m.AccountNo == loan.AccountNo);

            if (ModelState.IsValid)
            {
                foreach (var item in customerAccount)
                {
                    if (item.AccountNo == null)
                    {
                        return Redirect("~/CustomerSavings/AccountNotFound");
                    }
                }
                loan.DateCreated = DateTime.Now.ToShortDateString();
                loan.createdby = User.Identity.Name.ToString();

                _context.LoanTransactions.Add(loan);
                _context.SaveChanges();
            }
            return View("LoanCreditSuccess");
        }
        //
        //GET: /Loan/LoanCreditSuccess
        public ActionResult LoanCreditSuccess()
        {
            return View();
        }
        //
        //GET: /Loan/EnterAccountNo
        public ActionResult EnterAccountNo()
        {
            return View();
        }
        //
        //GET: /Loan/CheckLoanStatus
        public ActionResult CheckLoanStatus(string accountNo)
        {
            //set currency to naira
            _currency.SetCurrencyToNaira();

            ViewBag.AccountNo = accountNo;
            ViewBag.NoOfLoanAgreements = _context.Loans.Where(s => s.AccountNo == accountNo).Count();

            var totalCredits = (from s in _context.LoanTransactions where s.AccountNo == accountNo select s.Cr);
            var totalDebits = (from s in _context.LoanTransactions where s.AccountNo == accountNo select s.Dr);

            //check if materialized value is null 
            if (totalCredits.Count() == 0)
            {
                //return empty loan repo errorr:02547B 
                return Redirect("~/Error/ErrorCode?ErrorCode=02547B");
            }
            ViewBag.totalCredits = totalCredits.Sum();
            ViewBag.totalDebits = totalDebits.Sum();


            var BorrowedAmount = from s in _context.Borrows where s.accountNo == accountNo select s.amountborrowed;

            //if customer has never borrowed
            if (BorrowedAmount.Count() == 0)
            {
                //return empty loan repo errorr:02547B 
                return Redirect("~/Error/ErrorCode?ErrorCode=02547B");
            }
            //if we get here customer has loaned cash
            var AllBorrowed = new List<String>();

            foreach (var item in BorrowedAmount)
            {
                AllBorrowed.Add(string.Format("{0:C}", item));
            }

            ViewBag.AmountBorrowed = AllBorrowed.ElementAt(AllBorrowed.Count() - 1);

            //Change customer's loan status to "completed" if balance is 0
            var balance = totalCredits.Sum() - totalDebits.Sum();

            if (balance <= 0)
            {
                //change the loan aggreement status to "completed"
                var CurrentLoan = (from s in _context.Loans
                                   where s.AccountNo == accountNo
                                   orderby s.DateCreated descending
                                   select s).First<Loan>();

                CurrentLoan.LoanStatus = "completed";

                _context.SaveChanges();
            }

            return View(_context.LoanTransactions.Where(s => s.AccountNo == accountNo).ToList());
        }


        //
        // GET: /Loan/Edit/5
        public ActionResult Edit(int id = 0)
        {
            Loan loan = _context.Loans.Find(id);

            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }
        //
        // POST: /Loan/Edit/5
        [HttpPost]
        public ActionResult Edit(Loan loan)
        {
            if (ModelState.IsValid)
            {
                //DateTime completionDate = DateTime.Parse(loan.DateOfCommencement).AddMonths(Int32.Parse(loan.Duration));
                //loan.DateOfTermination = completionDate.ToShortDateString();
                _context.Entry(loan).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loan);
        }
        //
        // GET: /Loan/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Loan loan = _context.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }
        //
        // POST: /Loan/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Loan loan = _context.Loans.Find(id);
            _context.Loans.Remove(loan);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public virtual ActionResult Download(string imageUrl)
        {
            var spreadSheetStream = new MemoryStream(new WebClient().DownloadData(imageUrl));

            string contentType = "";
            switch (Path.GetExtension(imageUrl))
            {
                case ".jpeg": contentType = "image/jpeg"; break;
                case ".jpg": contentType = "image/jpeg"; break;
                case ".png": contentType = "image/png"; break;
                default: contentType = "image/png"; break;
            }

            return File(spreadSheetStream.ToArray(), contentType, Path.GetFileName(imageUrl));
        }

        /// <summary>
        /// returns loan statement
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns></returns>
        public ActionResult statement(int loanId)
        {
            ViewBag.LoanId = loanId;
            return View(_context.LoanTransactions.Where(x => x.Loan.Id == loanId).ToList());
        }

       

        /// <summary>
        /// Return all Loans both active and completed
        /// </summary>
        /// <returns></returns>
        public ActionResult history()
        {
            var loans = _context.Loans;
            return View(loans);
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }



        /// <summary>
        /// test script
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        public ActionResult load()
        {
            var loan_accounts = _context.Loans.Select(x => x.AccountNo).ToList();
            foreach(var account in loan_accounts)
            {
                RunLoanUpdateScript(account);             
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);


        }
        [HttpPost]
        public ActionResult loadSingle(string account_no)
        {         
                RunLoanUpdateScript(account_no);
           
            return new HttpStatusCodeResult(HttpStatusCode.OK);

        }

        private void RunLoanUpdateScript(string account_no)
        {
            int i = 0;
            int t = 0;
            using (var ctx = new CrimasDb())
            {
                var LoansByAccountNo = _context.Loans.Where(x => x.AccountNo == account_no).ToList();
                var LoanTransactionByAccountNo = _context.LoanTransactions.Where(x => x.AccountNo == account_no).ToList();


                //var loan = LoansByAccountNo[i];
                var _transaction = LoanTransactionByAccountNo[t];
                int flag = 0;

                while (true)
                {
                    foreach (var transaction in LoanTransactionByAccountNo)
                    {
                        if (flag == 1 || flag == 0) { i = 0; }

                        if (flag > 1) { i = flag - 2; }

                        //if (flag == LoansByAccountNo.Count()) { i = LoansByAccountNo.Count() - 1; }

                        if (transaction.Cr == 0 && transaction.Dr == 0)
                        {
                            transaction.Loan = null;
                            _context.SaveChanges();
                        }

                        if (transaction.Dr > 0 && transaction.Cr == 0)
                        {
                            if (flag == 1 || flag == 0)
                            {
                                ++flag;
                                transaction.Loan = LoansByAccountNo[i];
                                transaction.Narration = "Loan Disbursement";

                                //insert a record for Interest due
                                //ctx.LoanTransactions.Add(new LoanTransaction
                                //{
                                //    AccountNo = transaction.AccountNo,
                                //    Dr = 0, //intersest due
                                //    Cr = LoansByAccountNo[i].amount * (LoansByAccountNo[i].InterestRate * 0.01m) * Convert.ToDecimal(LoansByAccountNo[i].Duration),
                                //    createdby = "admin@digitalforte.ng",
                                //    DateCreated = DateTime.Now.ToString(),
                                //    Loan = LoansByAccountNo[i],
                                //    Narration = "Interest Due"
                                //});
                                _context.SaveChanges();
                            }

                            if (flag > 1)
                            {
                                i = flag - 1;

                                transaction.Loan = LoansByAccountNo[i];
                                transaction.Narration = "Loan Disbursement";
                                transaction.createdby = "admin@digitalforte.ng";
                                _context.SaveChanges();

                                flag++;
                            }
                        }
                        if (transaction.Dr == 0 && transaction.Cr > 0)
                        {

                            transaction.Loan = LoansByAccountNo[i];
                            transaction.Narration = "Loan Repayment";
                            transaction.createdby = "admin@digitalforte.ng";
                            _context.SaveChanges();
                        }
                    }
                    break;
                }

            }
        }

        [HttpPost]
        public ActionResult fix(string account_no)
        {
            var _transaction = _context.LoanTransactions.Where(x => x.AccountNo == account_no && x.Cr == 0 && x.Dr != 0).ToList();

            foreach(var tx in _transaction)
            {
                _context.Loans.Add(new Loan
                {
                    AccountNo = tx.AccountNo,
                    amount = tx.Dr,
                    createdby = tx.createdby,
                    LoanStatus = "completed",
                    DateCreated = DateTime.Now,
                    Duration = "10",
                    Customername = _context.Customers.Where(x => x.AccountNo == account_no).Select(x => x.Name).FirstOrDefault(),
                    InterestRate = 10,
                    DateOfCommencement = DateTime.Now,    
                    ImgAgreement = null,
                    ImgGuarantors = null,
                    DateOfTermination = DateTime.Now.AddDays(10),
                    ImgIrrevocable = null,
                    LoanTransactions = _transaction
                               
                });
              
            }
            _context.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);

        }
    }
}