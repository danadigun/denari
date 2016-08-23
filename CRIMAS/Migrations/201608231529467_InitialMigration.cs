namespace CRIMAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.BankReconciliations",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            dateCreated = c.DateTime(nullable: false),
            //            beneficiary = c.String(),
            //            deposit = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            int_cap = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            recovery_loan = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            loans = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            transfer_fee = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            VAT = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            sms_charge = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            with_holding_tax = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            other_charges = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            per_cap_com = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            credits = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            debits = c.Decimal(nullable: false, precision: 18, scale: 2),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Borroweds",
            //    c => new
            //        {
            //            id = c.Int(nullable: false, identity: true),
            //            DateCreated = c.String(),
            //            accountNo = c.String(),
            //            amountborrowed = c.Decimal(nullable: false, precision: 18, scale: 2),
            //        })
            //    .PrimaryKey(t => t.id);
            
            //CreateTable(
            //    "dbo.CustomerLoanTransactions",
            //    c => new
            //        {
            //            LoanId = c.Int(nullable: false, identity: true),
            //            Credit = c.String(),
            //            Debit = c.String(),
            //            Balance = c.String(),
            //            DateCreated = c.String(),
            //            CustomerId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.LoanId);
            
            //CreateTable(
            //    "dbo.Customers",
            //    c => new
            //        {
            //            CustomerId = c.Int(nullable: false, identity: true),
            //            AccountNo = c.String(),
            //            Name = c.String(nullable: false),
            //            NextOfkin = c.String(nullable: false),
            //            OfficeAddress = c.String(nullable: false),
            //            ResidentialAddress = c.String(nullable: false),
            //            employer = c.String(),
            //            Email = c.String(),
            //            phone = c.String(),
            //            StateOfOrigin = c.String(nullable: false),
            //            DateCreated = c.String(),
            //            ImageUrl = c.String(),
            //            image = c.Binary(),
            //        })
            //    .PrimaryKey(t => t.CustomerId);
            
            //CreateTable(
            //    "dbo.CustomerSavings",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            AccountNo = c.String(nullable: false),
            //            Name = c.String(),
            //            Credit = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            Debit = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            TransactionMsg = c.String(),
            //            Transactionby = c.String(),
            //            DateCreated = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Dividends",
            //    c => new
            //        {
            //            id = c.Int(nullable: false, identity: true),
            //            dividend_id = c.Int(nullable: false),
            //            percentage = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            amount = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            accountNo = c.String(),
            //            customerName = c.String(),
            //            dateCreated = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.id);
            
            //CreateTable(
            //    "dbo.DividendSummaries",
            //    c => new
            //        {
            //            id = c.Int(nullable: false, identity: true),
            //            dividend_id = c.Int(nullable: false),
            //            dateCreated = c.DateTime(nullable: false),
            //            total_amount = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            percentage = c.Decimal(nullable: false, precision: 18, scale: 2),
            //        })
            //    .PrimaryKey(t => t.id);
            
            //CreateTable(
            //    "dbo.LoanInterests",
            //    c => new
            //        {
            //            id = c.Int(nullable: false, identity: true),
            //            accountNo = c.String(),
            //            intrestAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
            //        })
            //    .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Loans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(nullable: false),
                        AccountNo = c.String(nullable: false),
                        Customername = c.String(),
                        Duration = c.String(nullable: false),
                        amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateOfCommencement = c.DateTime(nullable: false),
                        DateOfTermination = c.DateTime(nullable: false),
                        InterestRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        createdby = c.String(),
                        LoanStatus = c.String(),
                        ImgAgreement = c.Binary(),
                        ImgIrrevocable = c.Binary(),
                        ImgGuarantors = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LoanTransactions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        DateCreated = c.String(),
                        AccountNo = c.String(nullable: false),
                        Cr = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Dr = c.Decimal(nullable: false, precision: 18, scale: 2),
                        createdby = c.String(),
                        Narration = c.String(),
                        Loan_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Loans", t => t.Loan_Id)
                .Index(t => t.Loan_Id);
            
            //CreateTable(
            //    "dbo.ReconciliationProperties",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            int_cap_value = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            transfer_fee = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            vat = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            sms_charge = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            per_capital_commission = c.Decimal(nullable: false, precision: 18, scale: 2),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.UserProfile",
            //    c => new
            //        {
            //            UserId = c.Int(nullable: false, identity: true),
            //            FirstName = c.String(),
            //            LastName = c.String(),
            //            Address = c.String(),
            //            phone = c.String(),
            //            email = c.String(),
            //            role = c.String(),
            //            UserName = c.String(),
            //            Password = c.String(),
            //            ConfirmPassword = c.String(),
            //        })
            //    .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoanTransactions", "Loan_Id", "dbo.Loans");
            DropIndex("dbo.LoanTransactions", new[] { "Loan_Id" });
            //DropTable("dbo.UserProfile");
            //DropTable("dbo.ReconciliationProperties");
            DropTable("dbo.LoanTransactions");
            DropTable("dbo.Loans");
            //DropTable("dbo.LoanInterests");
            //DropTable("dbo.DividendSummaries");
            //DropTable("dbo.Dividends");
            //DropTable("dbo.CustomerSavings");
            //DropTable("dbo.Customers");
            //DropTable("dbo.CustomerLoanTransactions");
            //DropTable("dbo.Borroweds");
            //DropTable("dbo.BankReconciliations");
        }
    }
}
