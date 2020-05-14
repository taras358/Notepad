using Notepad.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notepad.Core.Interfaces.Repositories
{
    public interface IDebtorRepository : IBaseRepository<Debtor>
    {
        Task<Debtor> GetByFullName(string fullName);
        Task<Debtor> GetByIdWithIncludes(string id);
        Task<Debtor> GetByIdWithAllIncludes(string id);
        Task<List<Debtor>> GetAllWithIncludes();
        Task<List<Debtor>> GetAllWithAllIncludes();
        Task<List<Debtor>> FindByFullName(string query);
    }
}
