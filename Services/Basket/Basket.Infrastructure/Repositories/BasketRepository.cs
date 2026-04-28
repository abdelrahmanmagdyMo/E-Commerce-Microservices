using Basket.Core.Entities;
using Basket.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;


namespace Basket.Infrastructure.Repositories
{
    public class BasketRepository(IDistributedCache redisCache, ILogger<BasketRepository> logger) : IBasketRepository
    {
        private const int ExpirationInDays = 7;
        public async Task<ShoppingCart?> GetBasket(string userName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userName))
                    throw new ArgumentException("User Name Is Required", nameof(userName));
                var key = GetBasketKey(userName);
                var basket = await redisCache.GetStringAsync(key);
                if (string.IsNullOrEmpty(basket))
                {
                    logger.LogError($"Basket Not Found For User : {userName}", userName);
                    return null;
                }
                logger.LogInformation($"Basket Retrived For User : {userName}", userName);
                return JsonSerializer.Deserialize<ShoppingCart>(basket);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error While Retrieving for user {userName} ");
                throw;
            }
        }

        public async Task<ShoppingCart?> UpdateBasket(ShoppingCart cart)
        {
            try
            {
                if (cart == null)
                    throw new ArgumentException(nameof(cart));
                if (string.IsNullOrWhiteSpace(cart.UserName))
                    throw new ArgumentException("User Name Is Required", nameof(cart.UserName));
                var key = GetBasketKey(cart.UserName);
                var optins = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(ExpirationInDays)
                };
                var serializeBasket = JsonSerializer.Serialize(cart);
                await redisCache.SetStringAsync(key, serializeBasket, optins);
                logger.LogInformation("Basket updated for user {UserName}", cart.UserName);
                return await GetBasket(key);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while updating basket for user {UserName}", cart?.UserName);
                throw;
            }

        }
        public async Task<bool> DeleteBasket(string userName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userName))
                    throw new ArgumentException("User Name Is Required", nameof(userName));
                var key = GetBasketKey(userName);
                var basket = await redisCache.GetStringAsync(key);
                if (string.IsNullOrEmpty(basket))
                {
                    logger.LogWarning("No basket found to delete for user {UserName}", userName);
                    return false;
                }
                await redisCache.RemoveAsync(key);
                logger.LogInformation("Basket deleted for user {UserName}", userName);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error While Retrieving for user {userName} ");
                throw;
            }
        }
        private static string GetBasketKey(string userName)
        {
            return $"basket:{userName}";
        }
    }
}
