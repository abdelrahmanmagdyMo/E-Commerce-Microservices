using CatalogCore.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data.Contexts
{
    public static class ProductSeedContext
    {
        public static async Task SeedDataAsync(IMongoCollection<Product> productCollection)
        {
            var hasProducts = await productCollection.Find(_ => true).AnyAsync();
            if (hasProducts) return;
            var filePath = Path.Combine("Data", "SeedData", "products.json");
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Seed File Does Not Exist : {filePath}");
                return;
            }
            var productData = await File.ReadAllTextAsync(filePath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var products = JsonSerializer.Deserialize<List<Product>>(productData, options);
            if (products?.Any() is true)
                productCollection.InsertMany(products);


        }
    }
}
