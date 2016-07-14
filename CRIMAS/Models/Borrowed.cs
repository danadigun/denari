using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRIMAS.Models
{
    public class Borrowed
    {
        [Key]
        public int id { get; set; }
        public string DateCreated { get; set; }
        public string accountNo { get; set; }
        public decimal amountborrowed { get; set; }
    }
}