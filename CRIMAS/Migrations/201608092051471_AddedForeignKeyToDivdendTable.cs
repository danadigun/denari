namespace CRIMAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedForeignKeyToDivdendTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dividends",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        dividend_id = c.Int(nullable: false),
                        percentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        accountNo = c.String(),
                        customerName = c.String(),
                        dateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.DividendSummaries",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        dividend_id = c.Int(nullable: false),
                        dateCreated = c.DateTime(nullable: false),
                        total_amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        percentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Customers", "image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "image");
            DropTable("dbo.DividendSummaries");
            DropTable("dbo.Dividends");
        }
    }
}
