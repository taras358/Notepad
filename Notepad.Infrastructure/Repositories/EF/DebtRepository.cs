using Microsoft.EntityFrameworkCore;
using Notepad.Core.DTOs;
using Notepad.Core.Entities;
using Notepad.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notepad.Infrastructure.Repositories.EF
{
    public class DebtRepository : BaseRepository<Debt>, IDebtRepository
    {
        public DebtRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = appDbContext.Set<Debt>();
        }

        public async Task<List<Debt>> FindByQuery(string debtorId, DateRangeDto rangeDto)
        {
            return await _dbSet
               .Where(x => x.DebtorId == debtorId && x.CreationDate.Date >= rangeDto.BeginDate.Date && x.CreationDate.Date <= rangeDto.EndDate.Date)
               .ToListAsync();
        }

        public async Task<List<Debt>> GetAllByDebtorId(string debtorId)
        {
            return await _dbSet.
                Where(x => x.DebtorId == debtorId)
                .ToListAsync();
        }
    }
}
