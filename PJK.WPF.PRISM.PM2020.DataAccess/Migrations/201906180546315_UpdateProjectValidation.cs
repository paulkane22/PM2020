namespace PJK.WPF.PRISM.PM2020.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProjectValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Project", "SystemId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Project", "SystemId", c => c.Int());
        }
    }
}
