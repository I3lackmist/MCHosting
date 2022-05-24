using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using CMS.Logic.Services;

namespace CMS.Logic {
    public static class LogicLayer {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration) {
            services.AddScoped<UsersService>();
            services.AddScoped<GameServerService>();
            services.AddSingleton<GameVersionService>();
            services.AddScoped<ServerFlavorService>();
        }
    }
}