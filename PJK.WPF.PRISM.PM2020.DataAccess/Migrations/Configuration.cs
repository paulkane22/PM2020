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
                new Project { ProjectName = "Project1", SystemId = 1, Deadline = new System.DateTime(2019, 01, 01), Comment ="1" },
                new Project { ProjectName = "Project2", SystemId = 2, Deadline = new System.DateTime(2019, 01, 01), Comment = "2" },
                new Project { ProjectName = "Project3", SystemId = 3, Deadline = new System.DateTime(2019, 01, 01), Comment = "3" },
                new Project { ProjectName = "Project4", SystemId = 4, Deadline = new System.DateTime(2019, 01, 01), Comment = "4" }
                );

            context.SystemItems.AddOrUpdate(
                f => f.SystemName,
                new SystemItem { SystemName = "System 1"},
                new SystemItem { SystemName = "System 2" },
                new SystemItem { SystemName = "System 3" },
                new SystemItem { SystemName = "System 4" }
               );

            context.ComboLookups.AddOrUpdate(
                f => f.ComboName,
                new ComboLookup { ComboName = "High", ComboGroupId=1, ComboOrder =1 },
                new ComboLookup { ComboName = "Medium", ComboGroupId = 1, ComboOrder = 2 },
                new ComboLookup { ComboName = "Low", ComboGroupId = 1, ComboOrder = 3 },
                new ComboLookup { ComboName = "System 1", ComboGroupId = 2, ComboOrder = 1 },
                new ComboLookup { ComboName = "System 2", ComboGroupId = 2, ComboOrder = 2 }
               );


            context.SaveChanges();

            context.Subtasks.AddOrUpdate(st => st.SubTask,
                new ProjectSubtask { SubTask = "Do some work!", ProjectId = context.Projects.First().Id });


        }
    }
}
