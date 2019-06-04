namespace PJK.WPF.PRISM.PM2020.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSubTasks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectSubtask",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubTask = c.String(nullable: false, maxLength: 50),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectSubtask", "ProjectId", "dbo.Project");
            DropIndex("dbo.ProjectSubtask", new[] { "ProjectId" });
            DropTable("dbo.ProjectSubtask");
        }
    }
}
