using Dapper;
using FoodOrder.Repository.Dapper;
using FoodOrder.Repository.Dtos.Foods;
using FoodOrder.Repository.Dtos.Orders;

namespace FoodOrder.Repository.Services;

public class OrderService : IOrderService
{
    private readonly AppDbContext _dbContext;
    public OrderService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> AddOrder(CreateOrderDto orderToAdd)
    {
        using (var context = _dbContext.OpenConnection())
        {
            var query = @"
                INSERT INTO [dbo].[Order]
                       ([OrderNo]
                       ,[OrderDate]
                       ,[TotalPrice]
                       ,[CustomerName]
                       ,[CreatedBy]
                       ,[CreatedDate])
                 VALUES
                       (@OrderNo
                       ,@OrderDate 
                       ,@TotalPrice 
                       ,@CustomerName 
                       ,@CreatedBy 
                       ,@CreatedDate)

                SELECT CAST(SCOPE_IDENTITY() AS INT);

                ";

            var queryParams = new
            {
                OrderNo = orderToAdd.OrderNo,
                OrderDate = orderToAdd.OrderDate,
                TotalPrice = orderToAdd.TotalPrice,
                CustomerName = orderToAdd.CustomerName,
                CreatedBy = orderToAdd.CreatedBy,
                CreatedDate = orderToAdd.CreatedDate
            };

            var lastInsertId = await context.QuerySingleAsync<int>(query, queryParams);

            return lastInsertId;
        }
    }

    public async Task<int> AddOrderItem(CreateOrderItemDto orderItemToAdd)
    {
        using (var context = _dbContext.OpenConnection())
        {
            var query = @"
                INSERT INTO [dbo].[OrderItem]
                       ([OrderNo]
                       ,[FoodId]
                       ,[Qty]
                       ,[TotalPrice]
                       ,[CreatedBy]
                       ,[CreatedDate])
                 VALUES
                       (@OrderNo
                       ,@FoodId 
                       ,@Qty 
                       ,@TotalPrice 
                       ,@CreatedBy 
                       ,@CreatedDate)
                ";

            var queryParams = new
            {
                OrderNo = orderItemToAdd.OrderNo,
                FoodId = orderItemToAdd.FoodId,
                Qty = orderItemToAdd.Qty,
                TotalPrice = orderItemToAdd.TotalPrice,
                CreatedBy = orderItemToAdd.CreatedBy,
                CreatedDate = orderItemToAdd.CreatedDate
            };

            var result = await context.ExecuteAsync(query, queryParams);

            return result;
        }
    }

    public async Task<Food> GetFoodById(int foodId)
    {
        using (var context = _dbContext.OpenConnection())
        {
            var query = @"
                SELECT [FoodId]
                      ,[Name]
                      ,[Price]
                      ,[CreatedBy]
                      ,[CreatedDate]
                      ,[ModifiedBy]
                      ,[ModifiedDate]
                FROM [FoodOrderDb].[dbo].[Food] 
                WHERE [FoodId]=@FoodId
                ";

            var queryParams = new
            {
                FoodId = foodId
            };

            var result = await context.QueryFirstOrDefaultAsync<Food>(query, queryParams);

            return result;
        }
    }

    public async Task<IEnumerable<Food>> GetFoods()
    {
        using (var context = _dbContext.OpenConnection())
        {
            var query = @"
                SELECT [FoodId]
                      ,[Name]
                      ,[Price]
                      ,[CreatedBy]
                      ,[CreatedDate]
                      ,[ModifiedBy]
                      ,[ModifiedDate]
                  FROM [FoodOrderDb].[dbo].[Food]             
                ";

            var queryParams = new { };

            var result = await context.QueryAsync<Food>(query, queryParams);

            return result;
        }
    }

    public async Task<Order> GetOrderById(int orderId)
    {
        using (var context = _dbContext.OpenConnection())
        {
            var query = @"
                  SELECT a.[OrderId]
                      ,a.[OrderNo]
                      ,a.[OrderDate]
                      ,a.[TotalPrice]
                      ,ISNULL(a.[IsPaid],0) [IsPaid]
                      ,CASE WHEN a.[IsPaid] = 1 THEN 'Lunas' ELSE 'Belum bayar' END AS [PaidStatus]
                      ,a.[CustomerName]
                      ,a.[CreatedBy]
                      ,a.[CreatedDate]
                      ,a.[ModifiedBy]
                      ,a.[ModifiedDate]
                  FROM [Order] a
                  WHERE a.[OrderId]=@OrderId         
              ";

            var queryParams = new
            {
                OrderId = orderId
            };

            var result = await context.QueryFirstOrDefaultAsync<Order>(query, queryParams);

            return result;
        }
    }

