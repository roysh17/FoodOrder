namespace FoodOrder.Repository.Dtos.Orders;

public class GetOrdersWithPagingFilter : BaseWithPagingFilter
{
    public string? KeySearch { get; set; }
    public string? PaidStatus { get; set; }
    public int Page { get; set; }
    public int Draw { get; set; }
    public int Start { get; set; }

}
