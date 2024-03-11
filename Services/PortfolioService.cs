using SpicyGarnachas.InvestmentApiV2.Repositories.Interfaces;
using SpicyGarnachas.InvestmentApiV2.Services.Interfaces;
using SpicyGarnachas.InvestmentApiV2.Models;

namespace SpicyGarnachas.InvestmentApiV2.Services;

public class PortfolioService : IPortfolioService
{
    private readonly IPortfolioRepository repository;
    private readonly ILogger<PortfolioService> logger;

    public PortfolioService(IPortfolioRepository repository, ILogger<PortfolioService> logger)
    {
        this.repository = repository;
        this.logger = logger;
    }
    public async Task<(bool IsSuccess, IEnumerable<PortfolioModel>?, string Message)> GetPortfolioData()
    {
        try
        {
            var (IsSuccess, Result, Message) = await repository.GetPortfolioData();

            return IsSuccess ? (true, Result, string.Empty) : (false, null, Message);
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
            var (IsSuccess, Result, Message) = await repository.GetPortfolioByUserId(id);

            return IsSuccess ? (true, Result, string.Empty) : (false, null, Message);
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
            var (IsSuccess, Message) = await repository.CreateNewPortfolio(portfolio); 
             
            return IsSuccess.Equals(true) ? (true, string.Empty) : (false, Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return (false, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, string Message)> ModifyPorfolio(PortfolioModel portfolio)
    {
        try
        {
            bool isFirst = true;
            string sqlQuery = string.Empty;
            List<string> updateFields = new List<string>();

            if (portfolio.name != null)
            {
                updateFields.Add($"name = @name");
            }

            if (portfolio.description != null)
            {
                updateFields.Add($"description = @description");
            }

            if (portfolio.currencyCode != null)
            {
                updateFields.Add($"currencyCode = @currencyCode");
            }

            if (portfolio.expenseLimit != null)
            {
                updateFields.Add($"expenseLimit = @expenseLimit");
            }


            updateFields.Add($"updatedOn = NOW()");

            foreach (string field in updateFields)
            {
                if (isFirst)
                {
                    sqlQuery += $"UPDATE Portfolio SET {field}";
                    isFirst = false;
                }
                else
                {
                    sqlQuery += $", {field}";
                }
            }

            sqlQuery += $" WHERE id = @portfolioId AND userId = @userId";

            var (IsSuccess, Message) = await repository.ModifyPorfolio(portfolio, sqlQuery);

            return IsSuccess.Equals(true) ? (true, string.Empty) : (false, Message);
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
            var (IsSuccess, Message) = await repository.DeletePortfolio(id, userId);

            return IsSuccess.Equals(true) ? (true, string.Empty) : (false, Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return (false, ex.Message);
        }
    }
}