using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using mysite.Data;
using mysite.Data.FileManager;
using mysite.Data.Repository;
using mysite.Extensions;
using mysite.Interfaces;
using mysite.Services;

namespace mysite
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
     
            _config = config;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices(_config);
            services.AddIdentity<IdentityUser, IdentityRole>(options => 
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            })
            //.AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<DataContext>();
            services.ConfigureApplicationCookie(options => {
                options.LoginPath = "/Auth/Login";
            
            });
            services.AddTransient<IPortRepository, PortRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<ItruckRepository, TruckRepository>();
            services.AddTransient<IFileManager, FileManager>();
            services.AddMvc(options => 
            {
                options.CacheProfiles.Add("Monthly", new CacheProfile { Duration= 60*60* 24 *7 *4});
            
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();

            // app.Run(async (context) =>
            // {
            //  await context.Response.WriteAsync("Hello World!");
            // });
        }
    }
}
