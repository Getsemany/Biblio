using System;
using Biblio.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Biblio.Areas.Identity.IdentityHostingStartup))]
namespace Biblio.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
           /* builder.ConfigureServices((context, services) => {
                services.AddDbContext<BiblioIdentityDbContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("BiblioIdentityDbContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<LibrosContext>();
            });*/
        }
    }
}