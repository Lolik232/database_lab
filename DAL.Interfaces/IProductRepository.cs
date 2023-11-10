using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IProductRepository :
        IBaseGetRepository<Product>.WithGetAll
    {

    }
}
