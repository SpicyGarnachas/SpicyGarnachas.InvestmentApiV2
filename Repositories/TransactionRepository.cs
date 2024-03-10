using Dapper;
using MySql.Data.MySqlClient;
using SpicyGarnachas.FinanceApiV2.Models;
using SpicyGarnachas.FinanceApiV2.Repositories.Interfaces;

namespace SpicyGarnachas.FinanceApiV2.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly ILogger<TransactionRepository> logger;
    private readonly IConfiguration _configuration;
    private readonly string connectionString;

    public TransactionRepository(ILogger<TransactionRepository> logger, IConfiguration configuration)
    {
        this.logger = logger;
        connectionString = configuration.GetConnectionString("mainServer");
    }
    public async Task<(bool IsSuccess, IEnumerable<TransactionModel>?, string Message)> GetTransactionsData()
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Transaction";
                var transactions = await connection.QueryAsync<TransactionModel>(sqlQuery);
                return transactions.AsList().Count > 0 ? (IsSuccess: true, transactions, string.Empty) : (IsSuccess: false, null, "Database without Transactions");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return (false, null, ex.Message);
        }
    }
    public async Task<(bool IsSuccess, IEnumerable<TransactionModel>?, string Message)> GetTransactionsDataByPortfolioId(int id)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sqlQuery = $"SELECT * FROM Transaction WHERE portfolioId = @id";
                var transactions = await connection.QueryAsync<TransactionModel>(sqlQuery, new { id = id});
                return transactions.AsList().Count > 0 ? (IsSuccess: true, transactions, string.Empty) : (IsSuccess: false, null, "User has no Transactions in this portfolio");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return (false, null, ex.Message);
        }
    }
}