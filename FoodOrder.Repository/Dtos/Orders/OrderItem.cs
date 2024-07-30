using System.ComponentModel.DataAnnotations;

namespace FoodOrder.Repository.Dtos.Orders;

public class OrderItem
{
    [Key]
    public int OrderItemId { get; set; }
    public string? OrderNo { get; set; }
    public int FoodId { get; set; }
    public string? FoodName { get; set; }
    public int Qty { get; set; }
    public decimal ItemPrice { get; set; }    
    public decimal TotalPrice { get; set; }    
    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
