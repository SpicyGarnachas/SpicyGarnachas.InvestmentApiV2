using SpicyGarnachas.InvestmentApiV2.Models;
namespace SpicyGarnachas.InvestmentApiV2.Repositories.Interfaces;

public interface IPortfolioRepository
{
    Task<(bool IsSuccess, IEnumerable<PortfolioModel>?, string Message)> GetPortfolioData();
    Task<(bool IsSuccess, IEnumerable<PortfolioModel>?, string Message)> GetPortfolioByUserId(int id);
    Task<(bool IsSuccess, string Message)> CreateNewPortfolio(PortfolioModel portfolio);
    Task<(bool IsSuccess, string Message)> ModifyPorfolio(int id, string sqlQuery);
    Task<(bool IsSuccess, string Message)> DeletePortfolio(int id, int userId);
}