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

        public string LoanStatus { get; set; }//active or in-active
    }
    public class LoanTransaction
    {
        [Key]
        public int id { get; set; }       
        public string DateCreated { get; set; }

        [Required(ErrorMessage = "Customer acccount is required")]
        public string AccountNo { get; set; }
       
        public decimal amount { get; set; }//credit
        public decimal refund { get; set; }//debit
        public string createdby { get; set; }

    }
}