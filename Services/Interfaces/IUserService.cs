using SpicyGarnachas.FinanceApiV2.Models;

namespace SpicyGarnachas.FinanceApiV2.Services.Interfaces;

public interface IUserService
{
    Task<(bool IsSuccess, string Message)> Register(UserModel user);
    Task<(bool IsSuccess, string Message)> Login(string username, string password);

}
