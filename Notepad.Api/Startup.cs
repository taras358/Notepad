using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Notepad.Api.Middlewares;
using Notepad.Core.Constants;
using Notepad.Infrastructure.Extentions;
using Notepad.Infrastructure.Helpers;
using Notepad.Infrastructure.Options;
using System;
using System.Linq;

namespace Notepad.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDomainDependencies();

            services.Configure<AuthTokenOption>(Configuration.GetSection(typeof(AuthTokenOption).Name));

            services.AddCustomDbContext(Configuration.GetConnectionString("DefaultConnection"));
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Notepad",
                    Version = "v1"
                });
            });
            ConfigureAutomapper(services);
            ConfigureCors(services, Configuration);
            services.AddAuthOptions(Configuration.GetSection("AuthTokenOption:JwtKey").Value);
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            loggerFactory.AddFile(Configuration.GetSection("Logging"));
            app.UseCors("OriginPolicy");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notepad");
            });
            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();
            app.UseMiddleware<HttpMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public static void ConfigureAutomapper(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AllowNullCollections = true;
                mc.AddProfile(new MapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private void ConfigureCors(IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection corsOptions = configuration.GetSection("Cors");
            string origins = corsOptions["Origins"];
            services.AddCors(options =>
            {
                options.AddPolicy("OriginPolicy", builder =>
                {
                    builder.WithOrigins(origins.Split(",", StringSplitOptions.RemoveEmptyEntries).ToArray())
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials().WithExposedHeaders(ExceptionConstants.TokenExpiredHeader, ExceptionConstants.InvalidRefresh);
                });
            });
        }
    }
}