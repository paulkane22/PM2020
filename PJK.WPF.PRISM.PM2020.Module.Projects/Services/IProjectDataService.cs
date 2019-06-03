using PJK.WPF.PRISM.PM2020.Model;
using PJK.WPF.PRISM.PM2020.Module.Projects.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.Services
{
    public interface IProjectDataService
    {
        Task<Project> GetProjectByIdAsync(int projectId);
        Task SaveAsync(Project project);
        ProjectList GetProjects();
    }
}