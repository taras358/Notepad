using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Notepad.Core.Entities;
using System.Reflection;

namespace Notepad.Infrastructure.Extentions
{
    public static class DbContextExtention
    {
        public static void AddCustomDbContext(this IServiceCollection services, string connectionString)
        {
            var assemblyName = Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name;
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly(assemblyName));
            }, ServiceLifetime.Scoped);

            services
                .AddDefaultIdentity<User>(options =>
                {
                    options.Password.RequireDigit = true;
                })
                .AddEntityFrameworkStores<AppDbContext>();
        }
    }
}