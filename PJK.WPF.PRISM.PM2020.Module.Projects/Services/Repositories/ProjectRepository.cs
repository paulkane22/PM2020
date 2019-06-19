using PJK.WPF.PRISM.PM2020.DataAccess;
using PJK.WPF.PRISM.PM2020.Model;
using PJK.WPF.PRISM.PM2020.Module.Projects.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.Services.Repositories
{
    public class ProjectRepository : GenericRepository<Project, PM202DbContext>, IProjectRepository
    {
        public ProjectRepository(PM202DbContext context) :base(context)
        {
        }

        public override async Task<Project> GetByIdAsync(int projectId)
        {
            return await Context.Projects
                .Include(f => f.ProjectSubtasks)
                .SingleAsync(f => f.Id == projectId);
        }

    }
}


