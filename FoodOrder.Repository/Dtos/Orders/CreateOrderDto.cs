namespace FoodOrder.Repository.Dtos.Orders;

public class CreateOrderDto
{
    public DateTime OrderDate { get; set; }
    public string? OrderNo { get; set; }
    public decimal TotalPrice { get; set; }
    public bool IsPaid { get; set; }
    public string? CustomerName { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
}
