namespace PJK.WPF.PRISM.PM2020.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Project", name: "SystemItem_Id", newName: "SystemItemId");
            RenameIndex(table: "dbo.Project", name: "IX_SystemItem_Id", newName: "IX_SystemItemId");
            CreateTable(
                "dbo.ComboLookup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ComboName = c.String(nullable: false, maxLength: 50),
                        ComboGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ComboLookup");
            RenameIndex(table: "dbo.Project", name: "IX_SystemItemId", newName: "IX_SystemItem_Id");
            RenameColumn(table: "dbo.Project", name: "SystemItemId", newName: "SystemItem_Id");
        }
    }
}
