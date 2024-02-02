using SpicyGarnachas.InvestmentApiV2.Models;

namespace SpicyGarnachas.InvestmentApiV2.Repositories.Interfaces;

public interface IInvestmentRepository
{
    Task<(bool IsSuccess, IEnumerable<InvestmentModel>?, string Message)> GetInvestmentData();
    Task<(bool IsSuccess, IEnumerable<InvestmentModel>?, string Message)> GetInvestmentDataByPortfolioId(int id);
    Task<(bool IsSuccess, string Message)> CreateNewInvestment(InvestmentModel invest);
    Task<(bool IsSuccess, string Message)> ModifyInvestment(int id, string sqlQuery);
    Task<(bool IsSuccess, string Message)> DeleteInvestment(int id, int portfolioId);
}