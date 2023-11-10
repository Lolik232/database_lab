using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DAL.PostgreSQL.EfCore.Repositories;

public class ShopRepository : IShopRepository
{
    private readonly ChainOfShopsLuckContext _dbContext;

    public ShopRepository(ChainOfShopsLuckContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Shop Get(int id)
    {
        var shop = _dbContext.Shops
                .Include(s => s.ProductStocks.Where(p => p.Count != 0))
                    .ThenInclude(ps => ps.Product)
                .AsNoTracking()
                .FirstOrDefault(s => s.Id.Equals(id));

        return shop ?? throw new Exception("Эксепшн?");
    }

    public IEnumerable<Shop> GetAll()
    {
        return _dbContext.Shops
                .Include(s => s.ProductStocks.Where(p => p.Count != 0))
                    .ThenInclude(ps => ps.Product)
                .AsNoTracking()
                .ToList();
    }

    public Task<IEnumerable<Shop>> GetAllAsync()
    {
        return Task.Run(() => GetAll());
    }

    public Task<Shop> GetAsync(int id)
    {
        return Task.Run(() => Get(id));
    }

    public List<Rating> GetProductRating(int Id)
    {
        var query =
            (from rp in _dbContext.Set<ReceiptProduct>()
             join cr in _dbContext.Set<CashReceipt>()
                 on rp.ReceiptId equals cr.Id
             join shop in _dbContext.Set<Shop>()
                 on cr.ShopId equals shop.Id
             join prod in _dbContext.Set<Product>()
                 on rp.ProductId equals prod.Id
             join category in _dbContext.Set<Category>()
                 on prod.CategoryId equals category.Id
             select new { category, shop, rp })
            .GroupBy(c => c.shop)
            .Select(s =>
                new
                {
                    s.Key,
                    categories =
                        s
                            .GroupBy(c => c.category)
                            .Select(cat =>
                                new
                                {
                                    key = cat.Key,
                                    countProducts =
                                        cat.Sum(r => r.rp.CountProducts)
                                }
                            ).OrderByDescending(e => e.countProducts).AsEnumerable()

                }).First(s => s.Key.Id == Id);


        var result = query.categories.Select(c => new Rating(c.key, (int)c.countProducts)).ToList();

        return result;
    }
}
