namespace PJK.WPF.PRISM.PM2020.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSystemList : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SystemItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SystemName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Project", "SystemItem_Id", c => c.Int());
            AlterColumn("dbo.Project", "SystemId", c => c.Int());
            CreateIndex("dbo.Project", "SystemItem_Id");
            AddForeignKey("dbo.Project", "SystemItem_Id", "dbo.SystemItem", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Project", "SystemItem_Id", "dbo.SystemItem");
            DropIndex("dbo.Project", new[] { "SystemItem_Id" });
            AlterColumn("dbo.Project", "SystemId", c => c.Int(nullable: false));
            DropColumn("dbo.Project", "SystemItem_Id");
            DropTable("dbo.SystemItem");
        }
    }
}
