using System;

namespace Notepad.Infrastructure.Options
{
    public class AuthTokenOption
    {
        public string JwtKey { get; set; }
        public TimeSpan AccessTokenExpiration { get; set; } = TimeSpan.FromMinutes(30);
        public TimeSpan RefreshTokenExpiration { get; set; } = TimeSpan.FromDays(60);
    }
}
