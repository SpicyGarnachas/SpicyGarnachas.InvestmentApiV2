using MySql.Data.MySqlClient;
using SpicyGarnachas.FinanceApiV2.Models;
using SpicyGarnachas.FinanceApiV2.Repositories.Interfaces;
using SpicyGarnachas.FinanceApiV2.Services.Interfaces;
using System.Security.Policy;

namespace SpicyGarnachas.FinanceApiV2;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly ILogger<UserService> logger;

    public UserService(IUserRepository repository, ILogger<UserService> logger)
    {
        this.userRepository = repository;
        this.logger = logger;
    }

    public async Task<(bool IsSuccess, string Message)> Login(string username, string password)
    {
        try
        {
            var (userExist, userData, message) = await userRepository.GetUserData(username);

            if (userExist)
            {
                var (passwordIsEncripted, hash) = await userRepository.EncriptPassword(userData.salt, password);
                if (hash == userData.password)
                {
                    return (true, "User Login was succesfull");
                }
            }
            return (false, string.IsNullOrEmpty(message) ? "User password was incorrect" : message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return (false, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, string Message)> Register(UserModel user)
    {
        try
        {
            var (saltIsGenerated, salt) = await userRepository.GenerateRandomSalt();

            var (passwordIsEncripted, hash) = await userRepository.EncriptPassword(salt, user.password);

            user.password = hash;
            user.salt = salt;

            if (passwordIsEncripted && saltIsGenerated)
            {
                var (userIsRegistered, Message) = await userRepository.RegisterUser(user);
            }

            return (true, string.Empty);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return (false, ex.Message);
        }
    }
}