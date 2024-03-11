using Dapper;
using MySql.Data.MySqlClient;
using SpicyGarnachas.FinanceApiV2.Models;
using SpicyGarnachas.FinanceApiV2.Repositories.Interfaces;

namespace SpicyGarnachas.FinanceApiV2.Repositories;

public class FinanceRepository : IFinanceRepository
{
    private readonly ILogger<FinanceRepository> logger;
    private readonly IConfiguration _configuration;
    private readonly string connectionString;

    public FinanceRepository(ILogger<FinanceRepository> logger, IConfiguration configuration)
    {
        this.logger = logger;
        connectionString = configuration.GetConnectionString("mainServer");
    }

    public async Task<(bool IsSuccess, IEnumerable<FinanceModel>?, string Message)> GetFinanceData()
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Finance";
                var investment = await connection.QueryAsync<FinanceModel>(sqlQuery);

                return investment.AsList().Count > 0 ? (IsSuccess: true, investment, string.Empty) : (IsSuccess: false, null, "Database without investments");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return (false, null, ex.Message);
        }
    }
    public async Task<(bool IsSuccess, IEnumerable<FinanceModel>?, string Message)> GetFinanceDataByPortfolioId(int id)
    {
        try 
        { 
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sqlQuery = $"SELECT * FROM Finance where portfolioId = @id";
                var investment = await connection.QueryAsync<FinanceModel>(sqlQuery, new { id = id });

                return investment.AsList().Count > 0 ? (IsSuccess: true, investment, string.Empty) : (IsSuccess: false, null, "User has no investments");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return (false, null, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, string Message)> CreateNewFinance(FinanceModel invest)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sqlQuery = $"INSERT INTO Finance (portfolioId, name, clasification, description, platform, type, sector, risk, liquidity, currencyCode, createdOn, updatedOn) VALUES (@portfolioId, @name, @clasification, @description, @platform, @type, @sector, @risk, @liquidity, @currencyCode, NOW(), NOW())";
                await connection.ExecuteAsync(sqlQuery, new { portfolioId = invest.portfolioId, name = invest.name, clasification = invest.clasification, description = invest.description, platform = invest.platform, type = invest.type, sector = invest.sector, risk = invest.risk, liquidity = invest.liquidity, currencyCode = invest.currencyCode });

                return (true, "Finance created successfully");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return (false, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, string Message)> ModifyFinance(FinanceModel investment, string sqlQuery)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new { id = investment.id, portfolioId = investment.portfolioId, name = investment.name, clasification = investment.clasification, description = investment.description, platform = investment.platform, type = investment.type, sector = investment.sector, risk = investment.risk, liquidity = investment.liquidity, currencyCode = investment.currencyCode });

                return (true, "Finance modified successfully");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return (false, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, string Message)> DeleteFinance(int id, int portfolioId)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sqlQuery = $"DELETE FROM Finance WHERE id = @id AND portfolioId = @portfolioId";
                await connection.ExecuteAsync(sqlQuery, new { id = id, portfolioId = portfolioId });

                return (true, "Finance deleted successfully");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return (false, ex.Message);
        }
    }
}