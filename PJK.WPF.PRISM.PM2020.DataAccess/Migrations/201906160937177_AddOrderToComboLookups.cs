namespace PJK.WPF.PRISM.PM2020.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderToComboLookups : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComboLookup", "ComboOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ComboLookup", "ComboOrder");
        }
    }
}
