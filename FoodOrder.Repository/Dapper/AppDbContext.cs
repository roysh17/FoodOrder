using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FoodOrder.Repository.Dapper;

public class AppDbContext
{
    private ILogger<AppDbContext> _logger;
    private readonly IConfiguration _configuration;

    public AppDbContext(ILogger<AppDbContext> logger,
       IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }
    public SqlConnection OpenConnection()
    {
        try
        {
            var connectionString = _configuration.GetConnectionString("FoodOrderDbConnection");
            var connection = new SqlConnection(connectionString);
            connection.Open();

            return connection;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed connect to database: {ex.Message}");
            throw new ArgumentException("Failed connect to database, please contact the administrator to check the error log.");
        }
    }
}
