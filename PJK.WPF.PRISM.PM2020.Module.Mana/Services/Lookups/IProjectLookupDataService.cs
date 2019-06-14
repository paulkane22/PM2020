using PJK.WPF.PRISM.PM2020.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Mana.Services.Lookups
{
    public interface IProjectLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetProjectLookupAsync();
    }
}
