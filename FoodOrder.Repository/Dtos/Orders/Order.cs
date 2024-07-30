using System.ComponentModel.DataAnnotations;

namespace FoodOrder.Repository.Dtos.Orders;

public class Order
{
    [Key]
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }    
    public string? OrderNo { get; set; }
    public decimal TotalPrice { get; set; }
    public bool IsPaid { get; set; }
    public string? PaidStatus { get; set; }
    public string? CustomerName { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
}

