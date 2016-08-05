using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRIMAS.Models
{
    public class Dividends
    {
        public int id { get; set; }
        public decimal percentage { get; set; }
        public decimal amount { get; set; }
        public string accountNo { get; set; }
        public string customerName { get; set; }
        public DateTime dateCreated { get; set; }
    }

    public class DividendSummary
    {
        public int id { get; set; }
        public DateTime dateCreated { get; set; }
        public decimal total_amount { get; set; }
        public decimal percentage { get; set; }
    }
}