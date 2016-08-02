using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRIMAS.Models
{
    public class CustomerLoanTransaction
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int LoanId { get; set; }
        public string Credit { get; set; }
        public string Debit { get; set; }
        public string Balance { get; set; }
        public string DateCreated { get; set; }

        //relationship T<entity> id
        public int CustomerId { get; set; }
    }
}