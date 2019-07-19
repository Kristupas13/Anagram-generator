using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AnagramGenerator.WebApp.Services;
using AnagramGenerator.EF.CodeFirst.Repositories;
using AnagramGenerator.EF.CodeFirst.Services;
using AnagramGenerator.EF.CodeFirst.Interfaces;
using AnagramGenerator.EF.CodeFirst;
using Microsoft.EntityFrameworkCore;

namespace AnagramGenerator.WebApp
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


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Domain = null;
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                options.Cookie.SameSite = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            services.AddDbContext<CFDB_AnagramSolverContext>(options =>
            options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("connectionString")));

            services.AddScoped<IManagerRepository, ManagerRepository>();
            services.AddScoped<ICacheRepository, CacheRepository>();
            services.AddScoped<IWordRepository, WordRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAnagramSolver, AnagramSolver>();
            services.AddScoped<IUserLogRepository, UserLogRepository>();
            services.AddScoped<ITextRepository, TextRepository>();
            services.AddScoped<IRequestRepository, RequestRepository>();

            services.AddScoped<IModificationService, ModificationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRequestService, RequestService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
