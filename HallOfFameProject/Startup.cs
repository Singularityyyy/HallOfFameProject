using System;
using HallOfFameProject.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using HallOfFameProject.Service;
using HallOfFameProject.Data.Models;
using Audit.EntityFramework.Providers;
using Microsoft.Extensions.Logging;

namespace HallOfFameProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Hall of Fame Task", Version = "v1" });
            });            
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<ISkillService, SkillService>();
            services.AddDbContext<HallOfFameDbContext>(options =>
                options
                .UseLazyLoadingProxies()
                .UseSqlServer(
                    Configuration.GetConnectionString("HallOfFameConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HOF V1");
                c.RoutePrefix = string.Empty;
                //c.IndexStream = () => GetType().GetTypeInfo().Assembly
                //    .GetManifestResourceStream("HallOfFame.Web.root.index.html"); // requires file to be added as an embedded resource
            });
            Audit.Core.Configuration.DataProvider = new EntityFrameworkDataProvider()
            {
                AuditTypeMapper = t => t == typeof(Skill) ? typeof(AuditSkill) : null,
                AuditEntityAction = (evt, entry, auditEntity) =>
                {
                    var a = (dynamic)auditEntity;
                    a.AuditDate = DateTime.UtcNow;
                    a.AuditAction = entry.Action; // Insert, Update
                    
                    return true; 
                }
            };
            
            loggerFactory.AddFile("Logs/myapp-{Date}.txt");

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
