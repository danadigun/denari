namespace CRIMAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anskjfbnafl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DenariCustomers", "hasPayed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DenariCustomers", "hasPayed");
        }
    }
}
