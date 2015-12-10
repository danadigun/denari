namespace CRIMAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialmigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfile", "phone", c => c.String());
            AddColumn("dbo.UserProfile", "email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfile", "email");
            DropColumn("dbo.UserProfile", "phone");
        }
    }
}
