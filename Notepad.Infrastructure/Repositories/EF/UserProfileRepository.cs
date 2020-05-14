using Notepad.Core.Entities;
using Notepad.Core.Interfaces.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Notepad.Infrastructure.Repositories.EF
{
    public class UserProfileRepository : BaseRepository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = _appDbContext.Set<UserProfile>();
        }

        public async Task<UserProfile> GetByUserId(string userId)
        {
            return _dbSet
                .Where(x => x.UserId == userId)
                .SingleOrDefault();
        }
    }
}
