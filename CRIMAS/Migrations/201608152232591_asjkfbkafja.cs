namespace CRIMAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asjkfbkafja : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoanTransactions", "Cr", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.LoanTransactions", "Dr", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.LoanTransactions", "amount");
            DropColumn("dbo.LoanTransactions", "refund");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LoanTransactions", "refund", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.LoanTransactions", "amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.LoanTransactions", "Dr");
            DropColumn("dbo.LoanTransactions", "Cr");
        }
    }
}
