using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRIMAS.Models
{
    /// <summary>
    /// Parameter model to post Pay stack initiate transaction 
    /// POST -> https://api.paystack.co/transaction/initialize
    /// </summary>
    public class InitializeTransaction
    {
        public string Authorization { get; set; }
        public string reference { get; set; }
        public int amount { get; set; }
        public string email { get; set; }
        public string plan { get; set; }
        public string callback_url { get; set; }
    }
}