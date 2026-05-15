
using Asp.Versioning;
using Basket.Application.GrpcServices;
using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Core.Repositories;
using Basket.Infrastructure.Repositories;
using Discount.gRPC.Protos;
using System.Reflection;

namespace Basket.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddAutoMapper(typeof(BasketMappingProfile));
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(),
                    Assembly.GetAssembly(typeof(GetBasketByUserNameQuery)));
            });
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddScoped<DiscountGrpcService>();
            builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(cfg =>
            {
                cfg.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
            });

            builder.Services.AddSwaggerGen(op =>
            {
                op.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo()
                {
                    Title = "Basket Api",
                    Version = "v1",
                    Description = "This Is API For Basket Microservice In Ecommerce Application. ",
                    Contact = new Microsoft.OpenApi.OpenApiContact()
                    {
                        Email = "abdelrahman.magdy.dev@gmail.com",
                        Name = "Abdo Magdy",
                        Url = new Uri("https://YourWebsite.eg.com")
                    }
                });
            });
            builder.Services.AddApiVersioning(op =>
            {
                op.DefaultApiVersion = new ApiVersion(1, 0);
                op.ReportApiVersions = true;
                op.AssumeDefaultVersionWhenUnspecified = true;

            });
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
            });
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
