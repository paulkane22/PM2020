using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PJK.WPF.PRISM.PM2020.Module.Projects.Model;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.Services
{
    public class ProjectRepository : IProjectRepository
    {
        public ProjectRepository()
        {

        }

        public Task<Project> AddProjectAsync(Project project)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProjectAsync(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public Task<Project> GetProjectsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Project>> GetProjectssAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Project> UpdateProjectAsync(Project project)
        {
            throw new NotImplementedException();
        }
    }
}



