using Microsoft.Build.Framework;
using Redis_NetCore.Models;
using RedisApp.CacheLibrary;
using StackExchange.Redis;
using System.Text.Json;

namespace Redis_NetCore.Repositories
{
    public class ProductRepositoryWithCacheDecorator : IProductRepository
    {
        private const string CacheKey = "ProductList";
        private readonly IProductRepository _productRepository;
        private readonly RedisService _redisService;
        private readonly IDatabase _cacheRepository;

        public ProductRepositoryWithCacheDecorator(IProductRepository productRepository, RedisService redisService)
        {
            _productRepository = productRepository;
            _redisService = redisService;
            _cacheRepository = _redisService.GetDb(2);
        }

        public async Task<Product> AddAsync(Product product)
        {
            var newproduct = await _productRepository.AddAsync(product);

            if(await _cacheRepository.KeyExistsAsync(CacheKey))
            {
				await _cacheRepository.HashSetAsync(CacheKey, newproduct.Id, JsonSerializer.Serialize(newproduct));
			}
            return newproduct;
        }

        public async Task<List<Product>> GetAsync()
        {
			if(!await _cacheRepository.KeyExistsAsync(CacheKey))
            return await LoadToCacheFromDbAsync();
			
            var products = new List<Product>();
            var cahceProducts = await _cacheRepository.HashGetAllAsync(CacheKey);
            foreach (var product in cahceProducts.ToList())
            {
				products.Add(JsonSerializer.Deserialize<Product>(product.Value));
                
			}
            return products;
		}

        public async Task<Product> GetByIdAsync(int id)
        {
            if(!await _cacheRepository.KeyExistsAsync(CacheKey))
            { 
                var product = await _cacheRepository.HashGetAsync(CacheKey, id);
                return product.HasValue ? JsonSerializer.Deserialize<Product>(product) : null;
            }
            var products = await LoadToCacheFromDbAsync();

            return products.FirstOrDefault(p => p.Id == id);
        }

        private async Task<List<Product>> LoadToCacheFromDbAsync()
        {
			var products = await _productRepository.GetAsync();
				
			products.ForEach(product =>
            {
				_cacheRepository.HashSetAsync(CacheKey, product.Id,JsonSerializer.Serialize(product));
			});

			return products;
		}
    }
}
