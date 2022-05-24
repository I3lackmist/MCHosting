using Microsoft.AspNetCore.HttpOverrides;

using CMS.Data;
using CMS.Logic;

namespace CMS.Application {
    public class Application {
        public static void Main(string[] args) {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            DataLayer.RegisterSQLite(builder.Services, builder.Configuration);
            LogicLayer.RegisterServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            app.UseForwardedHeaders(new ForwardedHeadersOptions {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors( builder => {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });

            app.MapControllers();

            app.Run();
        }
    }
}