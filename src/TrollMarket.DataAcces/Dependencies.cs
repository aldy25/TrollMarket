using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAcces.Models;

namespace TrollMarket.DataAcces
{
    public static class Dependencies
    {
        public static void AddDataAccessServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TrollMarketContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TrollMarketConnection"))
                );
        }
    }
}
