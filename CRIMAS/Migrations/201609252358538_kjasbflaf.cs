namespace CRIMAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kjasbflaf : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DenariStates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        States = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DenariStates");
        }
    }
}
