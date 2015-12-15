using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRIMAS.Models
{
    public class CustomerSavings
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage="Please enter account number!")]
        public string AccountNo { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage="Please specify the amount to credit account!")]
        public decimal Credit { get; set; }

        [Required(ErrorMessage = "Please specify the amount to debit account!")]
        public decimal Debit { get; set; }

        public string TransactionMsg { get; set; } //withdrawal, deposits, monthly dividends
        //public string Balance { get; set; }
        public string Transactionby { get; set; }
        public DateTime DateCreated { get; set; }


        //relationship T<entity> id
        //public int CustomerId { get; set; }
    }
}