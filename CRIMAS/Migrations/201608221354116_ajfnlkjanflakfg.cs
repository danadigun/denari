namespace CRIMAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajfnlkjanflakfg : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerTransactions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        transactionId = c.String(),
                        amount = c.Int(nullable: false),
                        subscription_type = c.String(),
                        customer_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.DenariCustomers", t => t.customer_id)
                .Index(t => t.customer_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerTransactions", "customer_id", "dbo.DenariCustomers");
            DropIndex("dbo.CustomerTransactions", new[] { "customer_id" });
            DropTable("dbo.CustomerTransactions");
        }
    }
}
