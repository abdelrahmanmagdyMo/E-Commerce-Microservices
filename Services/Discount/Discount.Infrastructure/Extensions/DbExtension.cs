using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.Infrastructure.Extensions
{
    public static class DbExtension
    {
        public static IHost MigrateDatabase<TContext>(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var config = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();
                try
                {
                    logger.LogInformation("Discount Db Migration Started");
                    ApplyMigrations(config);
                    logger.LogInformation("Discount Db Migration Completed");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Cannot Create Database Migration ");
                    throw;
                }
                return host;
            }
        }

        private static async Task ApplyMigrations(IConfiguration config)
        {
            var attempts = 5;
            while (attempts > 0)
            {
                try
                {
                    await using var connection = new NpgsqlConnection(config.GetValue<string>("DatabaseSettings:ConnectionString"));
                    connection.Open();
                    using var cmd = new NpgsqlCommand()
                    {
                        Connection = connection,
                    };
                    cmd.CommandText = "Drop Table If Exists Coupon";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = @"CREATE TABLE COUPON (ID SERIAL PRIMARY KEY , ProductName VARCHAR(500) NOT NULL , Description TEXT , Amount INT)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO COUPON (ProductName,Description,Amount) Values ('Egypt Adidas Quick Force Indoor Badminton Shoes','Adidas Discount',600)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO COUPON (ProductName,Description,Amount) Values ('PowerFit 19 FH Rubber Spike Cricket Shoes','PowerFit Discount',700)";
                    cmd.ExecuteNonQuery();
                    break;
                }
                catch (Exception ex)
                {
                    attempts--;
                    if (attempts == 0)
                    {
                        throw;
                    }
                    Thread.Sleep(2000);

                }
            }
        }
    }
}
