using PJK.WPF.PRISM.PM2020.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Mana.Services.Lookups
{
    public interface ISystemItemLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetSystemItemLookupAsync();
    }
}
