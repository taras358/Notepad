using Notepad.Core.Entities;
using Notepad.Core.Interfaces.Repositories;

namespace Notepad.Infrastructure.Repositories.EF
{
    public class UserProfileRepository : BaseRepository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = _appDbContext.Set<UserProfile>();
        }
    }
}
