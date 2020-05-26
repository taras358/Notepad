using Microsoft.EntityFrameworkCore;
using Notepad.Core.Entities;
using Notepad.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Notepad.Infrastructure.Repositories.EF
{
    public class DeptorRepository : BaseRepository<Debtor>, IDebtorRepository
    {
        public DeptorRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = _appDbContext.Set<Debtor>();
        }
        public async Task<Debtor> GetByFullName(string fullName)
        {
            return await _dbSet
                    .Where(x => x.FullName == fullName)
                    .FirstOrDefaultAsync();
        }

        public async Task<Debtor> GetByIdWithIncludes(string id)
        {
            return await _dbSet
                   .Where(x => x.Id == id)
                   .Include(x => x.Debts)
                   .FirstOrDefaultAsync();
        }
        public async Task<Debtor> GetByIdWithAllIncludes(string id)
        {
            return await _dbSet
                   .Where(x => x.Id == id)
                   .Include(x => x.Debts)
                   .FirstOrDefaultAsync();
        }

        public async Task<List<Debtor>> GetAllWithIncludes()
        {
            return await _dbSet
                    .Include(x => x.Debts)
                    .ToListAsync();
        }
        public async Task<List<Debtor>> GetAllWithAllIncludes()
        {
            return await _dbSet
                    .Include(x => x.Debts)
                    .IgnoreQueryFilters()
                    .ToListAsync();
        }

        public async Task<List<Debtor>> FindByFullName(string query)
        {
            return await _dbSet
                     .Where(p => p.FullName.Contains(query))
                     .ToListAsync();
        }
    }
}
