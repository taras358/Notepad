using Notepad.Core.Entities;
using System.Threading.Tasks;

namespace Notepad.Core.Interfaces.Repositories
{
    public interface IUserProfileRepository : IBaseRepository<UserProfile>
    {
        Task<UserProfile> GetByUserId(string userId);
    }
}
