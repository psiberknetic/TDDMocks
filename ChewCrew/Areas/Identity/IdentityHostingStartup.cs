using System;
using ChewCrew.Areas.Identity.Data;
using ChewCrew.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(ChewCrew.Areas.Identity.IdentityHostingStartup))]
namespace ChewCrew.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<ChewCrewContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ChewCrewContextConnection")));

                services.AddDefaultIdentity<ChewCrewUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ChewCrewContext>();
            });
        }
    }
}