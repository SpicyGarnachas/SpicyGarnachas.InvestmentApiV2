using SpicyGarnachas.FinanceApiV2.Models;

namespace SpicyGarnachas.FinanceApiV2.Repositories.Interfaces;

public interface ITransactionRepository
{
    Task<(bool IsSuccess, IEnumerable<TransactionModel>?, string Message)> GetTransactionsData();
    Task<(bool IsSuccess, IEnumerable<TransactionModel>?, string Message)> GetTransactionsDataByPortfolioId(int id);
}