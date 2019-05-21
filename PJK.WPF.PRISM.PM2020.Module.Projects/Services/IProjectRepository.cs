using PJK.WPF.PRISM.PM2020.Module.Projects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.Services
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetProjectssAsync();
        Task<Project> GetProjectsAsync(Guid id);
        Task<Project> AddProjectAsync(Project project);
        Task<Project> UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(Guid projectId);
    }
}
