
using Catalog.Infrastructure.Data.Contexts;
using Catalog.Infrastructure.Repositories;
using CatalogApplication.Mappers;
using CatalogApplication.Queries;
using CatalogCore.Repositories;
using System.Reflection;

namespace CatalogApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<ICatalogContext, CatalogContext>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IBrandRepository, ProductRepository>();
            builder.Services.AddScoped<ITypeRepository, ProductRepository>();
            builder.Services.AddAutoMapper(typeof(ProductMappingProfile).Assembly);
            builder.Services.AddMediatR
                (cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(),
                Assembly.GetAssembly(typeof(GetProductByIdQuery))));
            builder.Services.AddApiVersioning(op =>
            {
                op.ReportApiVersions = true;
                op.AssumeDefaultVersionWhenUnspecified = true;
                op.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);

            });
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
                {
                    Title = "Catalog API",
                    Version = "v1",
                    Description = "This Is API For Catalog Microservice In Ecommerce Application. ",
                    Contact = new Microsoft.OpenApi.OpenApiContact
                    {
                        Email = "abdelrahman.magdy.dev@gmail.com",
                        Name = "Abdo Magdy",
                        Url = new Uri("https://YourWebsite.eg.com")
                    }
                });
            });
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ICatalogContext>();
                await BrandContextSeed.SeedDataAsync(context.Brands);
                await TypeContextSeed.SeedDataAsync(context.Types);
                await ProductSeedContext.SeedDataAsync(context.Products);
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            await app.RunAsync();
        }
    }
}
