namespace CRIMAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iniitlaanldasf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "StateOfOrigin", c => c.String(nullable: false));
            DropColumn("dbo.Customers", "LocalGovtArea");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "LocalGovtArea", c => c.String(nullable: false));
            DropColumn("dbo.Customers", "StateOfOrigin");
        }
    }
}
