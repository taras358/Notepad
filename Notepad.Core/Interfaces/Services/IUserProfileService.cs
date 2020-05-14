using Notepad.Core.Models.Requests;
using Notepad.Core.Models.Responses;
using System.Threading.Tasks;

namespace Notepad.Core.Interfaces.Services
{
    public interface IUserProfileService
    {
        Task<ProfileResponse> GetUserProfile(string userId);
        Task<string> UpdateUserProfile(ProfileRequest profile);

    }
}
