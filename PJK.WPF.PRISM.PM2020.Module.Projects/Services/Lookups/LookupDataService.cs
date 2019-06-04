using PJK.WPF.PRISM.PM2020.DataAccess;
using PJK.WPF.PRISM.PM2020.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.Services.Lookups
{
    public class LookupDataService : IProjectLookupDataService, ISystemItemLookupDataService
    {

        private PM202DbContext ctx;


        public LookupDataService()
        {
            ctx = new PM202DbContext();
        }

        public async Task<IEnumerable<LookupItem>> GetProjectLookupAsync()
        {
            return await ctx.Projects.AsNoTracking().Select(f =>
                new LookupItem
                {
                    Id = f.Id,
                    DisplayMember = f.ProjectName
                }
                ).ToListAsync();

        }


        public async Task<IEnumerable<LookupItem>> GetSystemItemLookupAsync()
        {
            return await ctx.SystemItems.AsNoTracking().Select(f =>
                new LookupItem
                {
                    Id = f.Id,
                    DisplayMember = f.SystemName
                }
                ).ToListAsync();

        }

    }
}
