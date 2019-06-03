namespace PJK.WPF.PRISM.PM2020.DataAccess.Migrations
{
    using PJK.WPF.PRISM.PM2020.Model;
    using System.Data.Entity.Migrations;

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
        }
    }
}