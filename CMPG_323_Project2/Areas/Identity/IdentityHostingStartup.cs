using System;
using CMPG_323_Project2.Areas.Identity.Data;
using CMPG_323_Project2.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CMPG_323_Project2.Areas.Identity.IdentityHostingStartup))]
namespace CMPG_323_Project2.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AutContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AutContextConnection")));

                services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<AutContext>();
            });
        }
    }
}