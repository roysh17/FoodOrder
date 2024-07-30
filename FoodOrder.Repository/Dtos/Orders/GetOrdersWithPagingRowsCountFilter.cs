namespace FoodOrder.Repository.Dtos.Orders;

public class GetOrdersWithPagingRowsCountFilter : BaseWithPagingFilter
{
    public string? KeySearch { get; set; } 
}
