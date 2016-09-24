namespace CRIMAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeDateCreated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DenariCustomers", "dateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DenariCustomers", "dateCreated");
        }
    }
}
