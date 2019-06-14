using System.Collections.Generic;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Mana.Services.Repositories
{
    public interface IGenericRepository<T> 
    {

        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task SaveAsync();
        bool HasChanges();
        void Add(T model);
        void Remove(T model);
    }
}