﻿using SpicyGarnachas.InvestmentApiV2.Models;

namespace SpicyGarnachas.InvestmentApiV2.Repositories.Interfaces;

public interface IFinanceRepository
{
    Task<(bool IsSuccess, IEnumerable<FinanceModel>?, string Message)> GetFinanceData();
    Task<(bool IsSuccess, IEnumerable<FinanceModel>?, string Message)> GetFinanceDataByPortfolioId(int id);
    Task<(bool IsSuccess, string Message)> CreateNewFinance(FinanceModel invest);
    Task<(bool IsSuccess, string Message)> ModifyFinance(FinanceModel invest, string sqlQuery);
    Task<(bool IsSuccess, string Message)> DeleteFinance(int id, int portfolioId);
}