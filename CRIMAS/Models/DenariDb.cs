using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CRIMAS.Models
{
    public class DenariDb : DbContext
    {
        public DenariDb()
               : base("name=DefaultConnection")
        {
        }
        public DbSet<UserProfile> UserProfiles { get; set; } //user profiles
        public DbSet<DenariCustomer> DenariCustomers { get; set; }
        public DbSet<CustomerTransaction> CustomerTransactions { get; set; }
    }
    public class DenariCustomer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public bool hasPayed { get; set; }
        public DateTime dateCreated { get; set; }
        List<CustomerTransaction> transactions { get; set; }
    }

    public class CustomerTransaction
    {
        public int id { get; set; }
        public string transactionId { get; set; }
        public int amount { get; set; }
        public string subscription_type { get; set; }
        public DenariCustomer customer { get; set; }
    }
}