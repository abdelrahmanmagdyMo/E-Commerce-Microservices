
using Discount.Api.Services;
using Discount.Application.Mappers;
using Discount.Application.Queries;
using Discount.Core.Repositories;
using Discount.Infrastructure.Extensions;
using Discount.Infrastructure.Repositories;
using System.Reflection;

namespace Discount.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddAutoMapper(typeof(DiscountProfile).Assembly);
            builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(),
            Assembly.GetAssembly(typeof(GetDiscountQuery))));
            builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
            builder.Services.AddGrpc();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.MigrateDatabase<Program>();
            app.UseRouting();
            app.UseEndpoints(endPoint =>
            {
                endPoint.MapGrpcService<DiscountService>();
                endPoint.MapGet("/", async context =>
                {
                    context.Response.WriteAsync("Communication With gRPC Must Be Made Through gRPC Client");
                });
            });
            app.MapControllers();

            app.Run();
        }
    }
}
