using Notepad.Core.Models.Requests;
using Notepad.Core.Models.Responses;
using System.Threading.Tasks;

namespace Notepad.Core.Interfaces.Services
{
    public interface IAccountService
    {
        Task<JwtAuthResponse> Login(LoginRequest requestModel);
        Task<JwtAuthResponse> RefreshToken(RefreshTokenRequest model);
        Task<UserResponse> CreateUser(CreateUserRequest createUserRequest);
        Task<UsersResponse> GetAllUsers();
        Task<UserResponse> GetUserById(string Id);
        Task<string> UpdateUser(UpdateUserRequest updateUserRequest);
        Task<string> DeleteUser(string id);
    }
}
