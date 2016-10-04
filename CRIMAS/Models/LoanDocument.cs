using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRIMAS.Models
{
    public class LoanDocument
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public int accountNo { get; set; }
        public string docUrl { get; set; }
        public string filename { get; set; }
    }


    public class LoanDocumentResponse
    {
        public string Message { get; set; }
        public int Id { get; set; }
        public string docUrl { get; set; }
    }

}