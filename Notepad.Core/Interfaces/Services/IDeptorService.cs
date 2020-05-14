using Notepad.Core.Models.Requests;
using Notepad.Core.Models.Responses;
using System.Threading.Tasks;

namespace Notepad.Core.Interfaces.Services
{
    public interface IDeptorService
    {
        Task<string> Create(CreateDebtorRequest request);
        Task<string> Update(UpdateDebtorRequest request);
        Task<DebtorResponse> GetById(string debtorId);
        Task<DebtorsResponse> GetAll();
        Task<DebtorsResponse> FindByFullName(string query);
    }
}
