using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
/*
 * This Table collects initial 10% of the loan requested for 
 */
namespace CRIMAS.Models
{
    public class LoanInterest
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string accountNo { get; set; }
        public decimal intrestAmount { get; set; }
    }
}