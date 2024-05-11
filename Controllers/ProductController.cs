using Microsoft.AspNetCore.Mvc;
using Redis_NetCore.Models;
using Redis_NetCore.Repositories;
using RedisApp.CacheLibrary;
using StackExchange.Redis;

namespace Redis_NetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok( await _productRepository.GetAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            return Ok( await _productRepository.AddAsync(product));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Created(string.Empty ,await _productRepository.GetByIdAsync(id));
        }


    }
}
