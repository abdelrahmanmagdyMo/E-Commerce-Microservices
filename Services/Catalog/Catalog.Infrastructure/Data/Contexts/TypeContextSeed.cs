using CatalogCore.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data.Contexts
{
    public static class TypeContextSeed
    {
        public static async Task SeedDataAsync(IMongoCollection<ProductType> typeCollection)
        {
            var hasTypes = await typeCollection.Find(_ => true).AnyAsync();
            if (hasTypes) return;
            var filePath = Path.Combine("Data", "SeedData", "types.json");
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Seed File Does Not Exist : {filePath}");
                return;
            }
            var typeData = await File.ReadAllTextAsync(filePath);
            var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);
            if (types?.Any() is true)
            {
                typeCollection.InsertMany(types);
            }
        }
    }
}
