using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Notepad.Core.Constants;
using Notepad.Infrastructure.Extentions;
using Notepad.Infrastructure.Helpers;
using Notepad.Infrastructure.Options;
using System;
using System.Linq;

namespace Notepad.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
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

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("OriginPolicy");

            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp/dist";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
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
                    builder
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials().WithExposedHeaders(ExceptionConstants.TokenExpiredHeader, ExceptionConstants.InvalidRefresh);
                });
            });
        }
    }
}
