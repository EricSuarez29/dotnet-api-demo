namespace API.Data;

public class Order
{
    public int Id { get; set; }
    public int? ProductId { get; set; }
    public int? UserId { get; set; }
    public int? Quantity { get; set; }
    public double? Total { get; set; }
    public int Status { get; set; }
}
