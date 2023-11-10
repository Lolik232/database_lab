namespace DAL.Entities;

public partial class CashReceipt
{
    public long Id { get; set; }

    public DateTime Date { get; set; }

    public long? ShopId { get; set; }

    public virtual ICollection<ReceiptProduct> ReceiptProducts { get; set; } = new List<ReceiptProduct>();

    public virtual Shop? Shop { get; set; }
}
