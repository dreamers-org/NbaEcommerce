using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NbaEcommerce.Areas.Identity.Data;
using NbaEcommerce.Models;

[assembly: HostingStartup(typeof(NbaEcommerce.Areas.Identity.IdentityHostingStartup))]
namespace NbaEcommerce.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<IdentityContext>(options => options.UseSqlServer(context.Configuration.GetConnectionString("IdentityContextConnection")));

                services.AddIdentity<NbaEcommerceUser, IdentityRole>(config =>
                {
                    config.SignIn.RequireConfirmedEmail = false;
                })
                    .AddEntityFrameworkStores<IdentityContext>()
                     .AddDefaultTokenProviders();
            });
        }
    }
}