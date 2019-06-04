using System.Collections.Generic;
using System.Threading.Tasks;
using PJK.WPF.PRISM.PM2020.Model;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.Services.Lookups
{
    public interface ISystemItemLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetSystemItemLookupAsync();
    }
}