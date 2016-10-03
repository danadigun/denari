namespace CRIMAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_document_url : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoanDocuments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoanId = c.Int(nullable: false),
                        accountNo = c.Int(nullable: false),
                        docUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LoanDocuments");
        }
    }
}
