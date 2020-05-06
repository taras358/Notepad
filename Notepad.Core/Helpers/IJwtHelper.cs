using Notepad.Core.Entities;
using Notepad.Core.Models.Responses;
using System.Threading.Tasks;

namespace Notepad.Core.Helpers
{
    public interface IJwtHelper
    {
        JwtAuthResponse GenerateToken(User user);
    }
}
