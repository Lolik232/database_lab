namespace rest_api.v1.DTO;
public class ShopDetailDTO
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public virtual ICollection<ProductStockDTO> ProductStocks { get; set; }

    public ShopDetailDTO(long id, string name, string address, ICollection<ProductStockDTO> productStocks)
    {
        Id = id;
        Name = name;
        Address = address;
        ProductStocks = productStocks;
    }
}
