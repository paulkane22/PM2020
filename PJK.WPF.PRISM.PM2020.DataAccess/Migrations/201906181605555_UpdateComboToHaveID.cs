namespace PJK.WPF.PRISM.PM2020.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateComboToHaveID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComboLookup", "ComboId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ComboLookup", "ComboId");
        }
    }
}
