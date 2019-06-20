using PJK.WPF.PRISM.PM2020.Model;

namespace PJK.WPF.PRISM.PM2020.Module.Mana.Services.Repositories
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        void RemoveSubtask(ProjectSubtask model);
    }
}
