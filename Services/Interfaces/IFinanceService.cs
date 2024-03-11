using SpicyGarnachas.FinanceApiV2.Models;

namespace SpicyGarnachas.FinanceApiV2.Services.Interfaces;

public interface IFinanceService
{
    Task<(bool IsSuccess, IEnumerable<FinanceModel>?, string Message)> GetFinanceData();
    Task<(bool IsSuccess, IEnumerable<FinanceModel>?, string Message)> GetFinanceDataByPortfolioId(int id);
    Task<(bool IsSuccess, string Message)> CreateNewFinance(FinanceModel invest);
    Task<(bool IsSuccess, string Message)> ModifyFinance(FinanceModel investment);
    Task<(bool IsSuccess, string Message)> DeleteFinance(int id, int portfolioId);
}