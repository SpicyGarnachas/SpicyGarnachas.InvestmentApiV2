﻿using SpicyGarnachas.InvestmentApiV2.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using SpicyGarnachas.InvestmentApiV2.Models;
using Dapper;

namespace SpicyGarnachas.InvestmentApiV2.Repositories;

public class PortfolioRepository : IPortfolioRepository
{
    private readonly ILogger<PortfolioRepository> logger;
    private readonly IConfiguration _configuration;
    private readonly string connectionString;

    public PortfolioRepository(ILogger<PortfolioRepository> logger, IConfiguration configuration)
    {
        this.logger = logger;
        connectionString = configuration.GetConnectionString("mainServer");
    }
    //ExecuteAsync(sqlQuery, new {});
    public async Task<(bool IsSuccess, IEnumerable<PortfolioModel>?, string Message)> GetPortfolioData()
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Portfolio";
                var portfolio = await connection.QueryAsync<PortfolioModel>(sqlQuery);

                return portfolio.AsList().Count > 0 ? (IsSuccess: true, portfolio, string.Empty) : (IsSuccess: false, null, "Database without portfolios");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return (false, null, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, IEnumerable<PortfolioModel>?, string Message)> GetPortfolioByUserId(int id)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sqlQuery = $"SELECT * FROM Portfolio WHERE userId = @id";
                var portfolio = await connection.QueryAsync<PortfolioModel>(sqlQuery, new { id = id });

                return portfolio.AsList().Count > 0 ? (IsSuccess: true, portfolio, string.Empty) : (IsSuccess: false, null, "User has no portfolios");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return (false, null, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, string Message)> CreateNewPortfolio(PortfolioModel portfolio)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sqlQuery = $"INSERT INTO Portfolio (userId, name, description, currencyCode, createdOn, updatedOn) VALUES (@userId,@name,@description,@currencyCode,@expenseLimit ,NOW(),NOW());";
                await connection.ExecuteAsync(sqlQuery, new {userId = portfolio.userId, name = portfolio.name, description = portfolio.description, currencyCode = portfolio.currencyCode, expenseLimit  = portfolio.expenseLimit  });

                return (IsSuccess: true, Message: "Portfolio created successfully");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return (false, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, string Message)> ModifyPorfolio(PortfolioModel portfolio, string sqlQuery)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new { name = portfolio.name, description = portfolio.description, currencyCode = portfolio.currencyCode, portfolioId = portfolio.id, userId = portfolio.userId, expenseLimit = portfolio.expenseLimit });

                return (IsSuccess: true, Message: "Portfolio updated successfully");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return (false, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, string Message)> DeletePortfolio(int id, int userId)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sqlQuery = $"DELETE FROM Portfolio WHERE Id = @id AND userId = @userId";
                await connection.ExecuteAsync(sqlQuery, new { id = id, userId = userId });

                return (IsSuccess: true, Message: "Portfolio deleted successfully");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return (false, ex.Message);
        }
    }
}