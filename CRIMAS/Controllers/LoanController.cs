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

namespace CRIMAS.Controllers
{
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
                #region  Validate File Type

                string fileAgreementName, fileIrrevocableName, fileGuarantorsName, extension;
                if (fileAgreement != null && fileAgreement.ContentLength > 0)
                {
                    extension = Path.GetExtension(fileAgreement.FileName);
                    if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
                    {
                        ModelState.AddModelError("Invalid Image", "Please upload .jpg or .png image.");
                        return View(loan);
                    }
                    fileAgreementName = Guid.NewGuid() + extension;
                    loan.ImgAgreementUrl = ConfigurationManager.AppSettings["Azure:StorageUrl"] + BlobContainer.loan.ToString() + "/" + fileAgreementName;
                }
                else
                {
                    ModelState.AddModelError("Invalid Image", "Please upload image for signed agreement form.");
                    return View(loan);
                }

                if (fileIrrevocable != null && fileIrrevocable.ContentLength > 0)
                {
                    extension = Path.GetExtension(fileIrrevocable.FileName);
                    if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
                    {
                        ModelState.AddModelError("Invalid Image", "Please upload .jpg or .png image.");
                        return View(loan);
                    }

                    fileIrrevocableName = Guid.NewGuid() + extension;
                    loan.ImgIrrevocableUrl = ConfigurationManager.AppSettings["Azure:StorageUrl"] + BlobContainer.loan.ToString() + "/" + fileIrrevocableName;
                }
                else
                {
                    ModelState.AddModelError("Invalid Image", "Please upload image for signed irrevocable authority form.");
                    return View(loan);
                }

                if (fileGuarantors != null && fileGuarantors.ContentLength > 0)
                {
                    extension = Path.GetExtension(fileGuarantors.FileName);
                    if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
                    {
                        ModelState.AddModelError("Invalid Image", "Please upload .jpg or .png image.");
                        return View(loan);
                    }

                    fileGuarantorsName = Guid.NewGuid() + extension;
                    loan.ImgGuarantorsUrl = ConfigurationManager.AppSettings["Azure:StorageUrl"] + BlobContainer.loan.ToString() + "/" + fileGuarantorsName;
                }
                else
                {
                    ModelState.AddModelError("Invalid Image", "Please upload image for signed irrevocable authority form.");
                    return View(loan);
                }

                #endregion

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

                //create a loan transaction record             
                var loanTransaction = new LoanTransaction
                {
                    DateCreated = DateTime.Now.ToShortDateString(),
                    AccountNo = loan.AccountNo,
                    amount = loan.amount,
                    refund = decimal.Parse("0"),//credit
                    createdby = User.Identity.Name.ToString()
                };

                //customer must pay 10% of loan upfront. create loan interest record.
                var loanInterest = new LoanInterest
                {
                    accountNo = loan.AccountNo,
                    intrestAmount = Decimal.Parse("0.1") * loan.amount,
                };
                _context.LoanInterests.Add(loanInterest);
                _context.Borrows.Add(amountBorred);
                _context.Loans.Add(loan);
                _context.LoanTransactions.Add(loanTransaction);
                _context.SaveChanges();

                #region Save Images

                if (fileAgreement != null && fileAgreement.ContentLength > 0)
                {
                    FileHelper.UploadImage(fileAgreement.InputStream, fileAgreementName, BlobContainer.loan);
                }

                if (fileIrrevocable != null && fileIrrevocable.ContentLength > 0)
                {
                    FileHelper.UploadImage(fileIrrevocable.InputStream, fileIrrevocableName, BlobContainer.loan);
                }

                if (fileGuarantors != null && fileGuarantors.ContentLength > 0)
                {
                    FileHelper.UploadImage(fileGuarantors.InputStream, fileGuarantorsName, BlobContainer.loan);
                }

                #endregion

                return RedirectToAction("Index");
            }

            return View(loan);
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

            var totalCredits = (from s in _context.LoanTransactions where s.AccountNo == accountNo select s.amount);
            var totalDebits = (from s in _context.LoanTransactions where s.AccountNo == accountNo select s.refund);

            //check if materialized value is null 
            if (totalCredits.Count() == 0)
            {
                //return empty loan repo errorr:02547B 
                return Redirect("~/Error/ErrorCode?ErrorCode=02547B");
            }
            ViewBag.totalCredits = totalCredits.Sum();
            ViewBag.totalDebits = totalDebits.Sum();


            //var LoanStatus = from s in db.Loans
            //                 join n in db.Borrows on s.Id equals n.id
            //                 select new
            //                 {
            //                     accountNo = s.AccountNo,
            //                     AmountBorrowed = n.amountborrowed
            //                 };
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

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }
    }
}