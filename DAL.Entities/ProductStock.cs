namespace DAL.Entities;

public partial class ProductStock
{
    public long Id { get; set; }

    public long? ShopId { get; set; }

    public long? ProductId { get; set; }

    public int? Count { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Shop? Shop { get; set; }
}
