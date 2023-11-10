namespace DAL.Entities;

public partial class ReceiptProduct
{
    public long Id { get; set; }

    public long? ReceiptId { get; set; }

    public long? ProductId { get; set; }

    public int? CountProducts { get; set; }

    public virtual Product? Product { get; set; }

    public virtual CashReceipt? Receipt { get; set; }
}
