using Redis_NetCore.Models;

namespace Redis_NetCore.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAsync();
        Task<Product> GetByIdAsync(int id);
        Task<Product> AddAsync(Product product);
    }
}
