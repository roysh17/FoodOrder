using FoodOrder.App.Helpers;
using FoodOrder.App.Interfaces;
using FoodOrder.Repository.Dapper;
using FoodOrder.Repository.Services;

namespace FoodOrder.App.Extensions;

public static class RepositoryServiceExtensions
{
    public static void AddRepositoryServiceExtensions(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped(typeof(AppDbContext));
        services.AddScoped<IJqueryAjaxUrlHelper, JqueryAjaxUrlHelper>();
    }
}
