using System.ComponentModel.DataAnnotations;

namespace FoodOrder.Repository.Dtos.Orders;

public class CreateOrderItemDto
{    
    public string OrderNo { get; set; }
    public int FoodId { get; set; }
    public int Qty { get; set; }
    public decimal TotalPrice { get; set; }    
    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
}
