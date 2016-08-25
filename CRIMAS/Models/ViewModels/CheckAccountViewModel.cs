using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRIMAS.Models.ViewModels
{
    public class CheckAccountViewModel
    {
        public string account { get; set; }
        public string name { get; set; }
        public decimal total_dividend { get; set; }
        public Customer customer { get; set; }
        public List<Dividends> dividendList { get; set; }
        public List<CustomerSavings> customerSavings { get; set; }
        public IQueryable<CustomerSavings> orderedCustomerSavings { get; set; }
    }
}