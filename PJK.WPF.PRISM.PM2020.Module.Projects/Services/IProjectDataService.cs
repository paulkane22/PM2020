using PJK.WPF.PRISM.PM2020.Model;
using PJK.WPF.PRISM.PM2020.Module.Projects.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.Services
{
    public interface IProjectDataService
    {
        Task<List<Project>> GetAllAsync();
        ProjectList GetProjects();
    }
}