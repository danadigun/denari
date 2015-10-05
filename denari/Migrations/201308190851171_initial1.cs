namespace CRIMAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoanInterests",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        accountNo = c.String(),
                        intrestAmount = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LoanInterests");
        }
    }
}
