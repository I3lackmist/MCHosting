using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using CMS.Data.Sqlite;
using CMS.Data.Sqlite.Adapters;

namespace CMS.Data {
    public static class DataLayer {
        private static string _connectionString = "";

        public static void RegisterSQLite(IServiceCollection services, IConfiguration configuration) {
            _connectionString = configuration.GetConnectionString("sqlite");

            services.AddDbContext<SQLiteDbContext>( options => {
                options.UseSqlite(_connectionString);
            });

            services.AddScoped<GameServersAdapter>();
            services.AddScoped<UsersAdapter>();
            services.AddScoped<ServerFlavorsAdapter>();
            services.AddScoped<UserGameServersAdapter>();
        }
    }
}