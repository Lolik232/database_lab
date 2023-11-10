using DAL.Entities;

namespace DAL.Interfaces;



public interface IShopRepository :
    IBaseGetRepository<Shop>.WithGetAll
{
    public List<Rating> GetProductRating(int Id);
}
