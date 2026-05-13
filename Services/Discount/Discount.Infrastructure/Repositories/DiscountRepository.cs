using Dapper;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.Infrastructure.Repositories
{
    public class DiscountRepository(IConfiguration _configuration) : IDiscountRepository
    {
        public async Task<Coupon> GetDiscount(string productName)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<String>
                ("DatabaseSettings:ConnectionString"));
            var coupon =
                await connection.QueryFirstOrDefaultAsync<Coupon>("Select * From Coupon Where ProductName = @productName",
                new Coupon
                {
                    ProductName = productName
                });
            if (coupon is null) return new Coupon
            {
                Amount = 0,
                Description = "No Discount Available For This Product",
                ProductName = productName
            };
            return coupon;
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<String>
                ("DatabaseSettings:ConnectionString"));

            var affected = await connection.
                    ExecuteAsync("INSERT INTO Coupon (ProductName,Description,Amount) , VALUES (@ProductName,@Description,@Amount))",
                    new Coupon
                    {
                        ProductName = coupon.ProductName,
                        Description = coupon.Description,
                        Amount = coupon.Amount
                    });
            if (affected == 0) return false;
            else return true;
        }
        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<String>
                ("DatabaseSettings:ConnectionString"));

            var affected = await connection.
                    ExecuteAsync("UPDATE Coupon SET ProductName = @ProductName ,Description = @Description,Amount = @Amount WHERE Id = @Id ",
                    new Coupon
                    {
                        Id = coupon.Id,
                        ProductName = coupon.ProductName,
                        Description = coupon.Description,
                        Amount = coupon.Amount
                    });
            if (affected == 0) return false;
            else return true;
        }
        public async Task<bool> DeleteDiscount(string productName)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<String>
               ("DatabaseSettings:ConnectionString"));

            var affected = await connection.
                    ExecuteAsync("DELETE FROM Coupon  WHERE ProductName = @productName ",
                    new Coupon
                    {
                        ProductName = productName,
                    });
            if (affected == 0) return false;
            else return true;
        }
    }
}
