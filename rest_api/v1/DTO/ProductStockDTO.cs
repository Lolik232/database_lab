namespace rest_api.v1.DTO;
public class ProductStockDTO
{
    public int? Count { get; set; }

    public ProductDTO? Product { get; set; }

    public ProductStockDTO(int? count, ProductDTO? product)
    {
        Count = count;
        Product = product;
    }
}

public class ProductDTO
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public ProductDTO(long id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
}

