using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRIMAS.Models
{
    public class Loan
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }

        [Required(ErrorMessage="Please enter customer account number!")]
        public string AccountNo { get; set; }
        public string Customername { get; set; }

        [DataType(DataType.Duration)]
        [Required(ErrorMessage="Please specify the duration of the loan.")]
        public string Duration { get; set; }
        public decimal amount { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage="Select the date loan deduction commences.")]
        public DateTime DateOfCommencement { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfTermination { get; set; }
        public decimal InterestRate { get; set; }
        public string createdby { get; set; }

        public string LoanStatus { get; set; }//active or completed

        //[StringLength(200)]
        //[Required(ErrorMessage = "Please upload image for signed agreement form.")]
        public byte[] ImgAgreement { get; set; }

        //[StringLength(200)]
        //[Required(ErrorMessage = "Please upload image for signed irrevocable authority form.")]
        public byte[] ImgIrrevocable { get; set; }

        //[StringLength(200)]
        //[Required(ErrorMessage = "Please upload image for duly signed guarantors forms.")]
        public byte[] ImgGuarantors { get; set; }

        public ICollection<LoanTransaction> LoanTransactions { get; set; }

    }
    public class LoanTransaction
    {
        [Key]
        public int id { get; set; }

        //public int LoanId { get; set; }

        public string DateCreated { get; set; }

        [Required(ErrorMessage = "Customer acccount is required")]
        public string AccountNo { get; set; }
       
        public decimal Cr { get; set; }//credit - Cr
        public decimal Dr { get; set; }//debit - Dr
        public string createdby { get; set; }

        //'Loan Disbursement' - in Dr side when a new Loan Is Created
        //'Interest Due' - in Dr side when a new Loan is Created = p * (Intr/100 * Duration) collected upfront.
        public string Narration { get; set; }

        public Loan Loan { get; set; }
    }
}