namespace PJK.WPF.PRISM.PM2020.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_ProjectSubtasks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ComboLookup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ComboName = c.String(nullable: false, maxLength: 50),
                        ComboGroupId = c.Int(nullable: false),
                        ComboOrder = c.Int(nullable: false),
                        ComboId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(nullable: false, maxLength: 20),
                        SystemId = c.Int(nullable: false),
                        Priority = c.Int(nullable: false),
                        Deadline = c.DateTime(nullable: false),
                        StatusId = c.Int(nullable: false),
                        Complete = c.Boolean(nullable: false),
                        Comment = c.String(nullable: false, maxLength: 5),
                        SystemItemId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SystemItem", t => t.SystemItemId)
                .Index(t => t.SystemItemId);
            
            CreateTable(
                "dbo.ProjectSubtask",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subtask = c.String(nullable: false, maxLength: 50),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.SystemItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SystemName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Project", "SystemItemId", "dbo.SystemItem");
            DropForeignKey("dbo.ProjectSubtask", "ProjectId", "dbo.Project");
            DropIndex("dbo.ProjectSubtask", new[] { "ProjectId" });
            DropIndex("dbo.Project", new[] { "SystemItemId" });
            DropTable("dbo.SystemItem");
            DropTable("dbo.ProjectSubtask");
            DropTable("dbo.Project");
            DropTable("dbo.ComboLookup");
        }
    }
}
