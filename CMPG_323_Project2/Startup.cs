using Azure.Storage.Blobs;
using CMPG_323_Project2.Data;
using CMPG_323_Project2.Logic;
using CMPG_323_Project2.Models;
using CMPG_323_Project2.Repository;
using CMPG_323_Project2.ViewModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMPG_323_Project2
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
            services.AddControllersWithViews();
            services.AddDbContext<CMPG_DBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AutContextConnection")));
            services.AddScoped(options => {
                return new BlobServiceClient(Configuration.GetConnectionString("AzureBlobStorage"));
                });
            services.AddScoped<IFileManagerLogic, FileManagerLogic>();
            services.AddTransient<IGenericRepository<Photo>, GenericRepository<Photo>>();
            services.AddTransient<IGenericRepository<AspNetUser>, GenericRepository<AspNetUser>>();
            services.AddTransient<IGenericRepository<UserPhoto>, GenericRepository<UserPhoto>>();
            services.AddTransient<IGenericRepository<MetaDatum>, GenericRepository<MetaDatum>>();
            services.AddTransient<IGenericRepository<Album>, GenericRepository < Album>>();
            services.AddTransient<IGenericRepository<Contain>, GenericRepository<Contain>>();
            services.AddTransient<IGenericRepository<ShareAlbum>, GenericRepository<ShareAlbum>>();
            //services.AddTransient<IGenericRepository<UserViewModelPhoto>, GenericRepository<UserViewModelPhoto>>();
            services.AddRazorPages();
        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
      
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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
