using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NETD3202_ASasitharan_Lab5_Comm2.Data;

[assembly: HostingStartup(typeof(NETD3202_ASasitharan_Lab5_Comm2.Areas.Identity.IdentityHostingStartup))]
namespace NETD3202_ASasitharan_Lab5_Comm2.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}