﻿using Microsoft.Extensions.DependencyInjection;
using Notepad.Core.Helpers;
using Notepad.Core.Interfaces.Repositories;
using Notepad.Core.Interfaces.Services;
using Notepad.Core.Services;
using Notepad.Infrastructure.Helpers;
using Notepad.Infrastructure.Repositories.EF;

namespace Notepad.Infrastructure.Extentions
{
    public static class DomainDependenciesExtension
    {
        public static void AddDomainDependencies(this IServiceCollection services)
        {

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IDebtorRepository, DeptorRepository>();
            services.AddScoped<IDebtRepository, DebtRepository>();



            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IDeptorService, DeptorService>();
            services.AddScoped<IDebtService, DebtService>();


            services.AddScoped<IJwtHelper, JwtHelper>();
        }
    }
}
