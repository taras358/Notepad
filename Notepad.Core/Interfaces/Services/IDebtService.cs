using Notepad.Core.Models.Requests;
using System.Threading.Tasks;

namespace Notepad.Core.Interfaces.Services
{
    public interface IDebtService
    {
        Task Create(CreateDebtRequest request);
        Task Delete(DeleteDebtRequest request);
    }
}
