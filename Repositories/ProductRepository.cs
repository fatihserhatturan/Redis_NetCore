using Microsoft.EntityFrameworkCore;
using Redis_NetCore.Models;

namespace Redis_NetCore.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public  ProductRepository(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Product> AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}
