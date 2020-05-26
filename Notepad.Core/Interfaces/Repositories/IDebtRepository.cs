using Notepad.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notepad.Core.Interfaces.Repositories
{
    public interface IDebtRepository : IBaseRepository<Debt>
    {
        Task<List<Debt>> GetAllByDebtorId(string debtorId);
        Task<List<Debt>> FindByQuery(string debtorId, DateTimeOffset beginDate, DateTimeOffset endDate);
    }
}
