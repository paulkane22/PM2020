using PJK.WPF.PRISM.PM2020.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PJK.WPF.PRISM.PM2020.DataAccess
{
    public class PM202DbContext: DbContext
    {
        public PM202DbContext():base("PM2020Db")
        {

        }
        
        public DbSet<Project> Projects { get; set; }
        public DbSet<SystemItem> SystemItems { get; set; }

        public DbSet<ProjectSubtask> Subtasks { get; set; }
        public DbSet<ComboLookup> ComboLookups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}