    public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderId(string orderNo)
    {
        using (var context = _dbContext.OpenConnection())
        {
            var query = @"
                  SELECT a.[OrderItemId]
                      ,a.[OrderNo]
                      ,a.[FoodId]
	                  ,b.[Name] AS [FoodName]
                      ,b.[Price] [ItemPrice]
                      ,a.[Qty]
                      ,a.[TotalPrice]
                      ,a.[CreatedBy]
                      ,a.[CreatedDate]
                      ,a.[ModifiedBy]
                      ,a.[ModifiedDate]
                  FROM [OrderItem] a
                  INNER JOIN [Food] b ON a.[FoodId]=b.[FoodId]
                  WHERE a.[OrderNo]=@OrderNo     
              ";

            var queryParams = new
            {
                OrderNo = orderNo
            };

            var result = await context.QueryAsync<OrderItem>(query, queryParams);

            return result;
        }
    }

    public async Task<IEnumerable<Order>> GetOrdersWithPaging(GetOrdersWithPagingFilter filter)
    {
        using (var context = _dbContext.OpenConnection())
        {
            var query = @"
                SELECT  *
                FROM    ( 
		                    SELECT ROW_NUMBER() OVER ( ORDER BY a.[OrderId] ) AS RowNum
		                          ,a.[OrderId]
                                  ,a.[OrderNo]
                                  ,a.[OrderDate]
                                  ,a.[TotalPrice]
                                  ,ISNULL(a.[IsPaid],0) [IsPaid]
                                  ,CASE WHEN a.[IsPaid] = 1 THEN 'Lunas' ELSE 'Belum bayar' END AS [PaidStatus]
                                  ,a.[CustomerName]
                                  ,a.[CreatedBy]
                                  ,a.[CreatedDate]
                                  ,a.[ModifiedBy]
                                  ,a.[ModifiedDate]
                              FROM [Order] a
                              WHERE (LOWER(a.[OrderNo]) LIKE @OrderNo OR LOWER(a.[CustomerName]) LIKE @CustomerName)
                                    AND ISNULL(a.[IsPaid],0)=@PaidStatus
                        ) AS RowConstrainedResult
                WHERE   RowNum >= @RowNumberStart AND RowNum <= @RowNumberEnd
                ORDER BY RowNum
                ";

            filter.KeySearch = filter.KeySearch ?? "";

            var queryParams = new
            {
                OrderNo = $"%{filter.KeySearch.Trim().ToLower()}%",
                CustomerName = $"%{filter.KeySearch.Trim().ToLower()}%",
                PaidStatus = filter.PaidStatus,
                RowNumberStart = filter.Start + 1,
                RowNumberEnd = filter.Start + filter.Length
            };

            var result = await context.QueryAsync<Order>(query, queryParams);

            return result;
        }
    }

    public async Task<int> GetOrdersWithPagingRowsCount(GetOrdersWithPagingFilter filter)
    {
        using (var context = _dbContext.OpenConnection())
        {
            var query = @"
                 SELECT COUNT(1) AS TotalRows  
                 FROM [Order] a
                 WHERE (LOWER(a.[OrderNo]) LIKE @OrderNo OR LOWER(a.[CustomerName]) LIKE @CustomerName)
                       AND ISNULL(a.[IsPaid],0)=@PaidStatus
                ";

            filter.KeySearch = filter.KeySearch ?? "";
            var queryParams = new
            {
                OrderNo = $"%{filter.KeySearch.Trim().ToLower()}%",
                CustomerName = $"%{filter.KeySearch.Trim().ToLower()}%",
                PaidStatus = filter.PaidStatus,
            };

            var result = await context.QueryFirstOrDefaultAsync<int>(query, queryParams);

            return result;
        }
    }

    public async Task<int> UpdatePaymentStatus(int orderId)
    {
        using (var context = _dbContext.OpenConnection())
        {
            var query = @"
                UPDATE [Order] SET IsPaid=1 WHERE [OrderId]=@OrderId
                ";

            var queryParams = new
            {
                OrderId = orderId
            };

            var result = await context.ExecuteAsync(query, queryParams);

            return result;
        }
    }
}
