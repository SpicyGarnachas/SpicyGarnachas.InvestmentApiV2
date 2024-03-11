using SpicyGarnachas.InvestmentApiV2.Models;

namespace SpicyGarnachas.InvestmentApiV2.Services.Interfaces;

public interface ITransactionService
{
    Task<(bool IsSuccess, IEnumerable<TransactionModel>?, string Message)> GetTransactionsData();
    Task<(bool IsSuccess, IEnumerable<TransactionModel>?, string Message)> GetTransactionsDataByPortfolioId(int id);
}