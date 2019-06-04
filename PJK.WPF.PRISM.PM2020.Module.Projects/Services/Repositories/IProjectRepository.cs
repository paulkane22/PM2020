using PJK.WPF.PRISM.PM2020.Model;
using PJK.WPF.PRISM.PM2020.Module.Projects.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.Services.Repositories
{ 
    public interface IProjectRepository
    {
        Task<Project> GetProjectByIdAsync(int projectId);
        Task SaveAsync();
        bool HasChanges();
        ProjectList GetProjects();
        void Add(Project project);
        void Remove(Project model);
    }
}