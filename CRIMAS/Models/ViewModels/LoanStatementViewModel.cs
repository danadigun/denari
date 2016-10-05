using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRIMAS.Models.ViewModels
{
    public class LoanStatementViewModel
    {
        public List<LoanTransaction> LoanTransactions { get; set; }
        public List<LoanDocument> loanDocuments { get; set; }
    }
}