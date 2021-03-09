using FirstLiveDemo.Models;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

[assembly: FunctionsStartup(typeof(FirstLiveDemo.Startup))]
namespace FirstLiveDemo
{
   public class Startup: FunctionsStartup
    {
        public IConfiguration Configuration { get; }
        public override void Configure(IFunctionsHostBuilder builder)
        {
            
            builder.Services.AddDbContext<StudentDbContext>(item => item.UseSqlServer(Configuration.GetConnectionString("mycs")));
        }
    }
}
