using FoodOrder.App.Interfaces;

namespace FoodOrder.App.Helpers;

public class JqueryAjaxUrlHelper : IJqueryAjaxUrlHelper
{
    public string GetBaseUrl()
    {
        return "https://localhost:7065";
    }

    public string GetFoodByFoodIdUrl()
    {
        return $"{GetBaseUrl()}/orders/GetFoodByFoodIdInJson";
    }

    public string GetOrdersByPagingUrl()
    {
        return $"{GetBaseUrl()}/orders/GetOrdersInJson";
    }

}
