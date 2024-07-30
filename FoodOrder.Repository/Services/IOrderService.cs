using FoodOrder.Repository.Dtos.Foods;
using FoodOrder.Repository.Dtos.Orders;

namespace FoodOrder.Repository.Services;

public interface IOrderService
{
    Task<IEnumerable<Food>> GetFoods(); 
    Task<Food> GetFoodById(int foodId); 
    Task<Order> GetOrderById(int orderId); 
    Task<IEnumerable<OrderItem>> GetOrderItemsByOrderId(string orderNo); 
    Task<IEnumerable<Order>> GetOrdersWithPaging(GetOrdersWithPagingFilter filter); 
    Task<int> GetOrdersWithPagingRowsCount(GetOrdersWithPagingFilter filter); 
    Task<int> AddOrder(CreateOrderDto orderToAdd);
    Task<int> AddOrderItem(CreateOrderItemDto orderItemToAdd);
    Task<int> UpdatePaymentStatus(int orderId);

}
