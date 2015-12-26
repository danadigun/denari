using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRIMAS.Models
{
    public class BankReconciliation
    {
        public int Id { get; set; }
        public DateTime dateCreated { get; set; }
        public string beneficiary { get; set; }
        public decimal deposit { get; set; }
        public decimal int_cap { get; set; }
        public decimal recovery_loan { get; set; }
        public decimal loans { get; set; }
        public decimal transfer_fee { get; set; }
        public decimal VAT { get; set; }
        public decimal sms_charge { get; set; }
        public decimal with_holding_tax { get; set; }
        public decimal other_charges { get; set; }
        public decimal per_cap_com { get; set; }
        public decimal credits { get; set; }
        public decimal debits { get; set; }
    }

    /// <summary>
    /// Settings for generating Bank Reconcilliation monthly
    /// </summary>
    public class ReconciliationProperties
    {
        public int Id { get; set; }
        public decimal int_cap_value { get; set; } //interest on capital from bank payable every month %
        public decimal transfer_fee { get; set; } //N100 from CBN for each transaction
        public decimal vat { get; set; } //5% on transfer fee
        public decimal sms_charge { get; set; }
        public decimal per_capital_commission { get; set; } //N200 commission is paid on deposit of each customer funds to d bank every month

    }
}