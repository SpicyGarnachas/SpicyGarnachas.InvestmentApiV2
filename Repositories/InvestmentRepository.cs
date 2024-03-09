using Dapper;
using MySql.Data.MySqlClient;
using SpicyGarnachas.InvestmentApiV2.Models;
using SpicyGarnachas.InvestmentApiV2.Repositories.Interfaces;

namespace SpicyGarnachas.InvestmentApiV2.Repositories;

public class InvestmentRepository : IInvestmentRepository
{
    private readonly ILogger<InvestmentRepository> logger;
    private readonly IConfiguration _configuration;

    public InvestmentRepository(ILogger<InvestmentRepository> logger, IConfiguration configuration)
    {
        this.logger = logger;
        _configuration = configuration;
    }

    public async Task<(bool IsSuccess, IEnumerable<InvestmentModel>?, string Message)> GetInvestmentData()
    {
        try
        {
            string connectionString = _configuration.GetConnectionString("mainServer");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Investment";
                var investment = await connection.QueryAsync<InvestmentModel>(sqlQuery);
                return investment.AsList().Count > 0 ? (IsSuccess: true, investment, string.Empty) : (IsSuccess: false, null, "Database without investments");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return (false, null, ex.Message);
        }
    }
    public async Task<(bool IsSuccess, IEnumerable<InvestmentModel>?, string Message)> GetInvestmentDataByPortfolioId(int id)
    {
        try
        {
            string? connectionString = _configuration.GetConnectionString("mainServer");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sqlQuery = $"SELECT * FROM Investment where portfolioId = {id}";
                var investment = await connection.QueryAsync<InvestmentModel>(sqlQuery);
                return investment.AsList().Count > 0 ? (IsSuccess: true, investment, string.Empty) : (IsSuccess: false, null, "User has no investments");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return (false, null, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, string Message)> CreateNewInvestment(InvestmentModel invest)
    {
        try
        {
            string? connectionString = _configuration.GetConnectionString("mainServer");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sqlQuery = $"INSERT INTO Investment (portfolioId, name, clasification, description, platform, type, sector, risk, liquidity, currencyCode, createdOn, updatedOn) VALUES ({invest.portfolioId}, '{invest.name}','{invest.clasification}',  '{invest.description}', '{invest.platform}', '{invest.type}', '{invest.sector}', {invest.risk}, {invest.liquidity},'{invest.currencyCode}', NOW(), NOW())";
                await connection.ExecuteAsync(sqlQuery);
                return (true, "Investment created successfully");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return (false, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, string Message)> ModifyInvestment(int id, string sqlQuery)
    {
        try
        {
            string? connectionString = _configuration.GetConnectionString("mainServer");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sqlQuery);
                return (true, "Investment modified successfully");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return (false, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, string Message)> DeleteInvestment(int id, int portfolioId)
    {
        try
        {
            string? connectionString = _configuration.GetConnectionString("mainServer");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sqlQuery = $"DELETE FROM Investment WHERE id = {id} AND portfolioId = {portfolioId}";
                await connection.ExecuteAsync(sqlQuery);
                return (true, "Investment deleted successfully");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return (false, ex.Message);
        }
    }
}