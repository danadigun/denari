namespace CRIMAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        role = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        ConfirmPassword = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        AccountNo = c.String(),
                        Name = c.String(nullable: false),
                        NextOfkin = c.String(nullable: false),
                        OfficeAddress = c.String(nullable: false),
                        ResidentialAddress = c.String(nullable: false),
                        VillageClan = c.String(nullable: false),
                        LocalGovtArea = c.String(nullable: false),
                        DateCreated = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.CustomerSavings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountNo = c.String(nullable: false),
                        Name = c.String(),
                        Credit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Debit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Transactionby = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerLoanTransactions",
                c => new
                    {
                        LoanId = c.Int(nullable: false, identity: true),
                        Credit = c.String(),
                        Debit = c.String(),
                        Balance = c.String(),
                        DateCreated = c.String(),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LoanId);
            
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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Borroweds",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        DateCreated = c.String(),
                        accountNo = c.String(),
                        amountborrowed = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.LoanTransactions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        DateCreated = c.String(),
                        AccountNo = c.String(nullable: false),
                        amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        refund = c.Decimal(nullable: false, precision: 18, scale: 2),
                        createdby = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LoanTransactions");
            DropTable("dbo.Borroweds");
            DropTable("dbo.Loans");
            DropTable("dbo.CustomerLoanTransactions");
            DropTable("dbo.CustomerSavings");
            DropTable("dbo.Customers");
            DropTable("dbo.UserProfile");
        }
    }
}
