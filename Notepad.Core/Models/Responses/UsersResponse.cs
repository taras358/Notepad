using System.Collections.Generic;

namespace Notepad.Core.Models.Responses
{
    public class UsersResponse
    {
        public List<UserResponse> Users { get; set; } = new List<UserResponse>();
    }
}
