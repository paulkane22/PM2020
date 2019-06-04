namespace PJK.WPF.PRISM.PM2020.DataAccess.Migrations
{
    using PJK.WPF.PRISM.PM2020.Model;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PJK.WPF.PRISM.PM2020.DataAccess.PM202DbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PJK.WPF.PRISM.PM2020.DataAccess.PM202DbContext context)
        {

            context.Projects.AddOrUpdate(
                f => f.ProjectName,
                new Project { ProjectName = "Project1", SystemId = 1, Deadline = new System.DateTime(2019, 01, 01) },
                new Project { ProjectName = "Project2", SystemId = 2, Deadline = new System.DateTime(2019, 01, 01) },
                new Project { ProjectName = "Project3", SystemId = 3, Deadline = new System.DateTime(2019, 01, 01) },
                new Project { ProjectName = "Project4", SystemId = 4, Deadline = new System.DateTime(2019, 01, 01) }
                );

            context.SystemItems.AddOrUpdate(
                f => f.SystemName,
                new SystemItem { SystemName = "System 1"},
                new SystemItem { SystemName = "System 2" },
                new SystemItem { SystemName = "System 3" },
                new SystemItem { SystemName = "System 4" }
               );

            context.SaveChanges();

            context.Subtasks.AddOrUpdate(st => st.SubTask,
                new ProjectSubtask { SubTask = "Do some work!", ProjectId = context.Projects.First().Id });


        }
    }
}
