namespace DAL.Entities;

public partial class Shop
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<CashReceipt> CashReceipts { get; set; } = new List<CashReceipt>();

    public virtual ICollection<ProductStock> ProductStocks { get; set; } = new List<ProductStock>();
}

public class Rating
{
    public Category Category { get; set; }
    public int CountProducts { get; set; }

    public Rating(Category category, int countProducts)
    {
        Category = category;
        CountProducts = countProducts;
    }
}