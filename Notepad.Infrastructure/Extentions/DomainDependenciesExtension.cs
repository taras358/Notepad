using Microsoft.Extensions.DependencyInjection;
using Notepad.Core.Interfaces.Repositories;
using Notepad.Core.Interfaces.Services;
using Notepad.Core.Services;
using Notepad.Infrastructure.Repositories.EF;

namespace Notepad.Infrastructure.Extentions
{
    public static class DomainDependenciesExtension
    {
        public static void AddDomainDependencies(this IServiceCollection services)
        {

            services.AddScoped<IUserRepository, UserRepository>();



            services.AddScoped<IAccountService, AccountService>();
        }
    }
}
