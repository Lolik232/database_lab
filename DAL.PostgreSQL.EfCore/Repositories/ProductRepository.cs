using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.PostgreSQL.EfCore.Repositories;
public class ProductRepository : IProductRepository
{
    private readonly ChainOfShopsLuckContext _dbContext;

    public ProductRepository(ChainOfShopsLuckContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Product Get(int id)
    {
        var product = _dbContext.Products
            .Include(p => p.ProductStocks)
            .AsNoTracking()
            .FirstOrDefault(p => p.Id == id);

        return product ?? throw new Exception("Нужно создать эксепшены");
    }

    public IEnumerable<Product> GetAll()
    {
        return _dbContext.Products.AsNoTracking().ToList();
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return Task.Run(() => GetAll());
    }

    public Task<Product> GetAsync(int id)
    {
        return Task.Run(() => Get(id));
    }
}
