namespace CRIMAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addednewfieldtoLoanDocuments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoanDocuments", "filename", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LoanDocuments", "filename");
        }
    }
}
