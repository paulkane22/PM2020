using System.Collections.Generic;
using System.Threading.Tasks;
using PJK.WPF.PRISM.PM2020.Model;

namespace PJK.WPF.PRISM.PM2020.Module.Mana.Services.Lookups
{
    public interface ILookupDataService
    {
        Task<IEnumerable<LookupItem>> GetComboLookupAsync(ComboGroups groupId = ComboGroups.Priority);
    }
}