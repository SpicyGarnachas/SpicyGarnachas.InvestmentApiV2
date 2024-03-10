using SpicyGarnachas.InvestmentApiV2.Models;
using SpicyGarnachas.InvestmentApiV2.Repositories.Interfaces;
using SpicyGarnachas.InvestmentApiV2.Services.Interfaces;

namespace SpicyGarnachas.InvestmentApiV2.Services;

public class InvestmentService : IInvestmentService
{
    private readonly IInvestmentRepository repository;
    private readonly ILogger<InvestmentService> logger;

    public InvestmentService(IInvestmentRepository repository, ILogger<InvestmentService> logger)
    {
        this.repository = repository;
        this.logger = logger;
    }

    public async Task<(bool IsSuccess, IEnumerable<InvestmentModel>?, string Message)> GetInvestmentData()
    {
        try
        {
            var (IsSuccess, Result, Message) = await repository.GetInvestmentData();
            return IsSuccess ? (true, Result, string.Empty) : (false, null, Message);
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
            var (IsSuccess, Result, Message) = await repository.GetInvestmentDataByPortfolioId(id);
            return IsSuccess ? (true, Result, string.Empty) : (false, null, Message);
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
            var (IsSuccess, Message) = await repository.CreateNewInvestment(invest);
            return IsSuccess ? (true, Message) : (false, Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return (false, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, string Message)> ModifyInvestment(InvestmentModel investment)
    {
        try
        {
            bool isFirst = true;
            string sqlQuery = string.Empty;
            List<string> updateFields = new List<string>();

            if (investment.name != null || investment.name != string.Empty)
            {
                updateFields.Add($"name = @name");
            }

            if (investment.clasification != null || investment.clasification != string.Empty)
            {
                updateFields.Add($"clasification = @clasification");
            }

            if (investment.description != null || investment.description != string.Empty)
            {
                updateFields.Add($"description = @description");
            }

            if (investment.platform != null || investment.platform != string.Empty)
            {
                updateFields.Add($"platform = @platform");
            }

            if (investment.type != null || investment.type != string.Empty)
            {
                updateFields.Add($"type = @type");
            }

            if (investment.sector != null || investment.sector != string.Empty)
            {
                updateFields.Add($"sector = @sector");
            }

            if(investment.currencyCode != null || investment.currencyCode != string.Empty)
            {
                updateFields.Add($"currencyCode = @currencyCode");
            }

            updateFields.Add($"risk = @risk");
            updateFields.Add($"liquidity = @liquidity");
            updateFields.Add($"updatedOn = NOW()");

            foreach (string field in updateFields)
            {
                if (isFirst)
                {
                    sqlQuery += $"UPDATE investment SET {field}";
                    isFirst = false;
                }
                else
                {
                    sqlQuery += $", {field}";
                }
            }

            sqlQuery += $" WHERE id = @id AND portfolioId = @portfolioId";

            var (IsSuccess, Message) = await repository.ModifyInvestment(investment, sqlQuery);

            return IsSuccess.Equals(true) ? (true, Message) : (false, Message);
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
            var (IsSuccess, Message) = await repository.DeleteInvestment(id, portfolioId);
            return IsSuccess.Equals(true) ? (true, Message) : (false, Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return (false, ex.Message);
        }
    }
}