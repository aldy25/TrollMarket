using TrollMarket.Business.Interface;
using TrollMarket.Business.Repositories;

namespace TrollMarket.Persentation.Web.Configuration
{
    public static class ConfigurationBusinessService
    {
        public static IServiceCollection AddBusinessService(this IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IMerchandiseRepository, MerchandiseRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IShopRepository, ShopRepository>();
            services.AddScoped<IShipperRepository, ShipperRepository>();
            return services;
        }
    }
}
