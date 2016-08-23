namespace CRIMAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatesToDenariCustomerDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DenariCustomers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        phone = c.String(),
                        email = c.String(),
                        CompanyName = c.String(),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        phone = c.String(),
                        email = c.String(),
                        role = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        ConfirmPassword = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserProfile");
            DropTable("dbo.DenariCustomers");
        }
    }
}
