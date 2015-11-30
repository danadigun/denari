namespace CRIMAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LoanInterests", "intrestAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LoanInterests", "intrestAmount", c => c.String());
        }
    }
}
