using Dapper;
using MySql.Data.MySqlClient;
using SpicyGarnachas.InvestmentApiV2.Models;
using SpicyGarnachas.InvestmentApiV2.Repositories.Interfaces;
using System.Security.Cryptography;
using System.Text;
namespace SpicyGarnachas.InvestmentApiV2;

public class UserRepository : IUserRepository
{
    private readonly ILogger<UserRepository> logger;
    private readonly IConfiguration _configuration;

    public UserRepository(ILogger<UserRepository> logger, IConfiguration configuration)
    {
        this.logger = logger;
        _configuration = configuration;
    }

    public async Task<(bool IsSuccess, UserModel, string Message)> GetUserData(string username)
    {
        try
        {
            string connectionString = _configuration.GetConnectionString("mainServer");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sqlQuery = $"SELECT * FROM Users WHERE username = '{username}'";
                var user = await connection.QuerySingleOrDefaultAsync<UserModel>(sqlQuery);
                if (user == null)
                {
                    return (false, null, "The user is not registered");
                }
                return (true, user, string.Empty);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return (false, null, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, string Message)> RegisterUser(UserModel user)
    {
        try
        {
            string? connectionString = _configuration.GetConnectionString("mainServer");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sqlQuery = $"INSERT INTO users (userName, password, salt, createdOn, updatedOn) VALUES ('{user.userName}', '{user.password}', '{user.salt}', NOW(), NOW())";
                var users = await connection.QueryAsync<UserModel>(sqlQuery);
                return (IsSuccess: true, string.Empty);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return (false, ex.Message);
        }
    }

    public async Task<(bool isSuccess, string salt)> GenerateRandomSalt()
    {
        try
        {
            byte[] salt = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return (true, Convert.ToBase64String(salt));
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return (false, ex.Message);
        }
    }

    public async Task<(bool isSuccess, string hash)> EncriptPassword(string salt, string password)
    {
        try
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(salt), 2, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(16);
                return (true, Convert.ToBase64String(hash));
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return (false, ex.Message);
        }
    }
}
