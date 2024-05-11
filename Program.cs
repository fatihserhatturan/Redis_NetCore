using Microsoft.EntityFrameworkCore;
using Redis_NetCore.Models;
using Redis_NetCore.Repositories;
using RedisApp.CacheLibrary;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductRepository>(sp =>
{
    var appdbcontext = sp.GetRequiredService<AppDbContext>();
    var productRepository = new ProductRepository(appdbcontext);
    var redisService = sp.GetRequiredService<RedisService>();

    return new ProductRepositoryWithCacheDecorator(productRepository, redisService);
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("ProductDb");
});

builder.Services.AddSingleton<RedisService>(sp=>
{
    return new RedisService(builder.Configuration["CacheOptions:Url"]);
});

builder.Services.AddSingleton<IDatabase>(sp=>
{
    var redisService = sp.GetRequiredService<RedisService>();
    return redisService.GetDb(0);
});

var app = builder.Build();

using var scope = app.Services.CreateScope();
    var dbcontext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbcontext.Database.EnsureCreated();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
