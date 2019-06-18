namespace PJK.WPF.PRISM.PM2020.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateValidationTest : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Project", "Comment", c => c.String(nullable: false, maxLength: 5));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Project", "Comment", c => c.String());
        }
    }
}
